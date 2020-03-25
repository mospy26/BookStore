using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Services.Interfaces;
using BookStore.Services.MessageTypes;

namespace BookStore.WebClient.ViewModels
{
    public class DetailsViewModel
    {

        public ICatalogueService CatalogueService
        {
            get
            {
                return ServiceFactory.Instance.CatalogueService;
            }
        }

        public List<Media> RecommendedMedia { get; set; }
        public Media CurrentMedia { get; set; }


        public DetailsViewModel(int pMediaId)
        {
            RecommendedMedia = GetMediaLikedByUsersWhoLikedThisMedia(pMediaId);
            CurrentMedia = GetMediaById(pMediaId);
        }

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

        public List<Media> GetMediaLikedByUsersWhoLikedThisMedia(int pMediaId)
        {
            RecommendedMedia = CatalogueService.GetMediaLikedByUsersWhoLikedThisMedia(pMediaId);
            return RecommendedMedia;
        }
    }
}