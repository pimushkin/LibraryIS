using LibraryIS.Domain.Common;

namespace LibraryIS.Domain.Entities
{
    public class Evaluation : Entity
    {
        public double Rating { get; }
        public virtual Book Book { get; }
        public virtual ReaderProfile ReaderProfile { get; }

        protected Evaluation()
        {
        }

        public Evaluation(double rating, Book book, ReaderProfile readerProfile) : this()
        {
            Rating = rating;
            Book = book;
            ReaderProfile = readerProfile;
        }
    }
}
