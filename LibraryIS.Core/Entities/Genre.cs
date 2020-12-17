using System;
using System.Collections.Generic;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class Genre : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public List<Book> Books { get; set; }
        public List<ReaderProfile> ReaderProfiles { get; set; }
    }
}
