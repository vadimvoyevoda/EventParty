using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventsParty_WebApplication.Concrete
{
    public static class LogInErrors
    {
        public static string IncorrectLogin             { get { return "Incorrect Login"; } }
        public static string IncorrectPassword          { get { return "Incorrect Password"; } }
        public static string IncorrectLoginOrPassword   { get { return "Incorrect Login Or Password"; } }
        public static string AuthorizationError         { get { return "User not found"; } }
    }
}