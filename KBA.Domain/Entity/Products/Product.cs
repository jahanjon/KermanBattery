using KBA.Farmework.Domain;
using KBA.Domain.Entity.Battery;
using KBA.Domain.Entity.Event;
using KBA.Domain.Entity.SellerAgg;

namespace KBA.Domain.Entity.Products
{
    public class Product : BaseEntity
    {
        public string SerialNumber { get; private set; }
        public int ProductEventTypeId { get; private set; }
        public int SellerId { get; private set; }
        public Seller Seller { get; private set; }
        public DateTime SaleDate { get; private set; }
        public DateTime? ActivationDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool IsActive { get; private set; }
        public int ProductBatteryDetailsId { get; private set; }

        public BatteryDetails BatteryDetails { get; private set; }
        public EventType EventType { get; private set; }

        #region Constructor
        public Product(string serialNumber, int productEventTypeId, int sellerId, DateTime saleDate)
        {
            SerialNumber = serialNumber;
            ProductEventTypeId = productEventTypeId;
            SellerId = sellerId;
            SaleDate = saleDate;
            IsActive = false;
        }

        public void Activate(DateTime activationDate)
        {
            ActivationDate = activationDate;
            IsActive = true;
        }
        public void SetBatteryDetails(int batteryDetailsId)
        {
            ProductBatteryDetailsId = batteryDetailsId;
        }
        #endregion
    }
}
