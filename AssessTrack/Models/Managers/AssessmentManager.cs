using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Transactions;
using System.Collections.Generic;
using AssessTrack.Helpers;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public Question GetQuestionByID(Guid id)
        {
            return dc.Questions.SingleOrDefault(q => q.QuestionID == id);
        }

        public List<Assessment> GetUpcomingUnsubmittedAssessments()
        {
            var assessments = from assessment in dc.Assessments
                              from member in dc.CourseTermMembers
                              where member.MembershipID == UserHelpers.GetCurrentUserID()
                              && member.CourseTermID == assessment.CourseTermID && member.AccessLevel == 1
                              && assessment.SubmissionRecords.Where(sub => sub.StudentID == member.MembershipID).Count() == 0
                              && assessment.DueDate.CompareTo(DateTime.Now) > 0
                              && assessment.IsVisible && !assessment.AssessmentType.QuestionBank
                              select assessment;

            return assessments.ToList();
        }

        public Assessment GetAssessmentByName(CourseTerm term, string name)
        {
            return term.Assessments.SingleOrDefault(a => a.Name == name);
        }

        public Assessment GetAssessmentByID(Guid id)
        {
            return dc.Assessments.SingleOrDefault(a => a.AssessmentID == id);
        }

        public Assessment GetAssessmentByID(CourseTerm courseTerm, Guid id)
        {
            return courseTerm.Assessments.SingleOrDefault(a => a.AssessmentID == id);
        }

        public void SaveAssessment(Assessment assessment)
        {
            SaveAssessment(assessment, true);
        }

        public void DeleteAnswer(Answer answer)
        {
            dc.Answers.DeleteOnSubmit(answer);
            dc.AnswerKeys.DeleteAllOnSubmit(answer.AnswerKeys);
            dc.Responses.DeleteAllOnSubmit(answer.Responses);
            DeleteAllTagsFromAnswer(answer);
        }

        public void DeleteQuestion(Question question)
        {
            //Delete the answers
            foreach (var answer in question.Answers)
            {
                DeleteAnswer(answer);
            }
            dc.Questions.DeleteOnSubmit(question);
            DeleteAllTagsFromQuestion(question);
        }

        public void DeleteAssessment(Assessment assessment)
        {
            dc.SubmissionExceptions.DeleteAllOnSubmit(assessment.SubmissionExceptions);
            DeleteAllTagsFromAssessment(assessment);
            foreach (var question in assessment.Questions)
            {
                DeleteQuestion(question);
            }
            dc.SubmissionRecords.DeleteAllOnSubmit(assessment.SubmissionRecords);
            dc.Assessments.DeleteOnSubmit(assessment);
        }

        public void SaveAssessment(Assessment assessment, bool isNew)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    if (isNew)
                    {
                        dc.Assessments.InsertOnSubmit(assessment);
                        dc.SubmitChanges();
                    }

                    XElement markup = XElement.Parse(assessment.Data);
                    //TODO: Remove this code and replace it with code to notify if related tag is missing from tag list
                    // after the ability to delete tags from an assessment is added to the UI
                    DeleteAllTagsFromAssessment(assessment);

                    XAttribute tags = markup.Attribute("tags");
                    if (tags != null && !string.IsNullOrEmpty(tags.Value))
                    {
                        string[] tagList = tags.Value.Split(new char[] { ',' });
                        foreach (string tagname in tagList)
                        {
                            string cleanName = tagname.Trim();
                            if (string.IsNullOrEmpty(cleanName))
                            {
                                continue;
                            }
                            Tag tag = GetTagByName(assessment.CourseTerm, cleanName);
                            if (tag == null)
                            {
                                throw new Exception("Tag not found.");
                                tag = new Tag()
                                {
                                    Profile = GetLoggedInProfile(),
                                    Name = cleanName,
                                    CourseTerm = assessment.CourseTerm
                                };
                                //TODO find a way to defer saving tags to db in order
                                //to link tags to questions
                                dc.Tags.InsertOnSubmit(tag);
                                dc.SubmitChanges();
                                //throw new Exception(string.Format("Invalid TagID in Assessments 'tags' attribute. ID = {{{0}}}", tagid));
                            }
                            if (!AssessmentHasTag(assessment, tag))
                            {
                                AddTagToAssessment(tag, assessment);
                            }
                        }
                    }
                    foreach (XElement questionNode in markup.Elements("question"))
                    {
                        Question question;
                        if (questionNode.Attribute("id") != null && isNew)
                        {
                            throw new InvalidOperationException("Do not use 'id' Attribute when creating new Assessments");
                        }
                        if (isNew || questionNode.Attribute("id") == null)
                        {
                            question = new Question();
                            assessment.Questions.Add(question);
                        }
                        else
                        {
                            question = dc.Questions.SingleOrDefault(q => q.QuestionID.ToString() == questionNode.Attribute("id").Value);
                        }
                        question.Weight = questionNode.Elements("answer").Sum(a => Convert.ToDouble(a.Attribute("weight").Value));
                        question.Data = questionNode.ToString();

                        if (isNew || questionNode.Attribute("id") == null)
                        {
                            
                            dc.SubmitChanges();
                            questionNode.SetAttributeValue("id", question.QuestionID);
                        }
                        question.Data = questionNode.ToString();
                        //TODO: Remove this line
                        DeleteAllTagsFromQuestion(question);
                        tags = questionNode.Attribute("tags");
                        if (tags != null && !string.IsNullOrEmpty(tags.Value))
                        {
                            string[] tagList = tags.Value.Split(new char[] { ',' });
                            foreach (string tagname in tagList)
                            {
                                string cleanName = tagname.Trim();
                                if (string.IsNullOrEmpty(cleanName))
                                {
                                    continue;
                                }
                                Tag tag = GetTagByName(assessment.CourseTerm, cleanName);
                                if (tag == null)
                                {
                                    throw new Exception("Tag not found.");
                                    tag = new Tag()
                                    {
                                        Profile = GetLoggedInProfile(),
                                        Name = cleanName,
                                        CourseTerm = assessment.CourseTerm
                                    };
                                    //TODO find a way to defer saving tags to db in order
                                    //to link tags to questions
                                    dc.Tags.InsertOnSubmit(tag);
                                    dc.SubmitChanges();
                                    //throw new Exception(string.Format("Invalid TagID in Question 'tags' attribute. ID = {{{0}}}, Question ID = {{{1}}}", tagname, question.QuestionID));
                                }
                                if (!QuestionHasTag(question, tag))
                                {
                                    AddTagToQuestion(tag, question);
                                }
                            }
                        }

                        foreach (XElement answerNode in questionNode.Elements("answer"))
                        {
                            Answer answer;
                            if (answerNode.Attribute("id") != null && isNew)
                            {
                                throw new InvalidOperationException("Do not use 'id' Attribute when creating new Assessments");
                            }
                            if (isNew || answerNode.Attribute("id") == null)
                            {
                                answer = new Answer();
                            }
                            else
                            {
                                answer = dc.Answers.SingleOrDefault(a => a.AnswerID.ToString() == answerNode.Attribute("id").Value);
                            }

                            //answer.AnswerKey = answerNode.Attribute("key").Value;
                            dc.AnswerKeys.DeleteAllOnSubmit(answer.AnswerKeys.ToList());
                            dc.SubmitChanges();
                            XElement keysNode = answerNode.Element("AnswerKeys");
                            if (keysNode != null)
                            {
                                answer.AnswerKeyText = keysNode.ToString();
                                foreach (XElement key in keysNode.Elements("AnswerKey"))
                                {
                                    AnswerKey newKey = new AnswerKey();
                                    XAttribute keyWeight = key.Attribute("weight");
                                    // It is assumed that this required attribute will always
                                    // be present
                                    newKey.Weight = Convert.ToDouble(keyWeight.Value);
                                    newKey.Value = key.Value;
                                    answer.AnswerKeys.Add(newKey);
                                }
                            }

                            answer.Type = answerNode.Attribute("type").Value;
                            answer.Weight = Convert.ToDouble(answerNode.Attribute("weight").Value);
                            answer.Question = question;

                            XElement stdin = answerNode.Element("Stdin");
                            if (stdin != null)
                            {
                                answer.Stdin = stdin.Value;
                            }

                            XElement fstream = answerNode.Element("Fstream");
                            if (fstream != null)
                            {
                                answer.Fstream = fstream.Value;
                            }

                            if (isNew || answerNode.Attribute("id") == null)
                            {
                                answer.Assessment = assessment;
                                dc.Answers.InsertOnSubmit(answer);
                                dc.SubmitChanges();
                                answerNode.SetAttributeValue("id", answer.AnswerID);
                            }

                            //TODO: Remove this line
                            DeleteAllTagsFromAnswer(answer);
                            tags = answerNode.Attribute("tags");
                            if (tags != null && !string.IsNullOrEmpty(tags.Value))
                            {
                                string[] tagList = tags.Value.Split(new char[] { ',' });
                                foreach (string tagname in tagList)
                                {
                                    string cleanName = tagname.Trim();
                                    if (string.IsNullOrEmpty(cleanName))
                                    {
                                        continue;
                                    }
                                    Tag tag = GetTagByName(assessment.CourseTerm, cleanName);
                                    if (tag == null)
                                    {
                                        throw new Exception("Tag not found.");
                                        tag = new Tag()
                                        {
                                            Profile = GetLoggedInProfile(),
                                            Name = cleanName,
                                            CourseTerm = assessment.CourseTerm
                                        };
                                        //TODO find a way to defer saving tags to db in order
                                        //to link tags to questions
                                        dc.Tags.InsertOnSubmit(tag);
                                        dc.SubmitChanges();
                                        //throw new Exception(string.Format("Invalid TagID in Answer 'tags' attribute. ID = {{{0}}}, Answer ID = {{{1}}}", tagname, answer.AnswerID));
                                    }
                                    if (!AnswerHasTag(answer, tag))
                                    {
                                        AddTagToAnswer(tag, answer);
                                    }
                                }
                            }
                        }
                        question.Data = questionNode.ToString();
                        dc.SubmitChanges();
                    }
                    assessment.Data = markup.ToString();
                    //Ensure that we aren't deleting an answer and leaving dangling responses
                    foreach (Answer answer in assessment.Answers)
                    {
                        XElement answerNode = markup.XPathSelectElement(string.Format("//answer[@id='{0}']", answer.AnswerID));
                        if (answerNode == null)
                        {
                            DeleteAnswer(answer);
                        }
                    }
                    //Do the same for questions
                    foreach (Question question in assessment.Questions)
                    {
                        XElement questionNode = markup.XPathSelectElement(string.Format("//question[@id='{0}']", question.QuestionID));
                        if (questionNode == null)
                        {
                            DeleteQuestion(question);
                        }
                    }
                    dc.SubmitChanges();
                    transaction.Complete();
                }
            }
            catch
            {
                throw;
            }

        }

        public List<Assessment> GetUpcomingAssessments(CourseTerm courseTerm)
        {
            Guid userID = UserHelpers.GetCurrentUserID();
            CourseTermMember member = GetCourseTermMemberByMembershipID(courseTerm, userID);
            return GetUpcomingAssessments(courseTerm, member);
        }


        public List<Assessment> GetUpcomingAssessments(CourseTerm courseTerm, CourseTermMember member)
        {
            var assessments = (from asmt in courseTerm.Assessments
                    where asmt.DueDate.CompareTo(DateTime.Now) >= 0
                    && asmt.IsVisible
                    && !asmt.AssessmentType.QuestionBank
                    
                    select asmt);
            if (member.AccessLevel == 1)
            {
                var filteredAssessments = assessments.Where(a => a.Section == null || (member.Section.HasValue && a.Section.Value == member.Section));
                return filteredAssessments.OrderByDescending(a => a.DueDate).ToList();
            }

            return assessments.OrderByDescending(asmt => asmt.DueDate).ToList();
        }
        public List<Assessment> GetAllNonTestBankAssessments(CourseTerm courseTerm)
        {
            return GetAllNonTestBankAssessments(courseTerm, true);
        }

        public List<Assessment> GetAllNonTestBankAssessments(CourseTerm courseTerm, bool includeExtraCredit)
        {
            return GetAllNonTestBankAssessments(courseTerm, includeExtraCredit, null);
        }

        public List<Assessment> GetAllNonTestBankAssessments(CourseTerm courseTerm, bool includeExtraCredit, AssessmentType filterby)
        {
            Guid userID = UserHelpers.GetCurrentUserID();
            CourseTermMember member = GetCourseTermMemberByMembershipID(courseTerm, userID);
            return GetAllNonTestBankAssessments(courseTerm, includeExtraCredit, filterby, member);
        }

        public List<Assessment> GetAllNonTestBankAssessments(CourseTerm courseTerm, bool includeExtraCredit, AssessmentType filterby, CourseTermMember member)
        {
            var assessments = from asmt in courseTerm.Assessments
                    where !asmt.AssessmentType.QuestionBank && asmt.IsVisible
                    select asmt;
            if (!includeExtraCredit)
                assessments = assessments.Where(a => !a.IsExtraCredit);
            if (filterby != null)
                assessments = assessments.Where(a => a.AssessmentType == filterby);
            if (member.AccessLevel == 1 )
                assessments = assessments.Where(a => a.Section == null || (member.Section.HasValue && a.Section.Value == member.Section.Value));
            return assessments.OrderBy(a => a.Name).ToList();
        }

        public List<Assessment> GetPastDueAssessments(CourseTerm courseTerm)
        {
            Guid userID = UserHelpers.GetCurrentUserID();
            CourseTermMember member = GetCourseTermMemberByMembershipID(courseTerm, userID);
            return GetPastDueAssessments(courseTerm, member);
        }


        public List<Assessment> GetPastDueAssessments(CourseTerm courseTerm, CourseTermMember member)
        {
            var assessments = (from asmt in courseTerm.Assessments
                               where asmt.DueDate.CompareTo(DateTime.Now) < 0
                               && asmt.IsVisible
                               && !asmt.AssessmentType.QuestionBank

                               select asmt);
            if (member.AccessLevel == 1)
                assessments = assessments.Where(a => a.Section == null || (member.Section.HasValue && a.Section.Value == member.Section.Value));
            //{
            //    var filteredAssessments = assessments.Where(a => a.Section == null || (a.Section.Value == member.Section.Value));
            //    return filteredAssessments.OrderByDescending(a => a.DueDate).ToList();
            //}

            return assessments.OrderByDescending(asmt => asmt.DueDate).ToList();
        }
        public List<Assessment> GetQuestionBankAssessments(CourseTerm courseTerm)
        {
            return (from asmt in courseTerm.Assessments
                    where asmt.AssessmentType.QuestionBank
                    orderby asmt.DueDate descending
                    select asmt).ToList();
        }

        public List<Assessment> GetPrivateAssessments(CourseTerm courseTerm)
        {
            return (from asmt in courseTerm.Assessments
                    where !asmt.IsVisible
                    && !asmt.AssessmentType.QuestionBank
                    orderby asmt.DueDate descending
                    select asmt).ToList();
        }

        public double GetWeightedPointValue(Assessment assessment)
        {
            if (assessment.IsExtraCredit)
                return 0.0;
            double totalAssignedPoints = GetAllNonTestBankAssessments(assessment.CourseTerm, false).Sum(a => a.Weight);
            double assessmentTypePercentage = assessment.AssessmentType.Weight / 100.0;
            double assessmentTypeWeightedPoints = assessmentTypePercentage * totalAssignedPoints;
            double assessmentTypeTotalPoints = GetAllNonTestBankAssessments(assessment.CourseTerm,false).Where(a => a.AssessmentType == assessment.AssessmentType).Sum(a => a.Weight);
            double assessmentPointPercentage = assessment.Weight / assessmentTypeTotalPoints;
            double weightedValue = assessmentPointPercentage * assessmentTypeWeightedPoints;

            return weightedValue;
        }

        public List<Assessment> GetUngradedAssessments(CourseTerm courseTerm)
        {
            var assessments = GetAllNonTestBankAssessments(courseTerm);
            List<Assessment> ungraded = new List<Assessment>();
            foreach (var assessment in assessments)
            {
                List<SubmissionRecord> records = GetMostRecentSubmissions(assessment);
                if (records.Exists(sub => sub.GradedBy == null))
                {
                    ungraded.Add(assessment);
                }
            }

            return ungraded;

        }
    }

}
