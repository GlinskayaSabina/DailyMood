using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DailyMood.Models
{
    public class StatisticOperations
    {

        public static async Task<List<Statistic>> GetUserStatistics(int userId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        List<Statistic> list = db.Statistics.Where(s => s.UserId == userId).ToList();
                        return list.Count != 0 ? list : null;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
            );
        }

        public static async Task<OperationsResponse> AddNote(string history, int emoji, int userid)
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (DataEntity db = new DataEntity())
                    {
                        Statistic stat = new Statistic
                        {
                            History = history,
                            Emoji = emoji,
                            UserId = userid
                        };
                        db.Statistics.Add(stat);
                        db.SaveChanges();
                        return OperationsResponse.Ok;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return OperationsResponse.ServerError;
                }
            });
        }
    }
}
