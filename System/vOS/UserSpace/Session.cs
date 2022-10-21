#nullable enable

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;
using vOS.UserSpace.Instance;
using Process = vOS.UserSpace.Instance.Process;

namespace vOS.UserSpace
{
    /// <summary>
    /// Represent a session wich a user can longon logout, in average vOS should only have one session
    /// </summary>
    internal class Session
    {
        internal Session()
        {
            sessions = new();
            users = new();
        }

        private static ObservableCollection<Session> sessions; // One session = one terminal
        private static ObservableCollection<User> users; // All active user, multi user is possible

        public User? CurrentUser { get; set; }

        /// <summary>
        /// Get the current user from the given instance
        /// </summary>
        /// <param name="handle">Instance handle</param>
        /// <returns>User from the instance</returns>
        /// <exception cref="ArgumentException"></exception>
        public static User GetCurrentUser(Guid handle)
        {
            foreach (var user in users)
            {
                var instance = Process.GetProcessByHandle(user, handle);

                if (instance != null)
                    return user;
            }

            throw new ArgumentException("The instance specified by the handle parameter is not running.");
        }

        public void LogIn(string userName, SecureString password)
        {
            // Already connected
            if (CurrentUser != null)
                Switch(default);

            CurrentUser = User.GetUser(userName, password);

            if (CurrentUser != null)
                Debug.WriteLine($"{CurrentUser.Name} log in");
        }

        private void Switch(User session)
        {


            /*Debug.WriteLine($"{CurrentSession.User.Name} switch to {session.User.Name}");

            CurrentSession = session;*/
        }

        public void LogOut()
        {
            /*Debug.WriteLine($"{CurrentSession.User.Name} log out");

            sessions.Remove(CurrentSession);
            CurrentSession = null;*/
        }
    }
}
