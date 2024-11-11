using WidgetCoUser.Repositories.Interfaces;
using WidgetCoUser.Models;

namespace WidgetCoUser.Repositories
{
    public class UserRepository : Repository<WidgetCoUser.Models.User>
    {
        public UserRepository(CosmosContext dbContext) : base(dbContext) //feelsgoodman
        {
        }
    }
}