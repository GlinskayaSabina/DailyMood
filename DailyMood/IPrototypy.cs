using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyMood
{
    public interface IPrototypy
    {
        bool Status();
        IPrototypy Clone(string name, int years, string telegram, int userid);
    }
}
