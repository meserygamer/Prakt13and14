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
            using (_dbContext = new Vorobiew2Context()) user = _dbContext.UserData
                                                                         .Include(a => a.IdUserNavigation)
                                                                         .Include(a => a.IdGenderNavigation)
                                                                         .Where(a => a.IdUser == userID)
                                                                         .FirstOrDefault();
            return user;
        }

        public List<UserGender> GetAllGenders()
        {
            List<UserGender> userGenders;
            using (_dbContext = new Vorobiew2Context()) userGenders = _dbContext.UserGenders.ToList();
            return userGenders;
        }

        public List<Cource> GetAllCources()
        {
            List<Cource> cources;
            Vorobiew2Context _dbContextForThis;
            using (_dbContextForThis = new Vorobiew2Context())
            {
                cources = _dbContextForThis.Cources
                                           .Include(a => a.TeacherCources)
                                           .ToList();
            }
            return cources;
        }

        public UserRole GetUserRole(int userID)
        {
            UserRole role;
            Vorobiew2Context _dbContextForThis;
            using (_dbContextForThis = new Vorobiew2Context()) role = _dbContextForThis.UserRoles
                                                                                       .Where(a => (a.UserLogins
                                                                                                     .Where(a => a.IdUser == userID))
                                                                                                     .LongCount() == 1)
                                                                                       .First();
            return role;
        }

        public bool UpdateUser(UserDatum user)
        {
            using(Vorobiew2Context DB = new Vorobiew2Context())
            {
                DB.Entry(user).State = EntityState.Modified;
                DB.Entry(user.IdUserNavigation).State = EntityState.Modified;
                DB.Entry(user.IdGenderNavigation).State = EntityState.Modified;
                DB.SaveChanges();
            }
            return true;
        }

        private Vorobiew2Context _dbContext;
    }
}
