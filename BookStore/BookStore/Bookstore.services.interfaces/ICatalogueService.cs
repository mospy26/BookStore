using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BookStore.Services.MessageTypes;

namespace BookStore.Services.Interfaces
{
    [ServiceContract]
    public interface ICatalogueService
    {
        [OperationContract]
        List<Media> GetMediaItems(int pOffset, int pCount);

        [OperationContract]
        Media GetMediaById(int pId);

        [OperationContract]
        void RateMedia(bool pLike, User pUser, Media pMedia);

        [OperationContract]
        Rating GetRating(int pUserId, int pMediaId);

        [OperationContract]
        Tuple<int, int> GetLikesAndDislikesForMedia(int pMediaId);

        [OperationContract]
        List<Media> GetRecommendedMedia(int pMediaId, int pUserId);

        [OperationContract]
        bool CheckIfPurchaseExistsForMedia(int pMediaId, int pUserId);

        [OperationContract]
        void AddPurchase(Media pMedia, User pUser);
    }
}
