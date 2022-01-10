using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StoreHouseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreHouseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var entity = new StoreHouse();
            if (id == null) return View(entity);
            entity = _unitOfWork.StoreHouseRepository.Get(id.GetValueOrDefault());
            if (entity == null) return NotFound();
            return View(entity);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _unitOfWork.StoreHouseRepository.GetAll();
            return Json(new { data = all });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(StoreHouse entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.Id == 0)
                {
                    _unitOfWork.StoreHouseRepository.Add(entity);
                }
                else
                {
                    _unitOfWork.StoreHouseRepository.Update(entity);
                }
                _unitOfWork.SavesChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.StoreHouseRepository.Get(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Hubo un error al eliminar el almacén." });
            }
            _unitOfWork.StoreHouseRepository.Remove(entity);
            _unitOfWork.SavesChanges();
            return Json(new {success = true, message = "Almacén eliminado exitosamente." });
        }

        #endregion

    }
}
