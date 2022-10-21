using System.IO;
using vOS.UserSpace;

namespace vOS
{
    public class Storage
    {
        public enum KnowFolders
        {
            Users,
            User_Home,
            User_Documents,
            User_Galerie,
            User_Download,
            System,
            Applications,
        }

        public static string GetFolder(KnowFolders knowFolders)
        {
            switch (knowFolders)
            {
                case KnowFolders.Users:
                    return StoragePaths.Users;
                case KnowFolders.User_Home:
                    return Path.Combine(StoragePaths.Users, Session.CurrentSession.user.Id);
                case KnowFolders.User_Documents:
                    return Path.Combine(GetFolder(KnowFolders.Users), "Documents");
                case KnowFolders.User_Galerie:
                    return Path.Combine(GetFolder(KnowFolders.Users), "Galerie");
                case KnowFolders.User_Download:
                    return Path.Combine(GetFolder(KnowFolders.Users), "Download");
                case KnowFolders.System:
                    return StoragePaths.System;
                case KnowFolders.Applications:
                    return StoragePaths.Applications;
                default:
                    throw new DirectoryNotFoundException("Unknown folder");
            }
        }
    }
}
