using System;
using System.Collections.Generic;
using System.Text;
using LibraryIS.SharedKernel;

namespace LibraryIS.Core.Entities
{
    public class User : BaseEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsApproved { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsAdmin { get; set; }
        public ReaderProfile ReaderProfile { get; set; }
    }
}
