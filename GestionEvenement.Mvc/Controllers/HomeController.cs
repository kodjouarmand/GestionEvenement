using System.Diagnostics;
using GestionEvenement.Domain.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace GestionEvenement.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(): base()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var error = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            var contextFeature = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                error.Message = contextFeature.Error.Message;
            }
            
            return View(error);
        }
    }
}
