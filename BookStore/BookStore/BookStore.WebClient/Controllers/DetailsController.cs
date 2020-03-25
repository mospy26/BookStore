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

        private DetailsViewModel DetailsViewModel { get; set; }
        // GET: Rated
        public ActionResult Index(int? pMediaId, string pReturnUrl)
        {
            if (pMediaId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            DetailsViewModel = new DetailsViewModel(pMediaId.Value);

            DetailsViewModel.GetMediaLikedByUsersWhoLikedThisMedia(pMediaId.Value);
            return View(DetailsViewModel.GetMediaById(pMediaId.Value));
        }
    }
}