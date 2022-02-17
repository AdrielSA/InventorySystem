using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoreHouseController : Controller
    {
        private readonly IStoreHouseService _service;

        public StoreHouseController(IStoreHouseService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var entity = new StoreHouse();
            if (id == null) return View(entity);
            entity = _service.GetStoreHouse(id.Value);
            if (entity == null) return NotFound();
            return View(entity);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _service.GetAllStoreHouse();
            return Json(new { data = all });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(StoreHouse entity)
        {
            if (ModelState.IsValid)
            {
                _service.UpsertStoreHouse(entity);
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _service.GetStoreHouse(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Hubo un error al eliminar el almacén." });
            }
            _service.DeleteStoreHouse(entity);
            return Json(new {success = true, message = "Almacén eliminado." });
        }

        #endregion

    }
}
