using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.IRepositories
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        void Update(Brand entity);
    }
}
