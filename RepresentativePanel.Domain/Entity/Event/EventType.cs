using KermanBattery.Farmework.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KermanBatterySeller.Domain.Entity.Products;

namespace KermanBatterySeller.Domain.Entity.Event
{
    public class EventType : BaseEntity
    {
        public string Name { get; private set; }
        public bool IsVisibleForSeller { get; private set; }

        #region Relations
        public ICollection<Product> Products { get; set; }
        #endregion

        #region Constructor
        public EventType(string name, bool isVisibleForSeller)
        {
            Name = name;
            IsVisibleForSeller = isVisibleForSeller;
        }
        #endregion
    }
}
