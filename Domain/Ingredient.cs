using Domain.Attributes;

using Domain.Interfaces;
namespace Domain
{
    [TableName("Ingredients")]
    public record Ingredient : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public float Amount { get; set; }

        public int ProductId { get; set; }

        public int DishId { get; set; }
    }
}
