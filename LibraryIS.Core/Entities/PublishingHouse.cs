using System;
using System.Collections.Generic;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class PublishingHouse : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
