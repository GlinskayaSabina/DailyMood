using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMood.Models
{
    public class TestOperations
    {
        public static List<Test> GetAllTests()
        {
            using (DataEntity db = new DataEntity())
            {
                var result = db.Tests.ToList();
                return result;
            }
        }

        public static async Task<OperationsResponse> CreateTest(string value)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        Test test = new Test
                        {
                            Value = value
                        };
                        db.Tests.Add(test);
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

        public static async Task<Test> GetTestById(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        Test test = db.Tests.FirstOrDefault(t => t.Id == id);
                        return test;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            });
        }

        public static async Task<OperationsResponse> DeleteTest(int id)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        Test test = db.Tests.FirstOrDefault(t => t.Id == id);
                        db.Tests.Remove(test);
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
    }
}
