using KBA.Farmework.Domain;
using KBA.Domain.Entity.Products;

namespace KBA.Domain.Entity.Event
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
