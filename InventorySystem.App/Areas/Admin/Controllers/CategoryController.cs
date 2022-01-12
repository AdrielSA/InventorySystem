using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var entity = new Category();
            if (id == null) return View(entity);
            entity = _service.GetCategory(id.Value);
            if (entity == null) return NotFound();
            return View(entity);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _service.GetAllCategories();
            return Json(new { data = all });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category entity)
        {
            if (ModelState.IsValid)
            {
                _service.UpsertCategory(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _service.GetCategory(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Hubo un error al eliminar la categoria." });
            }
            _service.DeleteCategory(entity);
            return Json(new { success = true, message = "Categoria eliminada." });
        }

        #endregion
    }
}
