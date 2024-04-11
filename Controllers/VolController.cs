using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirNoSQL.Controllers
{
    public class VolController : Controller
    {
        // GET: VolController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VolController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VolController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VolController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
