using System;
using System.Collections.Generic;
using System.Text;

namespace vOS
{
    public class User
    {
        public string Name;
        public string Id;

        internal User() { }

        public static User CreateUser()
        {
            return new User();
        }
    }
}
