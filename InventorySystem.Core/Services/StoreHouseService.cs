using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.Core.Interfaces.IServices;
using System.Collections.Generic;

namespace InventorySystem.Core.Services
{
    public class StoreHouseService : IStoreHouseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreHouseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<StoreHouse> GetAllStoreHouse()
        {
            return _unitOfWork.StoreHouseRepository.GetAll();
        }

        public StoreHouse GetStoreHouse(int? id)
        {
            return _unitOfWork.StoreHouseRepository.Get(id.GetValueOrDefault());
        }

        public void AddStoreHouse(StoreHouse entity)
        {
            _unitOfWork.StoreHouseRepository.Add(entity);
            _unitOfWork.SavesChanges();
        }

        public void UpdateStoreHouse(StoreHouse entity)
        {
            _unitOfWork.StoreHouseRepository.Update(entity);
            _unitOfWork.SavesChanges();
        }

        public void DeleteStoreHouse(StoreHouse entity)
        {
            _unitOfWork.StoreHouseRepository.Remove(entity);
            _unitOfWork.SavesChanges();
        }
    }
}
