using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;

namespace AssessTrack.Models
{
    public partial class AssessmentTag : IBackupItem
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
            XElement assessmenttag =
                new XElement("assessmenttag",
                    new XElement("tagid", TagID.ToString()),
                    new XElement("assessmentid", AssessmentID.ToString()));
            return assessmenttag;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                TagID = new Guid(source.Element("tagid").Value);
                AssessmentID = new Guid(source.Element("assessmentid").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize AssessmentTag entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.sp_AddAssessmentTag(TagID, AssessmentID);
        }

        #endregion
    }
}
