using Domain.Attributes;

using Domain.Interfaces;
namespace Domain
{
    [TableName("Ingredients")]
    public record Ingredient : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int DishId { get; set; }
    }
}
