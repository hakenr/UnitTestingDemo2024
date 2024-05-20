using UnitTestingDemo2024.Models.Sales;

namespace UnitTestingDemo2024.Repositories.Sales;

public interface ICustomerProductGroupDiscountRepository
{
	decimal? GetDiscountForCustomerAndProductGroup(Customer customer, ProductGroup productGroup);
}