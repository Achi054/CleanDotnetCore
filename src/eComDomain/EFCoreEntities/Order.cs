namespace Domain.EFCoreEntities
{
    public class Order : Entity
    {
        public string Name { get; set; }

        public string Details { get; set; }

        public int Quantity { get; set; }
    }
}
