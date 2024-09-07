using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CBTDWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public ManufacturerController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Manufacturer.GetAll() });
        }
    }
}
