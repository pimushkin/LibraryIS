using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class Author : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
