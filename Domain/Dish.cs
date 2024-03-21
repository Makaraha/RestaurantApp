using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("Dishes")]
    public record Dish : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }    

        public decimal ExtraCharge { get; set; }

        public int DishTypeId { get; set; }
    }
}
