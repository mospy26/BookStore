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
        public ActionResult Index(int? pMediaId, string pReturnUrl, UserCache pUserCache)
        {
            if (pMediaId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            DetailsViewModel = new DetailsViewModel(pMediaId.Value, pUserCache.Model.Id);

            DetailsViewModel.GetMediaLikedByUsersWhoLikedThisMedia(pMediaId.Value);
            return View(DetailsViewModel);
        }

        public ActionResult LikeBook(bool pLike, int pMediaId, string pReturnUrl, UserCache pUserCache)
        {
            DetailsViewModel = new DetailsViewModel(pMediaId, pUserCache.Model.Id);
            DetailsViewModel.RateMedia(pLike, pMediaId, pUserCache.Model);
            return RedirectToAction("Index", new { pMediaId,  pReturnUrl, pUserCache.Model });
        }
    }
}