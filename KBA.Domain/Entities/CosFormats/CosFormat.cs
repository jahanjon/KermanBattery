using KBA.Farmework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.Domain.Entities.CosFormats
{
    public class CosFormat :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        //public List<AssemblySerial> AssemblySerialsCos { get; set; }
    }
}
