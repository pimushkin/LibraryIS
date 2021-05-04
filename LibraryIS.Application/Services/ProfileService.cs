using LibraryIS.Application.Interfaces;
using LibraryIS.Domain.Entities;
using System;

namespace LibraryIS.Application.Services
{
    public class ProfileService : IProfileService
    {
        public User GetProfilePageData(Guid profileId)
        {
            throw new NotImplementedException();
        }

        public TakenBook[] GetOrderHistory(Guid profileId)
        {
            throw new NotImplementedException();
        }

        public string[] GetListOfFavoriteCategories(Guid profileId)
        {
            throw new NotImplementedException();
        }
    }
}
