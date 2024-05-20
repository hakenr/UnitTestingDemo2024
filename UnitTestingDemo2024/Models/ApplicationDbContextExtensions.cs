using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Models;

public static class ApplicationDbContextExtensions
{
	public static void EnsureSeedData(this ApplicationDbContext context)
	{
		if (!context.Customers.Any())
		{
			var customerHavit = new Customer() { Name = "HAVIT" };
			var customerMoonFish = new Customer() { Name = "MoonFish" };
			context.Customers.Add(customerHavit);
			context.Customers.Add(customerMoonFish);
			context.SaveChanges();

			var productGroupSoftware = new ProductGroup() { Name = "Software" };
			var productGroupConsulting = new ProductGroup() { Name = "Consulting" };
			context.ProductGroups.Add(productGroupSoftware);
			context.ProductGroups.Add(productGroupConsulting);
			context.SaveChanges();

			var productSw1 = new Product() { ProductNumber = "Microsoft Azure", ProductGroup = productGroupSoftware };
			var productCn1 = new Product() { ProductNumber = "Rada nad zlato", ProductGroup = productGroupConsulting };
			context.Products.Add(productSw1);
			context.Products.Add(productCn1);
			context.SaveChanges();

			context.BasicPrices.Add(new BasicPrice() { Product = productSw1, Price = 10.0M });
			context.BasicPrices.Add(new BasicPrice() { Product = productCn1, Price = 20.0M });
			context.SaveChanges();

			context.CustomerProductGroupDiscounts.Add(new CustomerProductGroupDiscount() { Customer = customerHavit, ProductGroup = productGroupSoftware, Discount = 0.1M });
			context.SaveChanges();

			context.CustomerPrices.Add(new CustomerPrice() { Customer = customerHavit, Product = productSw1, Price = 9.50M });
			context.SaveChanges();
		}
	}

}