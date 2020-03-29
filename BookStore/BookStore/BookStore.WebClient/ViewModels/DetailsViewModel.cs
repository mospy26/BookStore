using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Services.Interfaces;
using BookStore.Services.MessageTypes;


/* 
 * 
 * View Model for the new Details Page
 * Returns details related data from database and stores rating in the database
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
            RecommendedMedia = GetRecommendedMedia(pMediaId, pUserId);
            CurrentMedia = GetMediaById(pMediaId);
            HasPurchased = HasPurchasedMedia(pMediaId, pUserId);
            RatingForBook = GetRating(pUserId, pMediaId);
            Refresh(pMediaId, pUserId);
        }

        public Media GetMediaById(int id)
        {
            return CatalogueService.GetMediaById(id);
        }

        /*
         * Retrieves likes and dislikes for a given media as a tuple
         */
        public Tuple<int, int> GetLikesAndDislikesForMedia(int pMediaId)
        {
            return CatalogueService.GetLikesAndDislikesForMedia(pMediaId);
        }

        /*
         * Gets the looged in user's rating for a media
         */
        public Rating GetRating(int pUserId, int pMediaId)
        {
            return CatalogueService.GetRating(pUserId, pMediaId);
        }

        /*
         * Gets a list of media that users liked who liked the given media liked
         */
        public List<Media> GetRecommendedMedia(int pMediaId, int pUserId)
        {
            RecommendedMedia = CatalogueService.GetRecommendedMedia(pMediaId, pUserId);
            return RecommendedMedia;
        }

        /*
         * Checks if the logged in user has purchased a given media
         */
        public bool HasPurchasedMedia(int pMediaId, int pUserId)
        {
            return CatalogueService.CheckIfPurchaseExistsForMedia(pMediaId, pUserId);
        }

        /*
         * Allows a logged in user to rate a media
         */
        public void RateMedia(bool pLike, int pMediaId, User pUser)
        {
            CatalogueService.RateMedia(pLike, pUser, CatalogueService.GetMediaById(pMediaId));
        }

        /*
         * Refreshes display data after a rate is made
         */
        public void Refresh(int pMediaId, int pUserId)
        {
            Tuple<int, int> LikesAndDislikes = GetLikesAndDislikesForMedia(pMediaId);
            Likes = LikesAndDislikes.Item1;
            Dislikes = LikesAndDislikes.Item2;
            RecommendedMedia = GetRecommendedMedia(pMediaId, pUserId);
        }
    }
}