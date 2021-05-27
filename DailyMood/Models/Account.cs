using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMood.Models
{
    public class Account: IPrototypy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Years { get; set; }
        public string Telegram { get; set; }
        public string Role { get; set; }
        public int UserId { get; set; }

        public IPrototypy Clone(string name, int years, string telegram, int userid) 
        {
            return new Account { Name = name, Years = years, Telegram = telegram, Id = Id, Role = Role, UserId = userid };
        }
        public bool Status()
        {
            return true;
        }
    }
}
