using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("DishesOrders")]
    public record DishOrder : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int DishId { get; set; }

        public int OrderId { get; set; }
    }
}
