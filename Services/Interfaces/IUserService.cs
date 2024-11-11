using WidgetCoUser.Models;
using System.Threading.Tasks;

namespace WidgetCoUser.Services.Interfaces
{
    public interface IUserService : IService<User>
    {
        public Task<User> AuthenticateAsync(string email, string password);
    }
}