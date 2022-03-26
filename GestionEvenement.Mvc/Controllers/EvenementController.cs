using Microsoft.AspNetCore.Mvc;
using GestionEvenement.Domain.Assemblers;
using GestionEvenement.BusinessLogic.Services.Contracts;
using System;
using GestionEvenement.Mvc.Helpers;

namespace GestionEvenement.Areas.Admin.Controllers
{
    public class EvenementController : Controller
    {
        private readonly IEvenementService _evenementService;

        public EvenementController(IEvenementService evenementService) : base()
        {
            _evenementService = evenementService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddOrEdit(Guid? id)
        {
            EvenementDto evenementDto = new();
            if (id != null)
            {
                evenementDto = _evenementService.GetById(id.GetValueOrDefault());
                if (evenementDto == null)
                {
                    return NotFound();
                }
            }
            else
            {
                evenementDto.StartDateAndTime = DateTime.Now.Date;
                evenementDto.EndDateAndTime = DateTime.Now.Date;
            }
            return View(evenementDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(EvenementDto evenementDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (evenementDto.Id == Guid.Empty)
                    {
                        evenementDto.Id = Guid.NewGuid();
                        _evenementService.Add(evenementDto);
                    }
                    else
                    {
                        _evenementService.Update(evenementDto);
                    }
                    _evenementService.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw new Exception(MvcHelper.GetErrorMessages(ModelState));
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message.Replace("-", "\n");
                return View(evenementDto);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var evenements = _evenementService.GetAll();
            return Json(new { data = evenements });
        }

        public IActionResult Delete(Guid id)
        {
            try
            {
                var evenementDto = _evenementService.GetById(id);
                if (evenementDto == null)
                {
                    return Json(new { success = false, message = "Fail to delete" });
                }
                _evenementService.Delete(id);
                _evenementService.Save();
                return Json(new { success = true, message = "Delete successful" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Fail to delete : {ex.Message}" });
            }
        }
    }

}