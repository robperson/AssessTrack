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
        //The items ID as found in the backup file.
        Guid objectID
        {
            get;
            set;
        }

        XElement Serialize();
        void Deserialize(XElement source);
        void Insert(AssessTrackModelClassesDataContext dc);
    }
}
