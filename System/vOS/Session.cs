using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;

namespace vOS
{
    public class Session
    {
        public static Session CurrentSession { get; private set; }

        private static List<Session> sessions;

        public User user { get; private set; }

        internal Session(string userProfil)
        {
            user = new User { Name = "Root", Id = "Root".Trim(Path.GetInvalidFileNameChars()) }; // Empty name check inside user
        }

        public static Session LogIn(string userName, SecureString password = null)
        {
            // Already connected
            if (CurrentSession != null)
                return null;

            // TODO: TwoFish Decryption
            CurrentSession = new Session(userName);

            if (CurrentSession != null)
                Debug.WriteLine($"{CurrentSession.user.Name} log in");

            return CurrentSession;
        }

        public static void Switch(Session session)
        {
            Debug.WriteLine($"{CurrentSession.user.Name} switch to {session.user.Name}");

            CurrentSession = session;
        }

        public static void LogOut()
        {
            Debug.WriteLine($"{CurrentSession.user.Name} log out");

            sessions.Remove(CurrentSession);
            CurrentSession = null;
        }
    }
}
