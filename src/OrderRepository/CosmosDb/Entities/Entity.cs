using System;
using Cosmonaut.Attributes;

namespace Repository.CosmosDb.Entities
{
    public abstract class Entity
    {
        [CosmosPartitionKey]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
