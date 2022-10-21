using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Security;
using System.Text;
using vOS.UserSpace.Instance;

namespace vOS.UserSpace
{
    public class User
    {
        private User(string name, string id)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Id = id ?? throw new ArgumentNullException(nameof(id));
        }

        public readonly string Name;
        public readonly string Id;

        internal ObservableCollection<Process> instances;

        public static User GetUser(string username, SecureString password)
        {
            // TODO: TwoFish Decryption

            return new User(
                username,
                username.Trim(Path.GetInvalidFileNameChars())
            );
        }

        public static User CreateUser()
        {
            throw new NotImplementedException("CreateUser");
        }

        public static bool RemoveUser()
        {
            throw new NotImplementedException("RemoveUser");
        }

        public bool StartNewInstance(Process instance)
        {
            instances.Add(instance); // Add the current process to the active processes list

            var exitCode = Command.Send(instance.StartInfo.FileName + " " + instance.StartInfo.Arguments);

            instances.Remove(instance); // Remove the current process to the active processes list

            OnExit?.Invoke(typeof(Process), ExitCode.Value); // Signal that the processes have exited
        }
    }
}
