using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.Core.Interfaces.IServices;
using InventorySystem.Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InventorySystem.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public Product GetProduct(int id)
        {
            return _unitOfWork.ProductRepository.Get(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _unitOfWork.ProductRepository.GetAll(IncludProperties: "Category,Brand");
        }

        public void Upsert(Product entity)
        {
            if (entity.Id == 0)
            {
                _unitOfWork.ProductRepository.Add(entity);
            }
            else
            {
                _unitOfWork.ProductRepository.Update(entity);
            }
            _unitOfWork.SavesChanges();
        }

        public ProductViewModel FillViewModel(ProductViewModel entityVM)
        {
            entityVM.Categories = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            entityVM.Brands = _unitOfWork.BrandRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            entityVM.BaseList = _unitOfWork.ProductRepository.GetAll().Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = x.Id.ToString()
            });

            return entityVM;
        }

        public void DeleteProduct(Product entity)
        {
            // Delete image
            if (entity.ImageUrl != null)
            {
                var webRootPath = _hostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath, entity.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _unitOfWork.ProductRepository.Remove(entity);
            _unitOfWork.SavesChanges();
        }

        public Product SaveImage(Product entity, HttpContext http)
        {
            // Charge image
            var webRootPath = _hostEnvironment.WebRootPath;
            var files = http.Request.Form.Files;
            if (files.Count > 0)
            {
                var fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\products");
                var extension = Path.GetExtension(files[0].FileName);
                if (entity.ImageUrl != null)
                {
                    // Delete image
                    var imagePath = Path.Combine(webRootPath, entity.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                entity.ImageUrl = @"\images\products\" + fileName + extension;
            }
            else if (entity.Id != 0)
            {
                var productDB = _unitOfWork.ProductRepository.Get(entity.Id);
                entity.ImageUrl = productDB.ImageUrl;
            }
            return entity;
        }
    }
}
