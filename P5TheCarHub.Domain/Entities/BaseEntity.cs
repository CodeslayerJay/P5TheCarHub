using P5TheCarHub.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace P5TheCarHub.Domain.Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}
