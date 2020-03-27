using BookStore.Business.Components.Interfaces;
using BookStore.Services.Interfaces;
using BookStore.Services.MessageTypes;
using System;
using System.Collections.Generic;

namespace BookStore.Services
{
    public class CatalogueService : ICatalogueService
    {

        private ICatalogueProvider CatalogueProvider
        {
            get
            {
                return ServiceFactory.GetService<ICatalogueProvider>();
            }
        }

        private IDetailsProvider DetailsProvider
        {
            get
            {
                return ServiceFactory.GetService<IDetailsProvider>();
            }
        }

        public List<Media> GetMediaItems(int pOffset, int pCount)
        {
            var internalResult = CatalogueProvider.GetMediaItems(pOffset, pCount);
            var externalResult = MessageTypeConverter.Instance.Convert<
                List<BookStore.Business.Entities.Media>,
                List<BookStore.Services.MessageTypes.Media>>(internalResult);

            return externalResult;
        }


        public Media GetMediaById(int pId)
        {
            var external = MessageTypeConverter.Instance.Convert<
                BookStore.Business.Entities.Media,
                BookStore.Services.MessageTypes.Media>(
                CatalogueProvider.GetMediaById(pId));
            return external;
        }

        public void RateMedia(bool pLike, User pUser, Media pMedia)
        {
            DetailsProvider.RateMedia(
                    pLike,
                    MessageTypeConverter.Instance.Convert<
                    BookStore.Services.MessageTypes.User,
                    BookStore.Business.Entities.User>(pUser),
                    MessageTypeConverter.Instance.Convert<
                    BookStore.Services.MessageTypes.Media,
                    BookStore.Business.Entities.Media>(pMedia));
        }

        public Rating GetRating(int pUserId, int pMediaId)
        {
            var internalResult = DetailsProvider.GetRating(pUserId, pMediaId);
            return MessageTypeConverter.Instance.Convert<
                    BookStore.Business.Entities.Rating,
                    BookStore.Services.MessageTypes.Rating>(internalResult);
        }

        public Tuple<int, int> GetLikesAndDislikesForMedia(int pMediaId)
        {
            return DetailsProvider.GetCountLikesAndDislikesForMedia(pMediaId);
        }

        public List<Media> GetMediaLikedByUsersWhoLikedThisMedia(int pMediaId, int pUserId)
        {
            List<BookStore.Business.Entities.Media> internalResult = DetailsProvider.GetMediaLikedByUsersWhoLikedThisMedia(pMediaId, pUserId);
            List<BookStore.Services.MessageTypes.Media> externalResult = MessageTypeConverter.Instance.Convert<
                List<BookStore.Business.Entities.Media>,
                List<BookStore.Services.MessageTypes.Media>>(DetailsProvider.GetMediaLikedByUsersWhoLikedThisMedia(pMediaId, pUserId));

            return externalResult;
        }

        public bool CheckIfPurchaseExistsForMedia(int pMediaId, int pUserId)
        {
            return DetailsProvider.CheckIfPurchaseExistsForMedia(pMediaId, pUserId);
        }

        public void AddPurchase(Media pMedia, User pUser)
        {
            DetailsProvider.AddPurchase(
                    MessageTypeConverter.Instance.Convert<
                    BookStore.Services.MessageTypes.Media,
                    BookStore.Business.Entities.Media>(pMedia),
                    MessageTypeConverter.Instance.Convert<
                    BookStore.Services.MessageTypes.User,
                    BookStore.Business.Entities.User>(pUser)
                    );
        }
    }
}
