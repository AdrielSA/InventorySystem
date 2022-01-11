using InventorySystem.Core.Interfaces.IRepositories;
using InventorySystem.Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace InventorySystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var entityVM = new ProductViewModel()
            {
                Product = new(),
                Categories = _unitOfWork.CategoryRepository.GetAll().Select(x => new SelectListItem 
                { 
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                Brands = _unitOfWork.BrandRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                BaseList = _unitOfWork.ProductRepository.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Description,
                    Value = x.Id.ToString()
                })
            };
            if (id == null) return View(entityVM);
            entityVM.Product = _unitOfWork.ProductRepository.Get(id.GetValueOrDefault());
            if (entityVM.Product == null) return NotFound();
            return View(entityVM);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _unitOfWork.ProductRepository.GetAll(IncludProperties: "Category,Brand");
            return Json(new { data = all });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel entityVM)
        {
            if (ModelState.IsValid)
            {
                // Charge image
                var webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\products");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (entityVM.Product.ImageUrl != null)
                    {
                        // Delete image
                        var imagePath = Path.Combine(webRootPath, entityVM.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    entityVM.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                else
                {
                    if (entityVM.Product.Id != 0)
                    {
                        var productDB = _unitOfWork.ProductRepository.Get(entityVM.Product.Id);
                        entityVM.Product.ImageUrl = productDB.ImageUrl;
                    }
                }

                if (entityVM.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(entityVM.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(entityVM.Product);
                }
                _unitOfWork.SavesChanges();
                return RedirectToAction(nameof(Index));
            }
            else
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
                if (entityVM.Product.Id != 0)
                {
                    entityVM.Product = _unitOfWork.ProductRepository.Get(entityVM.Product.Id);
                }
            }
            return View(entityVM.Product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.ProductRepository.Get(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Hubo un error al eliminar el producto." });
            }

            // Delete image
            var webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, entity.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _unitOfWork.ProductRepository.Remove(entity);
            _unitOfWork.SavesChanges();
            return Json(new { success = true, message = "Producto eliminado exitosamente." });
        }

        #endregion
    }
}
