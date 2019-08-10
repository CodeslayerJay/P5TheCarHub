using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Exceptions
{
    public class PhotoNotFoundException : Exception
    {
        public PhotoNotFoundException(int id) : base($"Photo not found with id: {id}")
        { }
    }
}
