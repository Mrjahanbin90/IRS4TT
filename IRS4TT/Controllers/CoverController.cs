using IRS4TT.Domains;
using IRS4TT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IRS4TT.Controllers
{
    public class CoverController : Controller
    {
        private readonly ICoverService _coverService;

        public CoverController(ICoverService coverService)
        {
            _coverService = coverService;
        }

        public ActionResult Index()
        {
            var model = _coverService.GetAll();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var cover = _coverService.GetById(id);
            if (cover == null)
                return NotFound();

            return View(cover);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cover cover)
        {
            if (ModelState.IsValid)
            {
                _coverService.Add(cover);
                return RedirectToAction(nameof(Index));
            }

            return View(cover);
        }

        public ActionResult Edit(int id)
        {
            var cover = _coverService.GetById(id);
            if (cover == null)
                return NotFound();

            return View(cover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cover cover)
        {
            if (id != cover.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _coverService.Update(cover);
                return RedirectToAction(nameof(Index));
            }

            return View(cover);
        }

        public ActionResult Delete(int id)
        {
            var cover = _coverService.GetById(id);
            if (cover == null)
                return NotFound();

            return View(cover);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _coverService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
