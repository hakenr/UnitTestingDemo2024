using Moq;
using UnitTestingDemo2024.Models.Sales;
using UnitTestingDemo2024.Repositories.Sales;
using UnitTestingDemo2024.Services.Sales;

namespace UnitTestingDemo2024.Tests.Services.Sales;

[TestClass]
public class PriceResolverTests_MoqRepositories
{
	[TestMethod]
	public void Moq_PriceResolver_BasicPriceOnly_ReturnsBasicPrice()
	{
		// arrange
		const string productNumber = "FAKE_PN";

		var basicPriceRepositoryMock = new Mock<IBasicPriceRepository>();
		basicPriceRepositoryMock.Setup(r =>
			r.GetBasicPriceForProduct(It.Is<Product>(product => product.ProductNumber == productNumber)))
			.Returns(200M);

		var resolver = new PriceResolver(
			basicPriceRepositoryMock.Object,
			new Mock<ICustomerProductGroupDiscountRepository>().Object,
			new Mock<ICustomerPriceRepository>().Object);

		// act
		var resultPrice = resolver.GetPrice(
			new Customer() { Name = "ANY_CUSTOMER" },
			new Product()
			{
				ProductNumber = productNumber,
				ProductGroup = new ProductGroup()
				{
					Name = "ANY_GROUP"
				}
			});


		// assert
		Assert.AreEqual(200M, resultPrice);
	}

	[TestMethod]
	public void Moq_PriceResolver_CustomerPriceAndProductGroupDiscount_ReturnsDiscountedCustomerPrice()
	{
		// arrange
		const string productNumber = "FAKE_PN";
		const string customer = "FAKE_CUST";
		const string productGroup = "FAKE_PG";

		var basicPriceRepositoryMock = new Mock<IBasicPriceRepository>();
		basicPriceRepositoryMock.Setup(r =>
			r.GetBasicPriceForProduct(It.Is<Product>(product => product.ProductNumber == productNumber)))
			.Returns(100M);

		var customerProductGroupDicountRepositoryMock = new Mock<ICustomerProductGroupDiscountRepository>();
		customerProductGroupDicountRepositoryMock.Setup(r =>
			r.GetDiscountForCustomerAndProductGroup(
				It.Is<Customer>(c => c.Name == customer),
				It.Is<ProductGroup>(pg => pg.Name == productGroup)))
			.Returns(0.1M);

		var customerPriceRepositoryMock = new Mock<ICustomerPriceRepository>();
		customerPriceRepositoryMock.Setup(r =>
			r.GetCustomerPrice(
				It.Is<Customer>(c => c.Name == customer),
				It.Is<Product>(p => p.ProductNumber == productNumber)))
			.Returns(90M);

		var resolver = new PriceResolver(
			basicPriceRepositoryMock.Object,
			customerProductGroupDicountRepositoryMock.Object,
			customerPriceRepositoryMock.Object);

		// act
		var resultPrice = resolver.GetPrice(
			new Customer() { Name = customer },
			new Product()
			{
				ProductNumber = productNumber,
				ProductGroup = new ProductGroup()
				{
					Name = productGroup
				}
			});


		// assert
		Assert.AreEqual(81M, resultPrice);
	}
}