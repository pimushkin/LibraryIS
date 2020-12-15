using System;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class PublishingHouse : BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
