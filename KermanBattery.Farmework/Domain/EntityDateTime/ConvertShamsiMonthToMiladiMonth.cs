using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KermanBattery.Farmework.Domain.EntityDateTime
{
    public class ConvertShamsiMonthToMiladiMonth
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CalcDayDiff { get; set; }
        public int DayDiff { get; set; }
        public List<DateAndValueDto> AllShamsiDateInMonth { get; set; } = new();
    }
}
