using LibraryIS.Application.Repositories;
using LibraryIS.Domain.Entities;
using LibraryIS.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using LanguageExt;

namespace LibraryIS.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckExistenceOfUserAsync(string email, string libraryCard,
            string passportAndSeriesNumber)
        {
            return await Context.Set<User>().AnyAsync(x =>
                x.Email == email || x.ReaderProfile.LibraryCard == libraryCard ||
                x.ReaderProfile.PassportSeriesAndNumber == passportAndSeriesNumber);
        }

        public async Task<Option<User>> GetUserByCredentials(string email, string password)
        {
            return await Context.Set<User>()
                .SingleOrDefaultAsync(user => user.Email == email && user.Password == password);
        }
    }
}