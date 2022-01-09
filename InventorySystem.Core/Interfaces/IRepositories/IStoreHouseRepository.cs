using InventorySystem.Core.Entities;

namespace InventorySystem.Core.Interfaces.IRepositories
{
    public interface IStoreHouseRepository : IBaseRepository<StoreHouse>
    {
        void Update(StoreHouse entity);
    }
}
