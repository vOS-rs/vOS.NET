using System;
using System.Security;
using vOS.UserSpace;
using vOS.UserSpace.Instance;

namespace vOS.Application.Terminal
{
    public class Program
    {
        static string workingDirectory;

        public static int Main(string[] args)
        {
            Process process = null;
            Terminal.WriteLine("vOS Terminal v1 - 2021");
            Terminal.WriteLine();

            while (true)
            {
                if (Session.CurrentSession == null)
                {
                    LogIn();
                }
                else
                {
                    Terminal.Write("> "); //{workingDirectory} 
                    if (args != null && args.Length > 1)
                    {
                        args[0] = string.Empty;
                        process = Send(string.Join(" ", args));
                        //Command.Send(string.Join(" ", args));

                        args = new string[0];
                    }
                    else
                    {
                        string line = Terminal.ReadLine();
                        switch (line)
                        {

                            case "exit":
                                return 0;

                            default:
                                process = Send(line);
                                break;
                        }
                    }
                }

                if (process != null)
                    process.WaitUntilExit();

                Terminal.WriteLine();
            }
        }

        private static Process Send(string command)
        {
            if (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
                return null;
            else
                return Process.Start(command);
        }

        // Should be a vexe
        private static void LogIn()
        {

            workingDirectory = string.Empty;

            Terminal.WriteLine("[Login]");
            Terminal.Write("UserName: ");
            string userName = Terminal.ReadLine();

            Session.LogIn(userName);

            if (Session.CurrentSession == null)
            {

                Terminal.Write("Password: ");
                string password = Terminal.ReadLine();
                var ss = new SecureString();
                foreach (var _char in password)
                    ss.AppendChar(_char);
                password = string.Empty;

                Session.LogIn(userName, ss);
            }
        }
    }
}
