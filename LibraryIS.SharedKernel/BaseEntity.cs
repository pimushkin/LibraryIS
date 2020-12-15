using System;
using System.Collections.Generic;
using System.Text;
using LibraryIS.SharedKernel.Interfaces;

namespace LibraryIS.SharedKernel
{
    public abstract class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
