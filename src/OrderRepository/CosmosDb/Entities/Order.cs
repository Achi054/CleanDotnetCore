using Cosmonaut.Attributes;

namespace Repository.CosmosDb.Entities
{
    [CosmosCollection("Order")]
    public class Order : Entity
    {
        public string Name { get; set; }

        public string Details { get; set; }

        public int Quantity { get; set; }
    }
}
