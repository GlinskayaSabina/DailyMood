using DailyMood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace WaterBalance.Models.DataOperations
{
    public static class UserOperations
    {
        /// <summary>
        /// Get user id by login
        /// </summary>
        /// <param name="login">User login</param>
        /// <returns>User id (found) or -1 (not found / server error)</returns>
        public static async Task<int> GetUserId(string login)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        User user = db.Users.SingleOrDefault(u => u.Login == login);
                        return user?.Id ?? -1;
                    }
                }
                catch (Exception)
                {
                    return -1;
                }
            }
            );
        }

        public static List<User> GetAllUsers()
        {
            using (DataEntity db = new DataEntity())
            {
                var result = db.Users.ToList();
                return result;
            }
        }

        public static async Task<OperationsResponse> CreateUser(string login, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        bool checkIsExist = db.Users.Count() != 0 && db.Users.Any(el => el.Login == login);
                        if (!checkIsExist)
                        {
                            User newUser = new User
                            {
                                Login = login,
                                HashedPassword = password
                            };
                            db.Users.Add(newUser);
                            db.SaveChanges();
                            return OperationsResponse.Ok;
                        }

                        return OperationsResponse.UserExists;
                    }
                }
                catch (Exception e)
                {
                    return OperationsResponse.ServerError;
                }
            });
        }

        public static async Task<OperationsResponse> CheckAuthorization(string login, string password)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        List<User> list = GetAllUsers();
                        bool authorizated = false;

                        list.ForEach(user =>
                        {
                            if (user.Login == login)
                            {
                                if (BCrypt.Net.BCrypt.Verify(password, user.HashedPassword)) authorizated = true;
                            }
                        });

                        return authorizated ? OperationsResponse.Ok : OperationsResponse.UserNotExists;
                    }
                }
                catch (Exception)
                {
                    return OperationsResponse.ServerError;
                }

            });
        }
    }
}