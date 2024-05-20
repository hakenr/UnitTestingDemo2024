using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales;

public interface IBasicPriceRepository
{
	decimal? GetBasicPriceForProduct(Product product);
}