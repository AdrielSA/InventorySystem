using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.DataStore.Context;
using System.Linq;

namespace InventorySystem.DataStore.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Brand entity)
        {
            var entityDb = _context.Brands.FirstOrDefault(e => e.Id == entity.Id);
            if (entityDb != null)
            {
                entityDb.Name = entity.Name;
                entityDb.Status = entity.Status;

                _context.Brands.Update(entityDb);
            }
        }
    }
}
