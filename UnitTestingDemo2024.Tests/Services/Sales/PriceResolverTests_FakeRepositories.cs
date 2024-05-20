using UnitTestingDemo2024.Models.Sales;
using UnitTestingDemo2024.Repositories.Sales;
using UnitTestingDemo2024.Services.Sales;

namespace UnitTestingDemo2024.Tests.Services.Sales;

[TestClass]
public class PriceResolverTests_FakeRepositories
{
	private const string CustomerWithIndividualPriceAndDiscount = "FAKE_CUST1";
	private const string ProductGroupWithDiscount = "FAKE_PG1";
	private const string ProductNumberWithBasicAndIndividualPrice = "FAKE_PN1";

	[TestMethod]
	public void PriceResolver_BasicPriceOnly_ReturnsBasicPrice()
	{
		// arrange
		var resolver = new PriceResolver(
			new FakeBasicPriceRepository(),
			new FakeCustomerProductGroupDiscountRepository_NoDiscounts(),
			new FakeCustomerPriceRepository_NoPrices());

		// act
		var resultPrice = resolver.GetPrice(
			new Customer() { Name = "FAKE_OTHER_CUSTOMER" },
			new Product()
			{
				ProductNumber = "FAKE_PN2",
				ProductGroup = new ProductGroup()
				{
					Name = "FAKE_PGROUP"
				}
			});


		// assert
		Assert.AreEqual(200M, resultPrice);
	}

	[TestMethod]
	public void PriceResolver_CustomerPriceAndProductGroupDiscount_ReturnsDiscountedCustomerPrice()
	{
		// arrange
		var resolver = new PriceResolver(
			new FakeBasicPriceRepository(),
			new FakeCustomerProductGroupDiscountRepository(),
			new FakeCustomerPriceRepository());

		// act
		var resultPrice = resolver.GetPrice(
			new Customer() { Name = CustomerWithIndividualPriceAndDiscount },
			new Product()
			{
				ProductNumber = ProductNumberWithBasicAndIndividualPrice,
				ProductGroup = new ProductGroup()
				{
					Name = ProductGroupWithDiscount
				}
			});


		// assert
		Assert.AreEqual(81M, resultPrice);
	}

	public class FakeCustomerProductGroupDiscountRepository : ICustomerProductGroupDiscountRepository
	{
		public decimal? GetDiscountForCustomerAndProductGroup(Customer customer, ProductGroup productGroup)
		{
			if ((customer.Name == CustomerWithIndividualPriceAndDiscount) && (productGroup.Name == ProductGroupWithDiscount))
			{
				return 0.1M;
			}
			return null;
		}
	}

	public class FakeCustomerProductGroupDiscountRepository_NoDiscounts : ICustomerProductGroupDiscountRepository
	{
		public decimal? GetDiscountForCustomerAndProductGroup(Customer customer, ProductGroup productGroup)
		{
			return null;
		}
	}

	public class FakeBasicPriceRepository : IBasicPriceRepository
	{
		public decimal? GetBasicPriceForProduct(Product product)
		{
			switch (product.ProductNumber)
			{
				case ProductNumberWithBasicAndIndividualPrice:
					return 100M;
				case "FAKE_PN2":
					return 200M;
				default:
					return null;
			}
		}
	}

	public class FakeCustomerPriceRepository_NoPrices : ICustomerPriceRepository
	{
		public decimal? GetCustomerPrice(Customer customer, Product product)
		{
			return null;
		}
	}

	public class FakeCustomerPriceRepository : ICustomerPriceRepository
	{
		public decimal? GetCustomerPrice(Customer customer, Product product)
		{
			if ((customer.Name == CustomerWithIndividualPriceAndDiscount) && (product.ProductNumber == ProductNumberWithBasicAndIndividualPrice))
			{
				return 90M;
			}
			return null;
		}
	}
}
