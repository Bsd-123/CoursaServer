using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiServer.Controllers
{
    public class ProgressController : Controller
    {
        // GET: ProgressController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ProgressController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProgressController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProgressController/Create
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

        // GET: ProgressController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProgressController/Edit/5
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

        // GET: ProgressController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProgressController/Delete/5
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
