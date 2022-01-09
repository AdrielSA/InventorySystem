using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.DataStore.Context;
using System.Linq;

namespace InventorySystem.DataStore.Repositories
{
    public class StoreHouseRepository : BaseRepository<StoreHouse>, IStoreHouseRepository
    {
        private readonly ApplicationDbContext _context;

        public StoreHouseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(StoreHouse entity)
        {
            var entityDb = _context.StoreHouses.FirstOrDefault(e => e.Id == entity.Id);
            if (entityDb != null)
            {
                entityDb.Name = entity.Name;
                entityDb.Description = entity.Description;
                entityDb.Status = entity.Status;

                _context.StoreHouses.Update(entityDb);
            }
        }
    }
}
