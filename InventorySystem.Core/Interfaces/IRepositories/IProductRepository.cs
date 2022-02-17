using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.IRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        void Update(Product entity);
    }
}
