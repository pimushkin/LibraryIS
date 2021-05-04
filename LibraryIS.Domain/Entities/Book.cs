using LibraryIS.Domain.Common;
using System;
using System.Collections.Generic;

namespace LibraryIS.Domain.Entities
{
    public class Book : AggregateRoot
    {
        public string Name { get; }
        public virtual IReadOnlyList<Author> Authors { get; }
        public DateTime PublicationDate { get; }
        public string Description { get;}
        public int PageCount { get; }
        public virtual IReadOnlyList<PublishingHouse> PublishingHouses { get; }
        public virtual IReadOnlyList<Genre> Genres { get; }
        public virtual Language BookLanguage { get; }
        public double Rating { get; set; }

        protected Book()
        {
        }
    }
}
