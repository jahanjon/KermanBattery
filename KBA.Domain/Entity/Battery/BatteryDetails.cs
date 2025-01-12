using KBA.Domain.Entity.Products;
using KBA.Farmework.Domain;

namespace KBA.Domain.Entity.Battery
{
    public class BatteryDetails : BaseEntity
    {
        public string Description { get; private set; }
        public int Capacity { get; private set; }
        public string Manufacturer { get; private set; }

        #region Relations
        public ICollection<Product> Products { get; set; }
        #endregion

        #region Constructor
        public BatteryDetails(string description, int capacity, string manufacturer)
        {
            Description = description;
            Capacity = capacity;
            Manufacturer = manufacturer;
        }

        public void UpdateBatteryDetails(string description, int capacity, string manufacturer)
        {
            Description = description;
            Capacity = capacity;
            Manufacturer = manufacturer;
        }
        #endregion
    }
}
