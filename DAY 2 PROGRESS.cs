using System;
using System.Collections.Generic;

namespace DAY_2_SHOPPING_CART
{
    // =========================
    // PRODUCT CLASS (DAY 1)
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
    // SHOPPING CART CLASS (DAY 2)
    // =========================
    class ShoppingCart
    {
        private readonly List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void ViewCart()
        {
            Console.WriteLine("\n=============================");
            Console.WriteLine("    SHOPPING CART RECEIPT");
            Console.WriteLine("=============================\n");

            if (products.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            double total = 0;

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id}. {p.Name,-15} ₱ {p.Price,8:F2}");
                total += p.Price;
            }

            Console.WriteLine("\n-----------------------------");
            Console.WriteLine($"TOTAL:          ₱ {total,8:F2}");
            Console.WriteLine("=============================");
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

            List<Product> products = new List<Product>
            {
                new Product(1, "Puma Shoes", 7600),
                new Product(2, "Penshoppe Shirt", 1500),
                new Product(3, "Denim Jeans", 1500),
                new Product(4, "Cap", 199),
                new Product(5, "Socks", 99.99),
                new Product(6, "Jacket", 1500),
                new Product(7, "Hoodie", 1200),
                new Product(8, "Shorts", 450),
                new Product(9, "Belt", 250),
                new Product(10, "Watch", 1800),
                new Product(11, "Perfume", 1200)
            };

            foreach (var product in products)
            {
                cart.AddProduct(product);
            }

            cart.ViewCart();
        }
    }
}