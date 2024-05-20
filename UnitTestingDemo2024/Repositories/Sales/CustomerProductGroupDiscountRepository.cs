using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnitTestingDemo2024.Models;
using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales
{
	public class CustomerProductGroupDiscountRepository(ApplicationDbContext applicationDbContext) : ICustomerProductGroupDiscountRepository
	{
		private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

		public decimal? GetDiscountForCustomerAndProductGroup(Customer customer, ProductGroup productGroup)
		{
			return _applicationDbContext.CustomerProductGroupDiscounts
				.SingleOrDefault(d => (d.Customer.CustomerId == customer.CustomerId) && (d.ProductGroup.ProductGroupId == productGroup.ProductGroupId))?.Discount;
		}
	}
}