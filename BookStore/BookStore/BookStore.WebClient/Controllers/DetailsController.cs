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

        public ActionResult LikeBook(int pMediaId, string pReturnUrl, UserCache pUserCache)
        {
            return RedirectToAction("Index", new { pMediaId,  pReturnUrl, pUserCache.Model });
        }
    }
}