using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersTestAppWithLogging.DBModels
{
    public interface IProjectDataSource
    {
        /// <summary>
        /// Метод авторизации пользователя в системе
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns>Id пользователя в системе, если авторизация прошла успешно</returns>
        Task<int> AuthorizeUserInSystemAsync(string login, string password);

        /// <summary>
        /// Метод получения пользователя по id
        /// </summary>
        /// <param name="userID">id пользователя</param>
        /// <returns>пользователь</returns>
        Task<UserDatum?> FindUserByIdAsync(int userID);
    }
}
