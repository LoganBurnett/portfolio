using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CBTDWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public CategoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Category.GetAll() });
        }
    }
}
