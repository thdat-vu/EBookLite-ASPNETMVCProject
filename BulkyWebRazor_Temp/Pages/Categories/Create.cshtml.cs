using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{
    [BindProperties] //for more than 1 property
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //if we want property to be available when we post, have to add an attribute [bindproperty]
        //[BindProperty]
        public Category Category { get; set; }
        //creat ctor
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }
        //public IActionResult OnPost(Category obj)
        public IActionResult OnPost() //bind property will automatically be binded and available in the post handler
        {
            _db.Categories.Add(Category);//directly access Category property
            _db.SaveChanges();
            TempData["Success"] = "Category created successfully";
            return RedirectToPage("Index");
        } 
    }
}
