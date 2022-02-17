using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.Core.Interfaces.IServices;
using System.Collections.Generic;

namespace InventorySystem.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Category GetCategory(int id)
        {
            return _unitOfWork.CategoryRepository.Get(id);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _unitOfWork.CategoryRepository.GetAll();
        }

        public void UpsertCategory(Category entity)
        {
            if (entity.Id == 0)
            {
                _unitOfWork.CategoryRepository.Add(entity);
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(entity);
            }
            _unitOfWork.SavesChanges();
        }

        public void DeleteCategory(Category entity)
        {
            _unitOfWork.CategoryRepository.Remove(entity);
            _unitOfWork.SavesChanges();
        }
    }
}
