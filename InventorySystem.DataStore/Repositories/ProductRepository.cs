using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.DataStore.Context;
using System.Linq;

namespace InventorySystem.DataStore.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product entity)
        {
            var entityDb = _context.Products.FirstOrDefault(e => e.Id == entity.Id);
            if (entityDb != null)
            {
                if (entity.ImageUrl != null) entityDb.ImageUrl = entity.ImageUrl;
                if (entity.BaseId == 0)
                {
                    entityDb.BaseId = null;
                }
                else
                {
                    entityDb.BaseId = entity.BaseId;
                }
                entityDb.SerialNumber = entity.SerialNumber;
                entityDb.Description = entity.Description;
                entityDb.Price = entity.Price;
                entityDb.Cost = entity.Cost;
                entityDb.IdCategory = entity.IdCategory;
                entityDb.IdBrand = entity.IdBrand;

                _context.Products.Update(entityDb);
            }
        }
    }
}
