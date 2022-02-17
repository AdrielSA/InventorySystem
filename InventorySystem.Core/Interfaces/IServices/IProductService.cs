using InventorySystem.Core.Entities;
using InventorySystem.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace InventorySystem.Core.Interfaces.IServices
{
    public interface IProductService
    {
        Product GetProduct(int id);

        IEnumerable<Product> GetAllProducts();

        void Upsert(Product entity);

        ProductViewModel FillViewModel(ProductViewModel entityVM);

        void DeleteProduct(Product entity);

        Product SaveImage(Product entity, HttpContext http);
    }
}
