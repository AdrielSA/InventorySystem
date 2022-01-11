using InventorySystem.Core.Entities;
using InventorySystem.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace InventorySystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var entity = new Brand();
            if (id == null) return View(entity);
            entity = _unitOfWork.BrandRepository.Get(id.GetValueOrDefault());
            if (entity == null) return NotFound();
            return View(entity);
        }

        #region API

        [HttpGet]
        public IActionResult GetAll()
        {
            var all = _unitOfWork.BrandRepository.GetAll();
            return Json(new { data = all });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Brand entity)
        {
            if (ModelState.IsValid)
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
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var entity = _unitOfWork.BrandRepository.Get(id);
            if (entity == null)
            {
                return Json(new { success = false, message = "Hubo un error al eliminar la marca." });
            }
            _unitOfWork.BrandRepository.Remove(entity);
            _unitOfWork.SavesChanges();
            return Json(new { success = true, message = "Marca eliminada exitosamente." });
        }

        #endregion
    }
}
