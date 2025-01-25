using KBA.Farmework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBA.SellerApplication.Contract.CosFormat.ViewModel
{
    public class CosFormatViewModel :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
