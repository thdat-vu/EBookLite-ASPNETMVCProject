using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Ultility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Areas.Admin.Controllers
{
    [Area("Admin")]
    //Authorization so that Just admin can access CategoryController
   // [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            
            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id) //UpdateandInsert
        {                    //if the id is existed => Update
            
            //if id is null or == 0 
            if (id == null || id == 0)
            {
                //Create
                return View(new Company());

            }
            else
            {
                //Update
                //Step1:retrieve
                Company company = _unitOfWork.Company.Get((System.Linq.Expressions.Expression<Func<Company, bool>>)(u => u.Id == id));
                //Step2: return view model
                return View(company);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            
            if (ModelState.IsValid) //if obj is valid
            {
                
                

                if (company.Id == 0)
                {
                    _unitOfWork.Company.Add(company); //Add Company to Company Table
                }
                else
                {
                    _unitOfWork.Company.Update(company);
                }

                
                _unitOfWork.Save(); //keep track on change
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index"); // after adding, return to previous action and reload the page
            }
            else
            {

               
                    
                
            return View(company); //return previous action + invalid object
            }
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? productFromDb = _unitOfWork.Company.Get(u => u.Id == id);
            //Company? productFromDb1 = _productRepo.Categories.FirstOrDefault(u=>u.Id== id);
            //Company? productFromDb2 = _productRepo.Categories.Where(u => u.Id == id).FirstOrDefault();

        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost]
        //public IActionResult Edit(Company obj)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Company edited successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? productFromDb = _unitOfWork.Company.Get(u => u.Id == id);


        //    if (productFromDb == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDb);
        //}
        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Company? obj = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Company deleted successfully";
        //    return RedirectToAction("Index");
        //}

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get((System.Linq.Expressions.Expression<Func<Company, bool>>)(u => u.Id == id));
            //catch exception if productToBeDeleted is null
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { succes = true, message = "Delete Successful" });
        }
        #endregion
    }
}
