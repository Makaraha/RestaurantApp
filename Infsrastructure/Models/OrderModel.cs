namespace Infrastructure.Models
{
    public class OrderModel
    {
        public string Name { get; init; }

        public List<DishModel> Dishes { get; init; }

        public decimal TotalCost { get; init; }
    }
}
