using ElevenNote.Data;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            CategoryService service = new CategoryService(userId);
            var model = service.GetCategories();

            return View(model);
        }

        // Get: Create
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);

            if (service.CreateCategory(model))
            {
                // ViewBag.SaveResult = "Your category was created";
                TempData["SaveResult"] = "Your category was created";
                return (RedirectToAction("Index"));
            }
            else
            {
                ModelState.AddModelError("", "category could not be created");
            }
            return View();
        }


        public ActionResult Edit(int id)
        {
            var service = CreateCategoryService();
            var detail = service.GetCategoryById(id);
            var model =
                new Category
                {
                    CategoryId = detail.CategoryId,
                    CategoryType = detail.CategoryType,
                    CategoryEventType = detail.CategoryEventType,
                    CategoryOwner = detail.CategoryOwner,
                    CategoryDescription = detail.CategoryDescription
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int CategoryId, Category model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.CategoryId != CategoryId)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateCategoryService();

            if (service.UpdateCategory(model))
            {
                TempData["SaveResult"] = "Your category was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your category could not be updated.");
            return View(model);
        }


        public ActionResult Details(int id)
        {
            var svc = CreateCategoryService();
            var model = svc.GetCategoryById(id);

            return View(model);
        }



        public ActionResult Delete(int id)
        {
            var svc = CreateCategoryService();
            var model = svc.GetCategoryById(id);

            return View(model);
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateCategoryService();


            service.DeleteCategory(id);

            TempData["SaveResult"] = "Your Category was deleted";

            return RedirectToAction("Index");
        }


        public ActionResult NotesOfCategory(int id)
        {
            var service = CreateCategoryService();
            var model = service.GetNotesForCategory(id);
            var categoryObj = service.GetCategoryById(id);
            ViewBag.CategoryDesc = categoryObj.CategoryDescription;
            return View(model);
        }

        // Utility methods


        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CategoryService(userId);
            return service;
        }
    }
}