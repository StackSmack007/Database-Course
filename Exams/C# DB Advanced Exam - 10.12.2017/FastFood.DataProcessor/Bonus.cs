namespace FastFood.DataProcessor
{
    using FastFood.Data;
    using System.Linq;
    public static class Bonus
    {
	    public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
            var item = context.Items.FirstOrDefault(x => x.Name == itemName);
            if (item is null)
            {
                return $"Item {itemName} not found!";
            }
            decimal oldPrice = item.Price;
            item.Price = newPrice;
            context.SaveChanges();
            return $"{itemName} Price updated from ${oldPrice:F2} to ${newPrice:F2}";
	    }
    }
}