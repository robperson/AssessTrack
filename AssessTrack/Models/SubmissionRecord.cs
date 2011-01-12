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
using System.Data.Linq;
using System.Collections.Generic;
using AssessTrack.Helpers;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    public partial class SubmissionRecord : IBackupItem
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();
            //TODO: Confirm that score is not negative
            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
            {
                RuleViolation first = GetRuleViolations().First();
                throw new RuleViolationException(first.ErrorMessage);
            }
        }

        public double Score
        {
            get 
            {
                double score = 0.0;
                foreach (var response in Responses)
                {
                    score += response.Score ?? 0.0;
                }
                return score;
            }
        }

        #region IBackupItem Members

        public Guid objectID
        {
            get;
            set;
        }

        public XElement Serialize()
        {
            XElement submissionRecord =
                new XElement("submissionrecord",
                    new XElement("submissionrecordid", SubmissionRecordID.ToString()),
                    new XElement("studentid", StudentID.ToString()),
                    new XElement("assessmentid", AssessmentID.ToString()),
                    new XElement("submissiondate", SubmissionDate.ToString()),
                    new XElement("gradedon", GradedOn.ToString()),
                    new XElement("gradedby", GradedBy.ToString()),
                    new XElement("comments", HttpContext.Current.Server.HtmlEncode(Comments)));
            return submissionRecord;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                SubmissionRecordID = new Guid(source.Element("submissionrecordid").Value);
                StudentID = new Guid(source.Element("studentid").Value);
                AssessmentID = new Guid(source.Element("assessmentid").Value);
                SubmissionDate = DateTime.Parse(source.Element("submissiondate").Value);
                if (!string.IsNullOrEmpty(source.Element("gradedon").Value))
                {
                    GradedOn = DateTime.Parse(source.Element("gradedon").Value);
                }
                if (!string.IsNullOrEmpty(source.Element("gradedby").Value))
                {
                    GradedBy = new Guid(source.Element("gradedby").Value);
                }
                Comments = HttpContext.Current.Server.HtmlDecode(source.Element("comments").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize SubmissionRecord entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.SubmissionRecords.InsertOnSubmit(this);
        }

        #endregion
    }
}
