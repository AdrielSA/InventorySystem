using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.DataStore.Context;

namespace InventorySystem.DataStore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IStoreHouseRepository StoreHouseRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            StoreHouseRepository = new StoreHouseRepository(_context);
        }

        public void SavesChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null) _context.Dispose();
        }
    }
}
