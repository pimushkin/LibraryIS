using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class Language : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
