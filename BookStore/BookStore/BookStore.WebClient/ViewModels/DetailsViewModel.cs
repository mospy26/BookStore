using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Services.Interfaces;
using BookStore.Services.MessageTypes;


/* 
 * 
 * View Model for the new Details Page
 * Returns data from database and stores rating in the database
 * 
 */
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
        public bool HasPurchased { get; set; }
        public Rating RatingForBook { get; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }



        public DetailsViewModel(int pMediaId, int pUserId)
        {
            RecommendedMedia = GetMediaLikedByUsersWhoLikedThisMedia(pMediaId);
            CurrentMedia = GetMediaById(pMediaId);
            HasPurchased = HasPurchasedMedia(pMediaId, pUserId);
            RatingForBook = GetRating(pUserId, pMediaId);
            RefreshLikesAndDislikesForMedia(pMediaId);
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

        public bool HasPurchasedMedia(int pMediaId, int pUserId)
        {
            return CatalogueService.CheckIfPurchaseExistsForMedia(pMediaId, pUserId);
        }

        public void RateMedia(bool pLike, int pMediaId, User pUser)
        {
            CatalogueService.RateMedia(pLike, pUser, CatalogueService.GetMediaById(pMediaId));
        }

        public void RefreshLikesAndDislikesForMedia(int pMediaId)
        {
            Tuple<int, int> LikesAndDislikes = GetLikesAndDislikesForMedia(pMediaId);
            Likes = LikesAndDislikes.Item1;
            Dislikes = LikesAndDislikes.Item2;
        }
    }
}