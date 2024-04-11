using AirNoSQL.Models;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirNoSQL.Controllers
{
    public class VolController : Controller
    {
        // Interface de manipulation de la BDD Firebase
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "4EJD4pkj2tACGEEWHTP8GsCw0j8fTVANpvDxF4UQ",
            BasePath = "https://airnosql-default-rtdb.firebaseio.com"
        };
        IFirebaseClient? client;

        // GET: VolController
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Vol");
            dynamic? data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Vol>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<Vol>(((JProperty)item).Value.ToString()));
                }
            }
            return View(list);
        }

        // GET: VolController/Details/5
        public ActionResult Details(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Ville/" + id);
            Vol? vol = JsonConvert.DeserializeObject<Vol>(response.Body);
            dynamic? data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            return View(vol);
        }

        // GET: VolController/Create
        public ActionResult Create()
        {
            ViewData["Ville"] = new SelectList(GetListAuteurs(), "Name", "Name");
            return View();
        }

        // POST: VolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vol vol)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                PushResponse response = client.Push("Vol/" + vol.IdVol, vol);

                vol.IdVol = response.Result.name;
                SetResponse set = client.Set("Vol/" + vol.IdVol, vol);

                if (set.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    ModelState.AddModelError("Ok", "Tout marche");
                }
                else
                {
                    ModelState.AddModelError("Error", "Tout ne marche pas");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
            }

            return RedirectToAction("Index");
        }

        // GET: VolController/Edit/5
        public ActionResult Edit(string id)
        {
            ViewData["Ville"] = new SelectList(GetListAuteurs(), "Name", "Name");
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Vol/" + id);
            Vol vol = JsonConvert.DeserializeObject<Vol>(response.Body);
            return View(vol);
        }

        // POST: VolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Vol vol)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Vol/" + vol.IdVol, vol);
            return RedirectToAction("Index");
        }

        // GET: VolController/Delete/5
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Vol/" + id);
            Vol vol = JsonConvert.DeserializeObject<Vol>(response.Body);
            return View(vol);
        }

        // POST: VolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Vol vol)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Vol/" + id);
            return RedirectToAction("Index");
        }

        private List<Ville> GetListAuteurs()
        {
            var list = new List<Ville>();

            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Ville");
            dynamic? data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<Ville>(((JProperty)item).Value.ToString()));
                }
            }
            return list;
        }
    }
}
