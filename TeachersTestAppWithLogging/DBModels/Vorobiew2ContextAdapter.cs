using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersTestAppWithLogging.DBModels
{
    public class Vorobiew2ContextAdapter : IProjectDataSource
    {
        public async Task<int> AuthorizeUserInSystemAsync(string login, string password)
        {
            UserLogin? user;
            using (_dbContext = new Vorobiew2Context())
            {
                user = await Task.Run(() => _dbContext.UserLogins.FirstOrDefault(a => a.Login == login && a.Password == password));
            }
            if (user is null) return -1;
            return user.IdUser;
        }

        public async Task<UserDatum?> FindUserByIdAsync(int userID)
        {
            UserDatum? user;
            using (_dbContext = new Vorobiew2Context())
            {
                user = await Task.Run(() => _dbContext.UserData.Find(userID));
            }
            return user;
        }

        public UserDatum? FindUserByIdSync(int userID)
        {
            UserDatum? user;
            using (_dbContext = new Vorobiew2Context()) user = _dbContext.UserData.Find(userID);
            return user;
        }

        private Vorobiew2Context _dbContext;
    }
}
