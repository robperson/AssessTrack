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

namespace AssessTrack.Models
{
    public partial class Answer
    {
        public int Number
        {
            get
            {
                if (Assessment != null)
                {
                    XElement markup = XElement.Parse(Assessment.Data);
                    return Convert.ToInt32(markup.XPathEvaluate(string.Format("count(//answer[@id='{0}']/preceding-sibling::answer) + 1", AnswerID)));
                }
                throw new Exception("Answer cannot have a number until it is assigned to an Assessment.");
            }
        }
    }
}
