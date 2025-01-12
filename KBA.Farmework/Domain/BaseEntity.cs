using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.Farmework.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public BaseEntity()
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            IsDeleted = false; 
        }
    }
}
