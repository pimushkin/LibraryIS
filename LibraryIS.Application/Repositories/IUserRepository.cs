using LibraryIS.Domain.Entities;
using System.Threading.Tasks;
using LanguageExt;

namespace LibraryIS.Application.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<bool> CheckExistenceOfUserAsync(string email, string libraryCard, string passportAndSeriesNumber);
        public Task<Option<User>> GetUserByCredentials(string email, string password);
    }
}