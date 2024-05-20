using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using UnitTestingDemo2024.Models;
using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales
{
	public class CustomerRepository(ApplicationDbContext applicationDbContext) : ICustomerRepository
	{
		private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

		public IEnumerable<Customer> GetAll()
		{
			return _applicationDbContext.Customers.ToList();
		}

		public Customer GetObject(int customerId)
		{
			return _applicationDbContext.Customers.SingleOrDefault(c => c.CustomerId == customerId);
		}
	}
}