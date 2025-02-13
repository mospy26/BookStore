﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.WebClient.ViewModels;

namespace BookStore.WebClient.Controllers
{
    /*
     * Details Controller for the details page
     * 
     */
    public class DetailsController : Controller 
    {

        private DetailsViewModel DetailsViewModel { get; set; }

        public ActionResult Index(int? pMediaId, string pReturnUrl, UserCache pUserCache)
        {
            if (pMediaId == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (DetailsViewModel == null) DetailsViewModel = new DetailsViewModel(pMediaId.Value, pUserCache.Model.Id);
            else DetailsViewModel.Refresh(pMediaId.Value, pUserCache.Model.Id);

            return View(DetailsViewModel);
        }

        /*
         * Rate a book based on thumbs up/down button in views
         */
        public ActionResult RateBook(bool pLike, int pMediaId, string pReturnUrl, UserCache pUserCache)
        {
            if (DetailsViewModel == null) DetailsViewModel = new DetailsViewModel(pMediaId, pUserCache.Model.Id);
            DetailsViewModel.RateMedia(pLike, pMediaId, pUserCache.Model);

            return RedirectToAction("Index", new { pMediaId, pReturnUrl, pUserCache.Model });
        }
    }
}