using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.Farmework.Domain.EntityDateTime
{
    public class ShamsiCurrentDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthTitle { get; set; }
        public string DayOfWeekTitle { get; set; }
        public string Description { get; set; }
    }
}
