using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class Index_OriginalModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        public IEnumerable<Manufacturer> objManufacturerList;

        public Index_OriginalModel(UnitOfWork unitOfWork)  //dependency injection of the unit of work service
        {
            _unitOfWork = unitOfWork;
            objManufacturerList = new List<Manufacturer>();
        }

        public IActionResult OnGet()
        {
            objManufacturerList = _unitOfWork.Manufacturer.GetAll();
            return Page();
        }
    }
}
