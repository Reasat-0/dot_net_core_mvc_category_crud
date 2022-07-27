using CRUD_MVC.Data;
using Microsoft.AspNetCore.Mvc;
using CRUD_MVC.Models;

using Microsoft.AspNetCore.Http;
using System.Data;
using ClosedXML.Excel;

namespace CRUD_MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> categoryList = _db.Categories;
            return View(categoryList);
        }
        //GET
        public IActionResult Create() {
            return View();
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)

        
        {

            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "Name and Display Order cannot match...");
            
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Update(int? id)
        {
            if (id == null || id == 0) {
                return NotFound();
            }

            var categoryFound = _db.Categories.Find(id);

            if (categoryFound == null) {
                return NotFound();
            }
            return View(categoryFound);
        }

        // Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Category obj)
        {

            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and Display Order cannot match...");

            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var categoryFound = _db.Categories.Find(id);

            if (categoryFound == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(categoryFound);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Export() {

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Id"),
                                        new DataColumn("Name"),
                                        new DataColumn("Display Order"),
                                        new DataColumn("Created At") });

            var categories = _db.Categories;

            foreach (var item in categories)
            {
                dt.Rows.Add(item.Id, item.Name, item.DisplayOrder, item.CreatedDateTime);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }


            /*return RedirectToAction("Index");*/
        }
    }
}
