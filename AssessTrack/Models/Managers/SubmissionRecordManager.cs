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
using System.Collections.Generic;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public void SaveSubmissionRecord(SubmissionRecord record)
        {
            dc.SubmissionRecords.InsertOnSubmit(record);
            dc.SubmitChanges();
        }

        public bool HasUserSubmittedAssessment(Assessment assessment)
        {
            Guid Userid = (Guid)Membership.GetUser(HttpContext.Current.User.Identity.Name).ProviderUserKey;
            int submissionCount = dc.SubmissionRecords.Count(s => s.AssessmentID == assessment.AssessmentID
                                                              && s.StudentID == Userid);
            return (submissionCount > 0);
        }

        public SubmissionRecord GetSubmissionRecordByID(Guid id)
        {
            return dc.SubmissionRecords.SingleOrDefault(s => s.SubmissionRecordID == id);
        }

        public List<SubmissionRecord> GetOtherSubmissionRecords(SubmissionRecord record)
        {
            var subs = from sub in dc.SubmissionRecords
                       where sub.SubmissionRecordID != record.SubmissionRecordID
                       && sub.AssessmentID == record.AssessmentID
                       && sub.StudentID == record.StudentID
                       orderby sub.SubmissionDate descending
                       select sub;
            return subs.ToList();

        }
    }
}
