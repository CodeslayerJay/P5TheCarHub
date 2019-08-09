using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Interfaces
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
    }
}
