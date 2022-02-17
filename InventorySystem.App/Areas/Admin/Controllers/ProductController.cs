using InventorySystem.Core.Interfaces.IServices;
using InventorySystem.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var entityVM = new ProductViewModel();
            entityVM = _service.FillViewModel(entityVM);
            if (id == null) return View(entityVM);
            entityVM.Product = _service.GetProduct(id.Value);
            if (entityVM.Product == null) return NotFound();
            return View(entityVM);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _service.GetAllProducts();
            return Json(new { data = all });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel entityVM)
        {
            if (ModelState.IsValid)
            {
                entityVM.Product = _service.SaveImage(entityVM.Product, HttpContext);
                _service.Upsert(entityVM.Product);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                entityVM = _service.FillViewModel(entityVM);
                if (entityVM.Product.Id != 0)
                {
                    entityVM.Product = _service.GetProduct(entityVM.Product.Id);
                }
            }
            return View(entityVM.Product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _service.GetProduct(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Hubo un error al eliminar el producto." });
            }
            _service.DeleteProduct(entity);
            return Json(new { success = true, message = "Producto eliminado." });
        }

        #endregion
    }
}
