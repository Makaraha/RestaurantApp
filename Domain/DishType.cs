using Domain.Attributes;

namespace Domain
{
    [TableName("DishTypes")]
    public class DishType
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
