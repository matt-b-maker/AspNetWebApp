using LearningAspNetWebApp.Data;
using LearningAspNetWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningAspNetWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> categoriesList = _db.Categories;
            return View(categoriesList);
        }

        //Get is the default method here.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category obj)
        {
            //TODO Server Side Validations
            //TODO Custom validatons/error messages. Use the name of the property to assign a special message to the error
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("ValuesSameError", "The display order cannot match the name");
            }

            if (obj.Name is null or "") ModelState.AddModelError("Name", "Name cannot be empty");
            //TODO server side validations "ModelState.IsValid"
            if (!ModelState.IsValid) return View(obj);
            _db.Categories.Add(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Get 
        public IActionResult Edit(int? id)
        {
            if (id is null or 0) return NotFound();
            //TODO three ways to retrieve a row from the db using the id and LINQ
            var categoryFromDb = _db.Categories.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
            if (categoryFromDb is null) return NotFound();
            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category obj)
        {
            //TODO Server Side Validations
            //TODO Custom validatons/error messages. Use the name of the property to assign a special message to the error
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("ValuesSameError", "The display order cannot match the name");
            }

            if (obj.Name is null or "") ModelState.AddModelError("Name", "Name cannot be empty");
            //TODO server side validations "ModelState.IsValid"
            if (!ModelState.IsValid) return View(obj);
            _db.Categories.Update(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Get 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null or 0) return NotFound();
            //TODO three ways to retrieve a row from the db using the id and LINQ
            var categoryFromDb = await _db.Categories.FindAsync(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(x => x.Id == id);
            if (categoryFromDb is null) return NotFound();
            _db.Categories.Remove(categoryFromDb);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Post
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategoryItem(int? id)
        {
            //TODO Server Side Validations
            //TODO Custom validatons/error messages. Use the name of the property to assign a special message to the error
            var categoryToDelete = await _db.Categories.FindAsync(id);
            if (categoryToDelete is null) return NotFound();
            _db.Categories.Remove(categoryToDelete);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
