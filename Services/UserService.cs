using WidgetCoUser.Models;
using WidgetCoUser.Repositories.Interfaces;
using WidgetCoUser.Services.Interfaces;
using System.Threading.Tasks;
using System.Linq;

namespace WidgetCoUser.Services
{
    public class UserService : Service<User>, IUserService
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = (await _repository.GetWhereAsync(u => u.Email == email)).FirstOrDefault();

            if (user == null || user.Password != password) //im not here to do proper security, just a simple example
            {
                return null;
            }

            return user;
        }
    }
}