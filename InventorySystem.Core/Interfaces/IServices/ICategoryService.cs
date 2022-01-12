using InventorySystem.Core.Entities;
using System.Collections.Generic;

namespace InventorySystem.Core.Interfaces.IServices
{
    public interface ICategoryService
    {
        Category GetCategory(int id);

        IEnumerable<Category> GetAllCategories();

        void UpsertCategory(Category entity);

        void DeleteCategory(Category entity);
    }
}
