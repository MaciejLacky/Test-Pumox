using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Pumox.Authentication;

namespace Test_Pumox.Services
{
    public class UserServices : IUserServices
    {
        private List<User> _users = new List<User>
        {
            new User { ID = 1, UserName = "Pumox", Password = "123456" }
        };
        public async Task<User> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _users.SingleOrDefault(x => x.UserName == username && x.Password == password));
            if (user == null)
                return null;
            return user;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await Task.Run(() => _users);
        }
    }
}
