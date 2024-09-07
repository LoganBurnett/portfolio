using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CBTDWeb.Pages.Manufacturers
{
    public class DeleteModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;

        [BindProperty]  //synchronizes form fields with values in code behind
        public Manufacturer objManufacturer { get; set; }

        public DeleteModel(UnitOfWork unitOfWork)  //dependency injection
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            objManufacturer = new Manufacturer();

            if (id != 0)
            {
                objManufacturer = _unitOfWork.Manufacturer.GetById(id);
            }

            if (objManufacturer == null)  //nothing found in DB
            {
                return NotFound();   //built in page
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Manufacturer.Delete(objManufacturer);
            TempData["success"] = "Manufacturer Deleted Successfully";
            _unitOfWork.Commit();  // Saves the changes to the db

            return RedirectToPage("./Index");
        }
    }
}
