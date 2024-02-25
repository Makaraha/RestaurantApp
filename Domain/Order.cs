using Domain.Attributes;

namespace Domain
{
    [TableName("Orders")]
    public class Order
    {
        [PrimaryKey]
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }
    }
}
