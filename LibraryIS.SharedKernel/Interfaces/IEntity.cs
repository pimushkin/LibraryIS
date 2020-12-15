using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryIS.SharedKernel.Interfaces
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
