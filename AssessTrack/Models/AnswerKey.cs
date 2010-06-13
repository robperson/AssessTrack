using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Backup;
using System.Xml.Linq;

namespace AssessTrack.Models
{
    public partial class AnswerKey: IBackupItem
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
            XElement answerkey =
                new XElement("answerkey",
                    new XElement("answerkeyid", AnswerKeyID.ToString()),
                    new XElement("answerid", AnswerID.ToString()),
                    new XElement("weight", Weight.ToString()),
                    new XElement("value", HttpContext.Current.Server.HtmlEncode(Value)));
            return answerkey;
        }

        public void Deserialize(System.Xml.Linq.XElement source)
        {
            try
            {
                AnswerKeyID = new Guid(source.Element("answerkeyid").Value);
                AnswerID = new Guid(source.Element("answerid").Value);
                Weight = double.Parse(source.Element("weight").Value);
                Value = HttpContext.Current.Server.HtmlDecode(source.Element("value").Value);
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize AnswerKey entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.AnswerKeys.InsertOnSubmit(this);
        }

        #endregion
    }
}
