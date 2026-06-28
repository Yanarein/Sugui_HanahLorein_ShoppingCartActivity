using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAY_4
{
    // ======================================
    // PRODUCT CLASS
    // ======================================
    class Product
    {
        public int Id { get; }
        public string Name { get; }
        public double Price { get; }

        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    // =====================================
    // CART ITEM CLASS
    // =====================================
    class CartItem
    {
        public Product Product { get; }
        public int Quantity { get; set; }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public double GetSubtotal()
        {
            return Product.Price * Quantity;
        }
    }

    // ==================================
    // SHOPPING CART CLASS
    // ==================================
    class ShoppingCart
    {
        private readonly List<CartItem> items = new List<CartItem>();

        // ADD PRODUCT
        public void AddProduct(Product product, int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            items.Add(new CartItem(product, quantity));
        }

        // REMOVE PRODUCT
        public void RemoveProduct(int productId)
        {
            CartItem item = items.Find(i => i.Product.Id == productId);

            if (item != null)
            {
                items.Remove(item);
                Console.WriteLine($"{item.Product.Name} removed from cart.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        // COMPUTE TOTAL
        public double GetTotal()
        {
            double total = 0;

            foreach (var item in items)
            {
                total += item.GetSubtotal();
            }

            return total;
        }

        // DISPLAY RECEIPT
        public void ViewCart(double discountRate = 0)
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("           SHOPPING CART RECEIPT");
            Console.WriteLine("==========================================\n");

            if (items.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine(
                    $"{item.Product.Id}. " +
                    $"{item.Product.Name,-18} " +
                    $"x{item.Quantity} " +
                    $"₱ {item.GetSubtotal(),8:F2}"
                );
            }

            double total = GetTotal();
            double discount = total * discountRate;
            double finalTotal = total - discount;

            Console.WriteLine("\n------------------------------------------");
            Console.WriteLine($"Subtotal:              ₱ {total,8:F2}");
            Console.WriteLine($"Discount:              ₱ {discount,8:F2}");
            Console.WriteLine($"Final Total:           ₱ {finalTotal,8:F2}");
            Console.WriteLine("==========================================");
        }

        // CHECKOUT
        public void Checkout(double payment, double discountRate = 0)
        {
            double total = GetTotal();
            double discount = total * discountRate;
            double finalTotal = total - discount;

            Console.WriteLine("\n============== CHECKOUT =================");

            if (payment < finalTotal)
            {
                Console.WriteLine("Insufficient payment.");
                return;
            }

            double change = payment - finalTotal;

            Console.WriteLine($"Total Amount:   ₱ {finalTotal:F2}");
            Console.WriteLine($"Payment:        ₱ {payment:F2}");
            Console.WriteLine($"Change:         ₱ {change:F2}");

            Console.WriteLine("=========================================");
            Console.WriteLine("     THANK YOU FOR SHOPPING!");
            Console.WriteLine("=========================================");
        }
    }

    // ===============================
    // MAIN PROGRAM
    // ===============================
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();

            // PRODUCTS
            cart.AddProduct(new Product(1, "Puma Shoes", 7600), 1);
            cart.AddProduct(new Product(2, "Penshoppe Shirt", 1500), 2);
            cart.AddProduct(new Product(3, "Denim Jeans", 1500), 1);
            cart.AddProduct(new Product(4, "Cap", 199), 2);
            cart.AddProduct(new Product(5, "Socks", 99.99), 5);

            // REMOVE SAMPLE
            cart.RemoveProduct(4);

            // VIEW CART WITH 10% DISCOUNT
            cart.ViewCart(0.10);

            // CHECKOUT
            cart.Checkout(15000, 0.10);
        }
    }
}