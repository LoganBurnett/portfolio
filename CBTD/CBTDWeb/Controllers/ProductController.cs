using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace CBTDWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public ProductController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.Product.GetAll(null, null, "Category,Manufacturer") });
        }
    }
}
