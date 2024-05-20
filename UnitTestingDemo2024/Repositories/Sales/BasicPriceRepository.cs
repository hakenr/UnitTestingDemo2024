using UnitTestingDemo2024.Models;
using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales;

public class BasicPriceRepository(ApplicationDbContext applicationDbContext) : IBasicPriceRepository
{
	private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

	public decimal? GetBasicPriceForProduct(Product product)
	{
		return _applicationDbContext.BasicPrices.SingleOrDefault(p => p.Product.ProductId == product.ProductId)?.Price;
	}
}