﻿using System;
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

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public bool IsInstalled()
        {
            return dc.DatabaseExists();
        }

    }
}
