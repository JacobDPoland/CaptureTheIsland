using CaptureTheIsland.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CaptureTheIsland.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ResourceController : Controller
    {
        private ApplicationContext context { get; set; }

        public ResourceController(ApplicationContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Resource());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var resource = context.Resources.Find(id);
            return View(resource);
        }

        [HttpPost]
        public IActionResult Edit(Resource resource)
        {
            if (ModelState.IsValid)
            {
                if (resource.ResourceId == 0)
                {
                    context.Resources.Add(resource);
                }
                else
                {
                    context.Resources.Update(resource);
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (resource.ResourceId == 0) ? "Add" : "Edit";
                return View(resource);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var resource = context.Resources.Find(id);
            return View(resource);
        }

        [HttpPost]
        public IActionResult Delete(Resource resource)
        {
            if (resource != null)
            {
                context.Resources.Remove(resource);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
