using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.DataStore.Context;
using System.Linq;

namespace InventorySystem.DataStore.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category entity)
        {
            var entityDb = _context.Categories.FirstOrDefault(e => e.Id == entity.Id);
            if (entityDb != null)
            {
                entityDb.Name = entity.Name;
                entityDb.Status = entity.Status;

                _context.Categories.Update(entityDb);
            }
        }
    }
}
