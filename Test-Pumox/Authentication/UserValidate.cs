using System;
using System.Linq;

namespace Test_Pumox.Authentication
{
    public class UserValidate
    {
        public static bool Login(string username, string password)
        {
            Users userBL = new Users();
            var UserLists = userBL.GetUsers();
            return UserLists.Any(user => user.UserName == username && user.Password == password);
        }
    }
}
