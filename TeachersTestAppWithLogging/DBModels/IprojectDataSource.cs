﻿using System;
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
        /// Метод получения пользователя по id асинхронный
        /// </summary>
        /// <param name="userID">id пользователя</param>
        /// <returns>пользователь</returns>
        Task<UserDatum?> FindUserByIdAsync(int userID);

        /// <summary>
        /// Метод получения пользователя по id
        /// </summary>
        /// <param name="userID">id пользователя</param>
        /// <returns>пользователь</returns>
        UserDatum? FindUserByIdSync(int userID);

        /// <summary>
        /// Метод получения списка гендеров пользователей
        /// </summary>
        /// <returns>Список гендеров</returns>
        List<UserGender> GetAllGenders();

        /// <summary>
        /// Метод для получения списка курсов
        /// </summary>
        /// <returns>Список курсов</returns>
        List<Cource> GetAllCources();

        /// <summary>
        /// Метод для получения роли пользователя
        /// </summary>
        /// <param name="userID">Номер пользователя</param>
        /// <returns>Роль пользователя</returns>
        UserRole GetUserRole(int userID);

        /// <summary>
        /// Метод для обновлении информации о пользователе
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Статус добавления</returns>
        bool UpdateUser(UserDatum user);
    }
}
