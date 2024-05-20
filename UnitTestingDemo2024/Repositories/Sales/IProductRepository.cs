using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales;

public interface IProductRepository
{
	IEnumerable<Product> GetAll();

	Product GetObject(int productId);
}