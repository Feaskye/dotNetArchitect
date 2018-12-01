using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchWeb.AppCode;
using ElasticSearchWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nest;

namespace ElasticSearchWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ElasticClient _client;
        public HomeController(IEsClientProvider esClientProvider)
        {
            _client = esClientProvider.GetClient();
        }


        // GET: Home
        public ActionResult Index(DataModel o)
        {
            _client.IndexDocument(o);
            return Content("Index");
        }

        [HttpPost]
        [Route("value/search")]
        public IReadOnlyCollection<DataModel> Search(string type)
        {
            return _client.Search<DataModel>(s => s
                .From(0)
                .Size(10)
                .Query(q => q.Match(m => m.Field(f => f.Type).Query(type)))).Documents;
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}