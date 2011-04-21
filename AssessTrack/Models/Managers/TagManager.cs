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
using System.Web.Mvc;
using System.Collections.Generic;
using AssessTrack.Helpers;
using AssessTrack.Models.ViewModels;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public Tag GetTagByName(CourseTerm courseTerm, string name)
        {
            return courseTerm.Tags.SingleOrDefault(t => t.Name == name);
        }

        public List<Tag> GetTags(CourseTerm courseTerm, bool includeEmptyTags)
        {
            var tags = courseTerm.Tags.ToList();
            if (!includeEmptyTags)
            {
                tags = tags.Where(t => GetTaggedItems(t).Count > 0).ToList();
            }
            return tags.OrderBy(t => t.Name).ToList();
        }

        public List<Tag> GetCourseOutcomes(CourseTerm courseTerm, bool includeEmptyTags)
        {
            var tags = courseTerm.Tags.Where(t => t.IsCourseOutcome).ToList();
            if (!includeEmptyTags)
            {
                tags = tags.Where(t => GetTaggedItems(t).Count > 0).ToList();
            }
            return tags.OrderBy(t => t.Name).ToList();
        }

        public List<Tag> GetNonCourseOutcomes(CourseTerm courseTerm, bool includeEmptyTags)
        {
            var tags = courseTerm.Tags.Where(t => !t.IsCourseOutcome).ToList();
            if (!includeEmptyTags)
            {
                tags = tags.Where(t => GetTaggedItems(t).Count > 0).ToList();
            }
            return tags.OrderBy(t => t.Name).ToList();
        }

        public Tag GetTagByID(CourseTerm courseTerm, Guid id)
        {
            return courseTerm.Tags.SingleOrDefault(t => t.TagID == id);
        }

        public SelectList GetTagsSelectList(CourseTerm courseTerm)
        {
            return GetTagsSelectList(courseTerm,null);
        }

        public SelectList GetTagsSelectList(CourseTerm courseTerm, object selectedValue)
        {
            return new SelectList(courseTerm.Tags, "TagID", "Name", selectedValue);
        }

        public List<Assessment> GetTaggedAssessments(Tag tag)
        {
            var Assessments = from aTag in dc.AssessmentTags
                              join a in dc.Assessments on aTag.AssessmentID equals a.AssessmentID
                              where aTag.TagID == tag.TagID
                              select a;
            return Assessments.ToList();
        }

        public void AddTagToAssessment(Tag tag, Assessment assessment)
        {
            AddTagToAssessment(tag.TagID,assessment.AssessmentID);
        }

        public void AddTagToAssessment(Guid tagid, Guid assessmentid)
        {
            dc.sp_AddAssessmentTag(tagid, assessmentid);
        }

        public bool AssessmentHasTag(Assessment assessment, Tag tag)
        {
            int tagCount = dc.AssessmentTags.Count(at => at.TagID == tag.TagID && at.AssessmentID == assessment.AssessmentID);
            return tagCount > 0;
        }

        public void DeleteTagFromAssessment(Assessment assessment, Tag tag)
        {
            dc.sp_DeleteTagFromAssessment(tag.TagID, assessment.AssessmentID);
        }

        public void DeleteAllTagsFromAssessment(Assessment assessment)
        {
            List<Tag> tags = GetTagsForAssessment(assessment);
            foreach (Tag tag in tags)
            {
                DeleteTagFromAssessment(assessment, tag);
            }
        }

        public List<Tag> GetTagsForAssessment(Assessment assessment)
        {
            var tags = from aTag in dc.AssessmentTags
                       join a in dc.Assessments on aTag.AssessmentID equals a.AssessmentID
                       join t in dc.Tags on aTag.TagID equals t.TagID
                       where a.AssessmentID == assessment.AssessmentID
                       select t;
            return tags.ToList();
        }

        public List<Question> GetTaggedQuestions(Tag tag)
        {
            var Questions = from qTag in dc.QuestionTags
                            join q in dc.Questions on qTag.QuestionID equals q.QuestionID
                            where qTag.TagID == tag.TagID
                            select q;
            return Questions.ToList();
        }

        public void AddTagToQuestion(Tag tag, Question question)
        {
            dc.sp_AddQuestionTag(tag.TagID, question.QuestionID);
        }

        public bool QuestionHasTag(Question question, Tag tag)
        {
            QuestionTag qTag = dc.QuestionTags.SingleOrDefault(qt => qt.TagID == tag.TagID && qt.QuestionID == question.QuestionID);
            return (qTag != null);
        }

        public void DeleteTagFromQuestion(Question question, Tag tag)
        {
            dc.sp_DeleteTagFromQuestion(tag.TagID, question.QuestionID);
        }

        public void DeleteAllTagsFromQuestion(Question question)
        {
            List<Tag> tags = GetTagsForQuestion(question);
            foreach (Tag tag in tags)
            {
                DeleteTagFromQuestion(question, tag);
            }
        }

        public List<Tag> GetTagsForQuestion(Question question)
        {
            var tags = from qTag in dc.QuestionTags
                       join q in dc.Questions on qTag.QuestionID equals q.QuestionID
                       join t in dc.Tags on qTag.TagID equals t.TagID
                       where q.QuestionID == question.QuestionID
                       select t;
            return tags.ToList();
        }


        public List<Answer> GetTaggedAnswers(Tag tag)
        {
            var Answers = from aTag in dc.AnswerTags
                            join a in dc.Answers on aTag.AnswerID equals a.AnswerID
                            where aTag.TagID == tag.TagID
                            select a;
            return Answers.ToList();
        }

        public void AddTagToAnswer(Tag tag, Answer answer)
        {
            dc.sp_AddAnswerTag(tag.TagID, answer.AnswerID);
        }

        public bool AnswerHasTag(Answer answer, Tag tag)
        {
            AnswerTag aTag = dc.AnswerTags.SingleOrDefault(at => at.TagID == tag.TagID && at.AnswerID == answer.AnswerID);
            return (aTag != null);
        }

        public void DeleteTagFromAnswer(Answer answer, Tag tag)
        {
            dc.sp_DeleteTagFromAnswer(tag.TagID, answer.AnswerID);
        }

        public void DeleteAllTagsFromAnswer(Answer answer)
        {
            List<Tag> tags = GetTagsForAnswer(answer);
            foreach (Tag tag in tags)
            {
                DeleteTagFromAnswer(answer, tag);
            }
        }

        public List<Tag> GetTagsForAnswer(Answer answer)
        {
            var tags = from ansTag in dc.AnswerTags
                       join a in dc.Answers on ansTag.AnswerID equals a.AnswerID
                       join t in dc.Tags on ansTag.TagID equals t.TagID
                       where a.AnswerID == answer.AnswerID
                       select t;
            return tags.ToList();
        }

        public List<ITaggable> GetTaggedItems(Tag tag)
        {
            List<ITaggable> items = new List<ITaggable>();
            foreach (var answer in GetTaggedAnswers(tag))
            {
                items.Add(answer);
            }
            foreach (var question in GetTaggedQuestions(tag))
            {
                items.Add(question);
            }
            foreach (var assessment in GetTaggedAssessments(tag))
            {
                items.Add(assessment);
            }

            return items;
        }

        public double GetStudentPfmeForTag(Tag tag, Profile profile)
        {
            double totalweight = 0.0;
            double totalpoints = 0.0;

            List<ITaggable> items = GetTaggedItems(tag);
            foreach (var taggeditem in items)
            {
                totalpoints += taggeditem.Score(profile);
                totalweight += taggeditem.Weight;
            }
            double avg = totalpoints / totalweight * 100;
            double pfme = GradeHelpers.GetPfme(avg);

            return pfme;
        }

        public double GetStudentScoreForTag(Tag tag, Profile profile)
        {
            double totalweight = 0.0;
            double totalpoints = 0.0;

            List<ITaggable> items = GetTaggedItems(tag);
            foreach (var taggeditem in items)
            {
                totalpoints += taggeditem.Score(profile);
                totalweight += taggeditem.Weight;
            }
            double avg = totalpoints / totalweight * 100;

            if (double.IsNaN(avg))
            {
                avg = -100.0;
            }

            return avg;
        }

        public List<TagViewModel> GetStrugglingTags(Profile p, CourseTerm t)
        {
            var tags = from tag in t.Tags
                       let score = GetStudentScoreForTag(tag, p)
                       where score <= 65.0 && score >= 0.0
                       select new TagViewModel() { Tag = tag, Score = GetStudentScoreForTag(tag, p) };
            return tags.ToList();
        }

        public void DeleteTagFromTaggable(Tag tag, ITaggable taggable)
        {
            if (taggable is Answer)
            {
                DeleteTagFromAnswer(taggable as Answer, tag);
            }
            else if (taggable is Question)
            {
                DeleteTagFromQuestion(taggable as Question, tag);
            }
            else if (taggable is Assessment)
            {
                DeleteTagFromAssessment(taggable as Assessment, tag);
            }
            else
            {
                throw new Exception("Cannot delete unsupported ITaggable object.");
            }
        }

        public void DeleteTag(Tag tag)
        {
            List<ITaggable> items = GetTaggedItems(tag);
            foreach (var item in items)
            {
                DeleteTagFromTaggable(tag, item);
            }

            var progOutcomeRelationships = from tpo in dc.TagProgramOutcomes
                                           where tpo.Tag == tag
                                           select tpo;
            dc.TagProgramOutcomes.DeleteAllOnSubmit(progOutcomeRelationships);

            dc.Tags.DeleteOnSubmit(tag);
        }
    }
}
