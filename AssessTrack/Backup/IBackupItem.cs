using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AssessTrack.Models;

namespace AssessTrack.Backup
{
    public interface IBackupItem
    {
        XElement Serialize();
        void Deserialize(XElement source);
        void LinkRelationships(SiteBackup sitebackup);

        //The items ID as found in the backup file.
        Guid objectID
        {
            get;
            set;
        }
        void Insert(AssessTrackModelClassesDataContext dc);
        void OnPostInsert();
    }
}
