using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMood.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string HashedPassword { get; set; }
    }
}
