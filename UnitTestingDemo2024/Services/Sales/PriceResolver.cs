using UnitTestingDemo2024.Models.Sales;
using UnitTestingDemo2024.Repositories.Sales;

namespace UnitTestingDemo2024.Services.Sales;

public class PriceResolver(
	IBasicPriceRepository basicPriceRepository,
	ICustomerProductGroupDiscountRepository customerProductGroupDiscountRepository,
	ICustomerPriceRepository customerPriceRepository) : IPriceResolver
{
	private readonly IBasicPriceRepository _basicPriceRepository = basicPriceRepository;
	private readonly ICustomerProductGroupDiscountRepository _customerProductGroupDiscountRepository = customerProductGroupDiscountRepository;
	private readonly ICustomerPriceRepository _customerPriceRepository = customerPriceRepository;

	// Customer individual price has priority over basic price.
	// Customer discount for product group applies to both individual and basic price.
	public decimal? GetPrice(Customer customer, Product product)
	{
		decimal? basicPrice = _basicPriceRepository.GetBasicPriceForProduct(product);
		decimal? customerPrice = _customerPriceRepository.GetCustomerPrice(customer, product);

		decimal? priceBeforeDiscount = customerPrice ?? basicPrice;

		if (priceBeforeDiscount != null)
		{
			var customerProductGroupDiscount = _customerProductGroupDiscountRepository.GetDiscountForCustomerAndProductGroup(customer, product.ProductGroup);

			if (customerProductGroupDiscount != null)
			{
				return priceBeforeDiscount * (1 - customerProductGroupDiscount.Value);
			}

			return priceBeforeDiscount;
		}

		return null;
	}
}