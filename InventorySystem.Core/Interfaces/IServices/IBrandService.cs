using InventorySystem.Core.Entities;
using System.Collections.Generic;

namespace InventorySystem.Core.Interfaces.IServices
{
    public interface IBrandService
    {
        Brand GetBrand(int id);

        IEnumerable<Brand> GetAllBrands();

        void UpsertBrand(Brand entity);

        void DeleteBrand(Brand entity);
    }
}
