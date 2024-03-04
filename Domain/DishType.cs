using Domain.Attributes;
using Domain.Interfaces;

namespace Domain
{
    [TableName("DishTypes")]
    public class DishType : IEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
