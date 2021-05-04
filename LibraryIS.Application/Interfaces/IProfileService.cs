using LibraryIS.Domain.Entities;
using System;

namespace LibraryIS.Application.Interfaces
{
    public interface IProfileService
    {
        public User GetProfilePageData(Guid profileId);
        public TakenBook[] GetOrderHistory(Guid profileId);
        public string[] GetListOfFavoriteCategories(Guid profileId);
    }
}
