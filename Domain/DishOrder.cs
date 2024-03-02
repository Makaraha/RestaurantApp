using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("DishesOrders")]
    public class DishOrder : IIdHas
    {
        [PrimaryKey]
        public int Id { get; set; }

        public int DishId { get; set; }

        public int OrderId { get; set; }
    }
}
