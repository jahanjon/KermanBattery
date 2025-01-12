using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.Farmework.Domain.EntityDateTime
{
    public class DateAndValueDto
    {
        public string Date { get; set; }
        public int Value { get; set; }
        public string DayName { get; set; }
        public byte DayIndex { get; set; }
    }
}
