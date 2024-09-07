using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

namespace CBTDWeb.ViewModels
{
    // Purpose of a view model is to package up multiple models and bind the one model to a page
    public class ShoppingCartVM
    {
        public Product? Product { get; set; }

        [Range(1, 1000, ErrorMessage = "Must be between 1 and 1000")]
        public int Count { get; set; }

        public IEnumerable<ShoppingCart>? cartItems { get; set; }

        public double CartTotal { get; set; }

        public OrderHeader? OrderHeader { get; set; }

        public double GetPriceBasedOnQuantity(double quantity, double unitPrice, double priceHalfDozen, double priceDozen)
        {
            if (quantity <= 5)
            {
                return unitPrice;
            }
            else
            {
                if (quantity <= 11)
                {
                    return priceHalfDozen;
                }
                return priceDozen;
            }
        }

    }
}
