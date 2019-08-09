using P5TheCarHub.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Core.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
