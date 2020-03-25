using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.WebClient.ViewModels;

namespace BookStore.WebClient.Controllers
{
    public class DetailsController : Controller 
    {

        private CatalogueViewModel CatalogueViewModel
        {
            get
            {
                return new CatalogueViewModel();
            }
        }

        // GET: Rated
        public ActionResult Index(int? pMediaId, string pReturnUrl)
        {
            if (pMediaId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            return View(CatalogueViewModel.GetMediaById(pMediaId.Value));
        }
    }
}