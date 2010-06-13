using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Backup
{
    public static class BackupItemFactory
    {
        private static Dictionary<string, Type> _typeMap = new Dictionary<string, Type>
        {
            {"profile", typeof(AssessTrack.Models.Profile)},
            {"sitemember", typeof(AssessTrack.Models.SiteMember)},
            {"site", typeof(AssessTrack.Models.Site)},
            {"course", typeof(AssessTrack.Models.Course)},
            {"term", typeof(AssessTrack.Models.Term)},
            {"courseterm", typeof(AssessTrack.Models.CourseTerm)},
            {"coursetermmember", typeof(AssessTrack.Models.CourseTermMember)},
            {"tag", typeof(AssessTrack.Models.Tag)},
            {"assessmenttag", typeof(AssessTrack.Models.AssessmentTag)},
            {"questiontag", typeof(AssessTrack.Models.QuestionTag)},
            {"answertag", typeof(AssessTrack.Models.AnswerTag)},
            {"assessment", typeof(AssessTrack.Models.Assessment)},
            {"assessmenttype", typeof(AssessTrack.Models.AssessmentType)},
            {"question", typeof(AssessTrack.Models.Question)},
            {"answer", typeof(AssessTrack.Models.Answer)},
            {"answerkey", typeof(AssessTrack.Models.AnswerKey)},
            {"submissionrecord", typeof(AssessTrack.Models.SubmissionRecord)},
            {"response", typeof(AssessTrack.Models.Response)},
            {"membershipinfo", typeof(AssessTrack.Models.MembershipInfo)}
        };

        public static IBackupItem CreateBackupItem(string typename)
        {
            Type t = _typeMap[typename];
            Object backupItem = Activator.CreateInstance(t);
            return (IBackupItem)backupItem;
        }
    }
}
