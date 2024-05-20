using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales;

public interface ICustomerRepository
{
	IEnumerable<Customer> GetAll();

	Customer GetObject(int customerId);
}