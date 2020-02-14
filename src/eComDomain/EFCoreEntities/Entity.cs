using System;

namespace Domain.EFCoreEntities
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
