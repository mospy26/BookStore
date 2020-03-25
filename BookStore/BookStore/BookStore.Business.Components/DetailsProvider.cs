﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Entities;

namespace BookStore.Business.Components
{
    public class DetailsProvider : IDetailsProvider
    {
        public Tuple<int, int> GetCountLikesAndDislikesForMedia(int pMediaId) 
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                return Tuple.Create(((from Rating in lContainer.Ratings.Include("Media").Include("User")
                        where Rating.Like == true && Rating.Medium.Id == pMediaId
                        select Rating.User.Id).Count()),  
                       (from Rating in lContainer.Ratings.Include("User").Include("User")
                        where Rating.Like == false && Rating.Medium.Id == pMediaId
                        select Rating.User.Id).Count());
            }
        }
        public List<BookStore.Business.Entities.Media> GetMediaLikedByUsersWhoLikedThisMedia(int pMediaId) 
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                var SubQuery = (from Rating1 in lContainer.Ratings.Include("Media").Include("User")
                                where Rating1.Medium.Id == pMediaId && Rating1.Like == true
                                select Rating1.User.Id).ToList();
                return (from Rating in lContainer.Ratings.Include("Media").Include("User")
                        where SubQuery.Contains(Rating.User.Id) && Rating.Like == true && Rating.Medium.Id != pMediaId
                        select Rating.Medium).ToList();
            }
        }


        public Rating GetRating(int pUserId, int pMediaId)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                return (from Rating in lContainer.Ratings.Include("Media").Include("User")
                        where Rating.Medium.Id == pMediaId && Rating.User.Id == pUserId
                        select Rating).Single();
            }
        }

        public void AddPurchase(Media pMedia, User pUser)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                Purchase Purchase = new Purchase
                {
                    Medium = pMedia,
                    User = pUser
                };

                lContainer.Purchases.Add(Purchase);

                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        public void RateMedia(bool pLike, User pUser, Media pMedia)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                // Check if the user has actually purchased the item
                    if (!CheckIfPurchaseExistsForMedia(pMedia.Id, pUser.Id))
                        throw new UnauthorizedAccessException();

                var Rating = lContainer.Ratings.Include("User").Include("Media").SingleOrDefault(r => r.Medium == pMedia && r.User == pUser);

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
                        Medium = pMedia
                    };
                    lContainer.Ratings.Add(NewRating);
                }

                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        private void LikeMedia(Entities.Rating pRating)
        {
            pRating.Like = true;

        }

        private void DislikeMedia(Entities.Rating pRating)
        {
            pRating.Like = false;

        }

        public bool CheckIfPurchaseExistsForMedia(int pMediaId, int pUserId)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                return lContainer.Purchases.Include("User").Include("Media").SingleOrDefault(p => p.Medium.Id == pMediaId && p.User.Id == pUserId) != null;
            }
        }
    }
}