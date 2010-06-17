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
using System.Xml.XPath;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    public partial class Question : IBackupItem
    {
        public int Number
        {
            get
            {
                if (Assessment != null)
                {
                    XElement markup = XElement.Parse(Assessment.Data);
                    return Convert.ToInt32(markup.XPathEvaluate(string.Format("count(//question[@id='{0}']/preceding-sibling::question) + 1",QuestionID)));
                }
                throw new Exception("Question cannot have a number until it is assigned to an Assessment.");
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
            XElement question =
                new XElement("question",
                    new XElement("questionid", QuestionID.ToString()),
                    new XElement("weight", Weight.ToString()),
                    new XElement("assessmentid", AssessmentID.ToString()),
                    new XElement("data", Data));
            return question;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                QuestionID = new Guid(source.Element("questionid").Value);
                Weight = Convert.ToDouble(source.Element("weight").Value);
                AssessmentID = new Guid(source.Element("assessmentid").Value);
                Data = source.Element("data").Value;
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize Question entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.Questions.InsertOnSubmit(this);
        }

        #endregion
    }
}
