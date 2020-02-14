using System;
using Cosmonaut.Attributes;

namespace Domain.CosmosDb.Entities
{
    public abstract class Entity
    {
        [CosmosPartitionKey]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
