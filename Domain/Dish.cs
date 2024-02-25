using Domain.Attributes;

namespace Domain
{
    [TableName("Dishes")]
    public class Dish
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }    

        public int DishTypeId { get; set; }
    }
}
