using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.Interfaces;
using LibraryIS.Core.Entities;

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
