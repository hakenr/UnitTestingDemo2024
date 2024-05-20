using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Services.Sales;

public interface IPriceResolver
{
	decimal? GetPrice(Customer customer, Product product);
}