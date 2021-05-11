using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMood.Models
{
    public enum Emoji
    {
        None,
        Bad,
        Sad,
        Normal,
        Good,
        Great
    }

    public class Statistic
    {
        public int Id { get; set; }
        public string History { get; set; }
        public int Emoji { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
    }
}
