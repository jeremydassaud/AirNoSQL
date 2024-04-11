using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using AirNoSQL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using FireSharp.Exceptions;

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
            FirebaseResponse response = client.Get("Ville/" + id);
            Ville? ville = JsonConvert.DeserializeObject<Ville>(response.Body);
            dynamic? data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            return View(ville);
        }

        // GET: VilleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VilleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ville ville)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
                PushResponse response = client.Push("Ville/" + ville.IdVille, ville);

                ville.IdVille = response.Result.name;
                SetResponse set = client.Set("Ville/" + ville.IdVille, ville);

                if(set.StatusCode == System.Net.HttpStatusCode.OK)
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

        // GET: VilleController/Edit/5
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Ville/" + id);
            Ville ville = JsonConvert.DeserializeObject<Ville>(response.Body);
            return View(ville);
        }

        // POST: VilleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Ville ville)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Ville/" + ville.IdVille, ville);
            return RedirectToAction("Index");
        }

        // GET: VilleController/Delete/5
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Ville/" + id);
            Ville ville = JsonConvert.DeserializeObject<Ville>(response.Body);
            return View(ville);
        }

        // POST: VilleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Ville ville)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Ville/" + id);
            return RedirectToAction("Index");
        }
    }
}
