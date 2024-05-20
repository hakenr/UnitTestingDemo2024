using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales;

public interface ICustomerPriceRepository
{
	decimal? GetCustomerPrice(Customer customer, Product product);
}