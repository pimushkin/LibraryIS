using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Core.Entities;

namespace LibraryIS.Application.Interfaces
{
    public interface IProfileService
    {
        public User GetProfilePageData(Guid profileId);
        public TakenBook[] GetOrderHistory(Guid profileId);
        public string[] GetListOfFavoriteCategories(Guid profileId);
    }
}
