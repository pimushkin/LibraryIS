using LibraryIS.Domain.Common;
using System.Collections.Generic;

namespace LibraryIS.Domain.Entities
{
    public class Genre : Entity
    {
        public string Name { get; }
        public virtual IReadOnlyList<Book> Books { get; }
        public virtual IReadOnlyList<ReaderProfile> ReaderProfiles { get; }

        public Genre()
        {
        }
    }
}
