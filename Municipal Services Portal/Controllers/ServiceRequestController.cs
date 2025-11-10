using Microsoft.AspNetCore.Mvc;

namespace Municipal_Services_Portal.Controllers
{
    public class ServiceRequestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
