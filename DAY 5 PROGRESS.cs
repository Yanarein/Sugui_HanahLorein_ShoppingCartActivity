using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAY_5
{
    // =========================
    // PRODUCT CLASS
    // =========================
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

    // =========================
    // CART ITEM CLASS
    // =========================
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

    // =========================
    // SHOPPING CART CLASS
    // =========================
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
            Console.WriteLine($"{product.Name} added to cart.");
        }

        // VIEW CART
        public void ViewCart()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("         SHOPPING CART RECEIPT");
            Console.WriteLine("========================================\n");

            if (items.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            double total = 0;

            foreach (var item in items)
            {
                double subtotal = item.GetSubtotal();

                Console.WriteLine(
                    $"{item.Product.Id}. " +
                    $"{item.Product.Name,-18} " +
                    $"x{item.Quantity} " +
                    $"₱ {subtotal,8:F2}"
                );

                total += subtotal;
            }

            Console.WriteLine("\n--------------------------------------");
            Console.WriteLine($"TOTAL:                  ₱ {total,8:F2}");
            Console.WriteLine("======================================");
        }

        // CHECKOUT
        public void Checkout(double payment)
        {
            double total = 0;

            foreach (var item in items)
            {
                total += item.GetSubtotal();
            }

            Console.WriteLine("\n============== CHECKOUT ==============");

            if (payment < total)
            {
                Console.WriteLine("Insufficient payment.");
                return;
            }

            double change = payment - total;

            Console.WriteLine($"Total Amount:   ₱ {total:F2}");
            Console.WriteLine($"Payment:        ₱ {payment:F2}");
            Console.WriteLine($"Change:         ₱ {change:F2}");

            Console.WriteLine("======================================");
            Console.WriteLine("      THANK YOU FOR SHOPPING!");
            Console.WriteLine("======================================");
        }
    }

    // =========================
    // MAIN PROGRAM
    // =========================
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();

            // AVAILABLE PRODUCTS
            List<Product> products = new List<Product>
            {
                new Product(1, "Puma Shoes", 7600),
                new Product(2, "Penshoppe Shirt", 1500),
                new Product(3, "Denim Jeans", 1500),
                new Product(4, "Cap", 199),
                new Product(5, "Socks", 99.99),
                new Product(6, "Jacket", 1500),
                new Product(7, "Hoodie", 1200)
            };

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n======================================");
                Console.WriteLine("         SHOPPING CART MENU");
                Console.WriteLine("========================================");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Add Product to Cart");
                Console.WriteLine("3. View Cart");
                Console.WriteLine("4. Checkout");
                Console.WriteLine("5. Exit");
                Console.WriteLine("========================================");

                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\n========== PRODUCTS ==========\n");

                        foreach (var product in products)
                        {
                            Console.WriteLine(
                                $"{product.Id}. " +
                                $"{product.Name,-18} " +
                                $"₱ {product.Price:F2}"
                            );
                        }

                        break;

                    case 2:
                        Console.Write("\nEnter Product ID: ");
                        int productId = int.Parse(Console.ReadLine());

                        Product selectedProduct =
                            products.Find(p => p.Id == productId);

                        if (selectedProduct == null)
                        {
                            Console.WriteLine("Product not found.");
                            break;
                        }

                        Console.Write("Enter Quantity: ");
                        int quantity = int.Parse(Console.ReadLine());

                        cart.AddProduct(selectedProduct, quantity);

                        break;

                    case 3:
                        cart.ViewCart();
                        break;

                    case 4:
                        cart.ViewCart();

                        Console.Write("\nEnter Payment Amount: ₱ ");
                        double payment = double.Parse(Console.ReadLine());

                        cart.Checkout(payment);

                        running = false;
                        break;

                    case 5:
                        running = false;
                        Console.WriteLine("Program exited.");
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
