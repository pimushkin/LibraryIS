using LibraryIS.Domain.Common;
using System.Collections.Generic;

namespace LibraryIS.Domain.Entities
{
    public class PublishingHouse : Entity
    {
        public string Name { get; }
        public virtual IReadOnlyList<Book> Books { get; }

        protected PublishingHouse()
        {
        }
    }
}
