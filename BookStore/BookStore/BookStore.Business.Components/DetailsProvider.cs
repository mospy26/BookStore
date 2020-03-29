using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Entities;

/*
 * New Details Provider that returns data related to the details page and allows rating a media
 * It includes recording purchases and rating media
 */
namespace BookStore.Business.Components
{
    public class DetailsProvider : IDetailsProvider
    {
        public Tuple<int, int> GetCountLikesAndDislikesForMedia(int pMediaId) 
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                return Tuple.Create(((from Rating in lContainer.Ratings.Include("Media").Include("User")
                        where Rating.Like == true && Rating.Media.Id == pMediaId
                        select Rating.User.Id).Count()),  
                        (from Rating in lContainer.Ratings.Include("Media").Include("User")
                        where Rating.Like == false && Rating.Media.Id == pMediaId
                        select Rating.User.Id).Count());
            }
        }
        public List<BookStore.Business.Entities.Media> GetRecommendedMedia(int pMediaId, int pUserId) 
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
        {
                var subQuery = (from Rating1 in lContainer.Ratings.Include("Media.Stocks").Include("User").Include("Media.Purchases").Include("Media.Ratings")
                                where Rating1.Media.Id == pMediaId && Rating1.Like == true
                                select Rating1.User.Id).ToList();

                var internalResult = (from Rating in lContainer.Ratings.Include("Media.Stocks").Include("User").Include("Media.Purchases").Include("Media.Ratings")
                                      where subQuery.Contains(Rating.User.Id) && Rating.Like == true && Rating.Media.Id != pMediaId && Rating.User.Id != pUserId
                                      select Rating.Media).ToList<Media>();

                return internalResult;
            }
        }


        public Rating GetRating(int pUserId, int pMediaId)
        {

            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                Rating internalResult = (from Rating in lContainer.Ratings.Include("Media").Include("User")
                                         where Rating.Media.Id == pMediaId && Rating.User.Id == pUserId
                                         select Rating).FirstOrDefault();

                return internalResult;
            }
        }

        /*
         * Adds a new book Purchase into the db
         */
        public void AddPurchase(Media pMedia, User pUser)
        {

            // Ensure duplicate rows do not exist for the same media purchase
            if (CheckIfPurchaseExistsForMedia(pMedia.Id, pUser.Id)) return;


            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                Purchase Purchase = new Purchase
                {
                    Media = pMedia,
                    User = pUser
                };

                lContainer.Users.Attach(pUser);
                lContainer.Media.Attach(pMedia);
                lContainer.Purchases.Add(Purchase);

                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        /*
         * Rates or re-rates a book if purchased
         */
        public void RateMedia(bool pLike, User pUser, Media pMedia)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                // Check if the user has actually purchased the book before rating it
                if (!CheckIfPurchaseExistsForMedia(pMedia.Id, pUser.Id)) return;

                var Rating = lContainer.Ratings.Include("User").Include("Media").SingleOrDefault(r => r.Media.Id == pMedia.Id && r.User.Id == pUser.Id);

                if (Rating != null)
                {
                    if (pLike == true)
                    {
                        LikeMedia(Rating);
                    }
                    else
                    {
                        DislikeMedia(Rating);
                    }
                }
                else
                {
                    Rating NewRating = new Rating
                    {
                        Like = pLike,
                        User = pUser,
                        Media = pMedia
                    };

                    lContainer.Users.Attach(pUser);
                    lContainer.Media.Attach(pMedia);
                    lContainer.Ratings.Add(NewRating);
                }

                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        private void LikeMedia(Rating pRating)
        {
            pRating.Like = true;

        }

        private void DislikeMedia(Rating pRating)
        {
            pRating.Like = false;

        }

        /*
         * Checks if a logged in user has purchased a media
         */
        public bool CheckIfPurchaseExistsForMedia(int pMediaId, int pUserId)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                return lContainer.Purchases.Include("User").Include("Media").SingleOrDefault(p => p.Media.Id == pMediaId && p.User.Id == pUserId) != null;
            }
        }
    }
}
