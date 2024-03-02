using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("Dishes")]
    public class Dish : IIdHas
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }    

        public int DishTypeId { get; set; }
    }
}
