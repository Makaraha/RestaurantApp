using Domain.Attributes;

namespace Domain
{
    [TableName("Ingredients")]
    public class Ingredient
    {
        [PrimaryKey]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int DishId { get; set; }
    }
}
