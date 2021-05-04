using LibraryIS.Domain.Common;
using System;

namespace LibraryIS.Domain.Entities
{
    public class User : AggregateRoot
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public virtual string Password { get; }
        public bool IsApproved { get; }
        public DateTime CreationDate { get; }
        public bool IsAdmin { get; }
        public virtual ReaderProfile ReaderProfile { get; }

        protected User()
        {
        }

        public User(string firstName, string lastName, string email, string password, bool isApproved,
            DateTime creationDate, bool isAdmin, ReaderProfile readerProfile) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            IsApproved = isApproved;
            CreationDate = creationDate;
            IsAdmin = isAdmin;
            ReaderProfile = readerProfile;
        }
    }
}
