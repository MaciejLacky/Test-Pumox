using System.Collections.Generic;

namespace Test_Pumox.Authentication
{
    public class Users
    {
        public List<User> GetUsers()
        {
            List<User> userList = new List<User>();
            userList.Add(new User()
            {
                ID = 1,
                UserName = "Pumox",
                Password = "123456"
            });
            
            return userList;
        }
    }
}
