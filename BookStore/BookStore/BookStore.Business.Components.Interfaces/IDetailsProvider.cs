using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Business.Entities;

namespace BookStore.Business.Components.Interfaces
{
    public interface IDetailsProvider
    {
        Tuple<int, int> GetCountLikesAndDislikesForMedia(int pMediaId);
        List<BookStore.Business.Entities.Media> GetMediaLikedByUsersWhoLikedThisMedia(int pMediaId, int pUserId);
        void RateMedia(bool pLike, User pUser, Media pMedia);
        void AddPurchase(Media pMedia, User pUser);
        Rating GetRating(int pUserId, int pMediaId);
        bool CheckIfPurchaseExistsForMedia(int pMediaId, int pUserId);
    }
}
