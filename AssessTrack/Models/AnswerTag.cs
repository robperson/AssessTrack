using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;

namespace AssessTrack.Models
{
    public partial class AnswerTag : IBackupItem
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
            XElement answertag =
                new XElement("answertag",
                    new XElement("tagid", TagID.ToString()),
                    new XElement("answerid", AnswerID.ToString()));
            return answertag;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                TagID = new Guid(source.Element("tagid").Value);
                AnswerID = new Guid(source.Element("answerid").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize AnswerTag entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.sp_AddAnswerTag(TagID, AnswerID);
        }

        #endregion
    }
}
