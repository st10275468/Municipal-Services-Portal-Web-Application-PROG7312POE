using Microsoft.AspNetCore.Mvc;

namespace Municipal_Services_Portal.Controllers
{
    public class ServiceRequestController : Controller
    {
        public IActionResult ServiceRequest()
        {
            return View();
        }
    }
}
