using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        void Update(Category entity);
    }
}
