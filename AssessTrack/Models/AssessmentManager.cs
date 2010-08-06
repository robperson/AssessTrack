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
using System.Transactions;
using System.Collections.Generic;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public Assessment GetAssessmentByName(CourseTerm term, string name)
        {
            return term.Assessments.SingleOrDefault(a => a.Name == name);
        }

        public Assessment GetAssessmentByID(CourseTerm courseTerm, Guid id)
        {
            return courseTerm.Assessments.SingleOrDefault(a => a.AssessmentID == id);
        }

        public void SaveAssessment(Assessment assessment)
        {
            SaveAssessment(assessment, true);
        }

        public void SaveAssessment(Assessment assessment, bool isNew)
        {
            try
            {
                using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required,TimeSpan.MaxValue))
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
                    if (tags != null)
                    {
                        string[] tagList = tags.Value.Split(new char[] { ',' });
                        foreach (string tagname in tagList)
                        {
                            Tag tag = GetTagByName(assessment.CourseTerm, tagname);
                            if (tag == null)
                            {
                                tag = new Tag()
                                {
                                    Profile = GetLoggedInProfile(),
                                    Name = tagname,
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
                        if (isNew)
                        {
                            question = new Question();
                        }
                        else
                        {
                            question = dc.Questions.SingleOrDefault(q => q.QuestionID.ToString() == questionNode.Attribute("id").Value);
                        }
                        question.Weight = questionNode.Elements("answer").Sum(a => Convert.ToDouble(a.Attribute("weight").Value));
                        question.Data = questionNode.ToString();

                        if (isNew)
                        {
                            assessment.Questions.Add(question);
                            dc.SubmitChanges();
                            questionNode.SetAttributeValue("id", question.QuestionID);
                        }

                        //TODO: Remove this line
                        DeleteAllTagsFromQuestion(question);
                        tags = questionNode.Attribute("tags");
                        if (tags != null)
                        {
                            string[] tagList = tags.Value.Split(new char[] { ',' });
                            foreach (string tagname in tagList)
                            {
                                Tag tag = GetTagByName(assessment.CourseTerm, tagname);
                                if (tag == null)
                                {
                                    tag = new Tag()
                                    {
                                        Profile = GetLoggedInProfile(),
                                        Name = tagname,
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
                            if (isNew)
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

                            if (isNew)
                            {
                                answer.Assessment = assessment;
                                dc.Answers.InsertOnSubmit(answer);
                                dc.SubmitChanges();
                                answerNode.SetAttributeValue("id", answer.AnswerID);
                            }

                            //TODO: Remove this line
                            DeleteAllTagsFromAnswer(answer);
                            tags = answerNode.Attribute("tags");
                            if (tags != null)
                            {
                                string[] tagList = tags.Value.Split(new char[] { ',' });
                                foreach (string tagname in tagList)
                                {
                                    if (string.IsNullOrEmpty(tagname))
                                    {
                                        continue;
                                    }
                                    Tag tag = GetTagByName(assessment.CourseTerm, tagname);
                                    if (tag == null)
                                    {
                                        tag = new Tag()
                                        {
                                            Profile = GetLoggedInProfile(),
                                            Name = tagname,
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
            return (from asmt in courseTerm.Assessments
                    where asmt.DueDate.CompareTo(DateTime.Now) >= 0
                    && asmt.IsVisible
                    && !asmt.AssessmentType.QuestionBank
                    orderby asmt.DueDate descending
                    select asmt).ToList();
        }
        public List<Assessment> GetAllNonTestBankAssessments(CourseTerm courseTerm)
        {
            return (from asmt in courseTerm.Assessments
                    where !asmt.AssessmentType.QuestionBank
                    orderby asmt.DueDate descending
                    select asmt).ToList();
        }
        public List<Assessment> GetPastDueAssessments(CourseTerm courseTerm)
        {
            return (from asmt in courseTerm.Assessments
                    where asmt.DueDate.CompareTo(DateTime.Now) < 0
                    && asmt.IsVisible
                    && !asmt.AssessmentType.QuestionBank
                    orderby asmt.DueDate descending
                    select asmt).ToList();
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
    }

}
