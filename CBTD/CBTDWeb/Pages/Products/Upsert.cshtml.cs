using DataAccess;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CBTDWeb.Pages.Products
{
    public class UpsertModel : PageModel
    {
        private readonly UnitOfWork _unitOfWork;
        // Will give us where the wwwroot path on the server
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public Product objProduct { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> ManufacturerList { get; set; }

        public UpsertModel(UnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            objProduct = new Product();
            CategoryList = new List<SelectListItem>();
            ManufacturerList = new List<SelectListItem>();

            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            objProduct = new Product();
            CategoryList = _unitOfWork.Category.GetAll()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }
                );
            ManufacturerList = _unitOfWork.Manufacturer.GetAll()
                .Select(m => new SelectListItem
                {
                    Text = m.Name,
                    Value = m.Id.ToString()
                }
                );

            if (id == null || id == 0) //create mode
            {
                return Page();
            }

            //edit mode

            if (id != 0)  //retreive product from DB
            {
                objProduct = _unitOfWork.Product.GetById(id);
            }

            if (objProduct == null) //maybe nothing returned
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            // Gets the path for the wwwroot on the server
            string webRootPath = _webHostEnvironment.WebRootPath;
            // files is an array, in this case there is only one file so we access files[0]
            var files = HttpContext.Request.Form.Files;

            // Check if the product is new (create)
            if (objProduct.Id == 0)
            {
                // Check if any files were even uploaded
                if (files.Count > 0)
                {
                    // Create a unique id for each image name
                    string newFileName = Guid.NewGuid().ToString();

                    // The full path (location) to store the image
                    var uploadPath = Path.Combine(webRootPath, @"images\products\");

                    // Get the original file extension to preserve it
                    var extension = Path.GetExtension(files[0].FileName);

                    // Create the full path for the new uploaded (renamed) image
                    var fullPath = Path.Combine(uploadPath, newFileName + extension);

                    // Save the physical image to the server
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    // Set the URL path string to be saved in the database
                    objProduct.ImageUrl = @"\images\products\" + newFileName + extension;
                }

                // Add new product to the database
                _unitOfWork.Product.Add(objProduct);
                TempData["success"] = "Product Added Successfully";
            }
            else    // Existing Product we are editing
            {
                // Retreive the existing Product from the Database
                var objProductFromDb = _unitOfWork.Product.Get(p => p.Id == objProduct.Id);

                // Check if any files were even uploaded
                if (files.Count > 0)
                {
                    // Create a unique id for each image name
                    string newFileName = Guid.NewGuid().ToString();

                    // The full path (location) to store the image
                    var uploadPath = Path.Combine(webRootPath, @"images\products\");

                    // Get the original file extension to preserve it
                    var extension = Path.GetExtension(files[0].FileName);

                    // Delete the existing image associated with the product
                    if (objProductFromDb.ImageUrl != null)
                    {
                        var imagePath = Path.Combine(webRootPath, objProductFromDb.ImageUrl.TrimStart('\\'));
                        // If the image exists delete it
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    // Create the full path for the new uploaded (renamed) image
                    var fullPath = Path.Combine(uploadPath, newFileName + extension);

                    // Save the physical image to the server
                    using var fileStream = System.IO.File.Create(fullPath);
                    files[0].CopyTo(fileStream);

                    // Set the URL path string to be saved in the database
                    objProduct.ImageUrl = @"\images\products\" + newFileName + extension;
                }
                else    // No new files
                {
                    objProduct.ImageUrl = objProductFromDb.ImageUrl;
                }

                // Update the existing product
                _unitOfWork.Product.Update(objProduct);
                TempData["success"] = "Product Updated Successfully";
            }

            // Save the changes to the database
            _unitOfWork.Commit();

            // Redirect to the products Index Page
            return RedirectToPage("./Index");
        }

    }
}
