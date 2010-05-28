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
using System.Transactions;
using System.Collections.Generic;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public void CreateTerm(Term newTerm)
        {
            using (TransactionScope t = new TransactionScope())
            {
                dc.Terms.InsertOnSubmit(newTerm);
                dc.SubmitChanges();
                

                t.Complete();
            }
        }

        public IEnumerable<Term> GetSiteTerms(Site site)
        {
            return site.Terms.ToList();
        }

        public Term GetTermByID(Guid id)
        {
            return (from term in dc.Terms where term.TermID == id select term).SingleOrDefault();
        }
    }
}
