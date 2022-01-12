using InventorySystem.Core.Entities;
using System.Collections.Generic;

namespace InventorySystem.Core.Interfaces.IServices
{
    public interface IStoreHouseService
    {
        IEnumerable<StoreHouse> GetAllStoreHouse();

        StoreHouse GetStoreHouse(int id);

        void UpsertStoreHouse(StoreHouse entity);

        void DeleteStoreHouse(StoreHouse entity);
    }
}
