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
    public static class AccountOperations
    {

        public static List<Account> GetAllAccounts()
        {
            using (DataEntity db = new DataEntity())
            {
                var result = db.Accounts.ToList();
                return result;
            }
        }

        public static async Task<OperationsResponse> CreateAccount(int userId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        Account acc = new Account
                        {
                            Name = "Новый пользователь",
                            Birthday = DateTime.Now,
                            Phone = "Ваш номер",
                            UserId = userId
                        };
                        db.Accounts.Add(acc);
                        db.SaveChanges();
                        return OperationsResponse.Ok;
                    }
                }
                catch (Exception e)
                {
                    return OperationsResponse.ServerError;
                }
            });
        }

        //public static async Task<OperationsResponse> CreateAccount(int userId, string name, string phone, DateTime birthday)
        //{
        //    return await Task.Run(() =>
        //    {
        //        try
        //        {
        //            using (DataEntity db = new DataEntity())
        //            {
        //                Account acc = new Account
        //                {
        //                    Name = name,
        //                    Birthday = birthday,
        //                    Phone = phone,
        //                    UserId = userId
        //                };
        //                db.Accounts.Add(acc);
        //                db.SaveChanges();
        //                return OperationsResponse.Ok;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            return OperationsResponse.ServerError;
        //        }
        //    });
        //}

        public static async Task<OperationsResponse> EditAccount(int userId, string name, string birthday, string phone)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        Account acc = db.Accounts.FirstOrDefault(a => a.UserId == userId);

                        if(acc != null)
                        {
                            acc.Name = name;
                            acc.Phone = phone;
                            acc.Birthday = DateTime.Parse(birthday);
                            db.SaveChanges();
                            return OperationsResponse.Ok;
                        }
                        return OperationsResponse.UserNotExists;   
                    }
                }
                catch (Exception e)
                {
                    return OperationsResponse.ServerError;
                }
            });
        }

    }
}