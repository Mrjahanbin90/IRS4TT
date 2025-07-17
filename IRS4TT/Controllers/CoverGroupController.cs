using IRS4TT.Domains;
using IRS4TT.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IRS4TT.Controllers
{
    public class CoverGroupController : Controller
    {
        private readonly ICoverGroupService _coverGroupService;

        public CoverGroupController(ICoverGroupService coverGroupService)
        {
            _coverGroupService = coverGroupService;
        }

        public ActionResult Index()
        {
            var model = _coverGroupService.GetAll();
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var group = _coverGroupService.GetById(id);
            if (group == null)
                return NotFound();

            return View(group);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CoverGroup group)
        {

            _coverGroupService.Add(group);
            return RedirectToAction(nameof(Index));


            return View(group);
        }

        public ActionResult Edit(int id)
        {
            var group = _coverGroupService.GetById(id);
            if (group == null)
                return NotFound();

            return View(group);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CoverGroup group)
        {
            if (id != group.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _coverGroupService.Update(group);
                return RedirectToAction(nameof(Index));
            }

            return View(group);
        }

        public ActionResult Delete(int id)
        {
            var group = _coverGroupService.GetById(id);
            if (group == null)
                return NotFound();

            return View(group);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _coverGroupService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
