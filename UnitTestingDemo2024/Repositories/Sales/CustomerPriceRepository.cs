using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestingDemo2024.Models;
using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales
{
	public class CustomerPriceRepository(ApplicationDbContext applicationDbContext) : ICustomerPriceRepository
	{
		private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

		public decimal? GetCustomerPrice(Customer customer, Product product)
		{
			return _applicationDbContext.CustomerPrices.SingleOrDefault(p => (p.Customer.CustomerId == customer.CustomerId) && (p.Product.ProductId == product.ProductId))?.Price;
		}
	}
}