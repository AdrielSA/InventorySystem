using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.Core.Interfaces.IServices;
using System.Collections.Generic;

namespace InventorySystem.Core.Services
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Brand GetBrand(int id)
        {
            return _unitOfWork.BrandRepository.Get(id);
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return _unitOfWork.BrandRepository.GetAll();
        }

        public void UpsertBrand(Brand entity)
        {
            if (entity.Id == 0)
            {
                _unitOfWork.BrandRepository.Add(entity);
            }
            else
            {
                _unitOfWork.BrandRepository.Update(entity);
            }
            _unitOfWork.SavesChanges();
        }

        public void DeleteBrand(Brand entity)
        {
            _unitOfWork.BrandRepository.Remove(entity);
            _unitOfWork.SavesChanges();
        }
    }
}
