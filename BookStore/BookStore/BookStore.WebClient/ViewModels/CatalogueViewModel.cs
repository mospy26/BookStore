﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Services.Interfaces;
using BookStore.Services.MessageTypes;

namespace BookStore.WebClient.ViewModels
{
    public class CatalogueViewModel
    {

        private ICatalogueService CatalogueService
        {
            get
            {
                return  ServiceFactory.Instance.CatalogueService;
            }
        }

        public List<Media> Items
        {
            get
            {
                return CatalogueService.GetMediaItems(0, Int32.MaxValue);
            }
        }

        public List<Media> RecommendedItems { get; set; }

        public Media GetMediaById(int id)
        {
            return CatalogueService.GetMediaById(id);
        }

        public Tuple<int, int> GetLikesAndDislikesForMedia(int pMediaId)
        {
            return CatalogueService.GetLikesAndDislikesForMedia(pMediaId);
        }

        public Rating GetRating(int pUserId, int pMediaId)
        {
            return CatalogueService.GetRating(pUserId, pMediaId);
        }

        public List<Media> GetRecommendedMedia(int pMediaId, int pUserId)
        {
            RecommendedItems = CatalogueService.GetRecommendedMedia(pMediaId, pUserId);
            return RecommendedItems;
        }
    }
}