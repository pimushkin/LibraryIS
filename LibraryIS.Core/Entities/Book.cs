using System;
using System.Collections.Generic;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class Book : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public List<PublishingHouse> PublishingHouses { get; set; }
        public List<Genre> Genres { get; set; }
        public Language Language { get; set; }
    }
}
