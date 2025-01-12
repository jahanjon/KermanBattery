
namespace KBA.Domain.Entity.SellerAgg
{
    public interface ISellerRepository
    {
        bool VerifyHashedPassword(string hashedPassword, string password);
        string HashPassword(string password);
        
    }
}
