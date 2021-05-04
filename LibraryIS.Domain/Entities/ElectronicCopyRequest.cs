using LibraryIS.Domain.Common;
using LibraryIS.Domain.Enums;

namespace LibraryIS.Domain.Entities
{
    public class ElectronicCopyRequest : Entity
    {
        public virtual ReaderProfile RequestingReader { get; }
        public virtual Book Book { get; }
        public RequestStatus Status { get; }
        public string PagesRange { get; }

        protected ElectronicCopyRequest()
        {
        }
    }
}
