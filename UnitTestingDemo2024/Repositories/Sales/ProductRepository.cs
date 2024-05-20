using Microsoft.EntityFrameworkCore;
using UnitTestingDemo2024.Models;
using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales;

public class ProductRepository(ApplicationDbContext applicationDbContext) : IProductRepository
{
	private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

	public IEnumerable<Product> GetAll()
	{
		return _applicationDbContext.Products
					.Include(p => p.ProductGroup)
					.ToList();
	}

	public Product GetObject(int productId)
	{
		return _applicationDbContext.Products
			.Include(p => p.ProductGroup)
			.SingleOrDefault(p => p.ProductId == productId);
	}
}