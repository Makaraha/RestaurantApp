using Domain.Attributes;

namespace Domain
{
    [TableName("DishesOrders")]
    public class DishOrder
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int DishId { get; set; }

        public int OrderId { get; set; }
    }
}
