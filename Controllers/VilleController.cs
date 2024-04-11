using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AirNoSQL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirNoSQL.Controllers
{
    public class VilleController : Controller
    {
        // Interface de manipulation de la BDD Firebase
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "4EJD4pkj2tACGEEWHTP8GsCw0j8fTVANpvDxF4UQ",
            BasePath = "https://airnosql-default-rtdb.firebaseio.com"
        };
        IFirebaseClient? client;

        // GET: VilleController
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Ville");
            dynamic? data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Ville>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<Ville>(((JProperty)item).Value.ToString()));
                }
            }
            return View(list);
        }

        // GET: VilleController/Details/id
        public ActionResult Details(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Ville");
            return View();
        }

        // GET: VilleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VilleController/Create
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

        // GET: VilleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VilleController/Edit/5
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

        // GET: VilleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VilleController/Delete/5
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
