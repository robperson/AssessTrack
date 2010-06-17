using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;

namespace AssessTrack.Models
{
    public partial class QuestionTag : IBackupItem
    {
        #region IBackupItem Members

        public Guid objectID
        {
            get;
            set;
        }

        public System.Xml.Linq.XElement Serialize()
        {
            XElement questiontag =
                new XElement("questiontag",
                    new XElement("tagid", TagID.ToString()),
                    new XElement("questionid", QuestionID.ToString()));
            return questiontag;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                TagID = new Guid(source.Element("tagid").Value);
                QuestionID = new Guid(source.Element("questionid").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize QuestionTag entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.sp_AddQuestionTag(TagID, QuestionID);
        }

        #endregion
    }
}
