using Domain.Attributes;

using Domain.Interfaces;
namespace Domain
{
    [TableName("Ingredients")]
    public  class Ingredient : IIdHas
    {
        [PrimaryKey]
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public int DishId { get; set; }
    }
}
