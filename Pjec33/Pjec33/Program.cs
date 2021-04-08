using System;
using System.Threading;
using static System.Console;

namespace Pjec33
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine(DateTime.Now);
            WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
            WriteLine(DateTime.Now.ToString("HH:mm:ss"));
            WriteLine(DateTime.Now.DayOfWeek);

            TimeSpan ts = DateTime.Now - new DateTime(2000, 1, 1);
            WriteLine((int)ts.TotalDays);
            WriteLine((Decimal)ts.TotalSeconds);

            DateTime date_start = DateTime.Now;
            Thread.Sleep(2000);
            DateTime date_cur = DateTime.Now;
            WriteLine((date_cur - date_start).TotalMilliseconds);
        }
    }
}
