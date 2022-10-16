using System.Collections.Generic;
using System.Threading.Tasks;
using Test_Pumox.Authentication;

namespace Test_Pumox.Services
{
    public interface IUserServices
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAllUsers();
    }
}
