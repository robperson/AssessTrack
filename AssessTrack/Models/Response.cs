using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;

namespace AssessTrack.Models
{
    public partial class Response : IBackupItem
    {
        #region IBackupItem Members

        public Guid objectID
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Xml.Linq.XElement Serialize()
        {
            XElement response =
                new XElement("response",
                    new XElement("responseid", ResponseID.ToString()),
                    new XElement("submissionrecordid", SubmissionRecordID.ToString()),
                    new XElement("answerid", AnswerID.ToString()),
                    new XElement("responsetext", ResponseText),
                    new XElement("score", Score.ToString()));
            return response;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                ResponseID = new Guid(source.Element("responseid").Value);
                SubmissionRecordID = new Guid(source.Element("submissionrecordid").Value);
                AnswerID = new Guid(source.Element("answerid").Value);
                ResponseText = source.Element("responsetext").Value;
                Score = double.Parse(source.Element("score").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize Response entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.Responses.InsertOnSubmit(this);
        }

        #endregion
    }
}
