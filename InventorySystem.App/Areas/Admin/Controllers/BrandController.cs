using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _service;

        public BrandController(IBrandService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var entity = new Brand();
            if (id == null) return View(entity);
            entity = _service.GetBrand(id.Value);
            if (entity == null) return NotFound();
            return View(entity);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _service.GetAllBrands();
            return Json(new { data = all });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Brand entity)
        {
            if (ModelState.IsValid)
            {
                _service.UpsertBrand(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _service.GetBrand(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Hubo un error al eliminar la marca." });
            }
            _service.DeleteBrand(entity);
            return Json(new { success = true, message = "Marca eliminada." });
        }

        #endregion
    }
}
