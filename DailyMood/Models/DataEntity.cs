using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMood.Models
{
    public enum OperationsResponse
    {
        Ok,
        UserExists,
        UserNotExists,
        ServerError
    }

    public class DataEntity : DbContext
    {
        public DataEntity() : base("DailyMoodDataBase") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Statistic> Statistics { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Test> Tests { get; set; }
    }
}
