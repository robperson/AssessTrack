using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Backup
{
    public static class BackupItemFactory
    {
        private static Dictionary<string, Type> _typeMap = new Dictionary<string, Type>
        {
            {"profile", typeof(AssessTrack.Models.Profile)}
        };

        public static IBackupItem CreateBackupItem(string typename)
        {
            Type t = _typeMap[typename];
            Object backupItem = Activator.CreateInstance(t);
            return (IBackupItem)backupItem;
        }
    }
}
