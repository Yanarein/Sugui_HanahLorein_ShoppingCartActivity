using System;

namespace ShoppingCart
{
    class Product
    {
        // Getters and Setters
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        // Constructor
        public Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        // Display Product
        public void Display()
        {
            Console.WriteLine($"{Id}. {Name} - ₱{Price:F2}");
        }
    }

    class Program
    {
        static void Main()
        {
            Product[] products =
            {
                new Product(1, "Laptop", 40000),
                new Product(2, "Headphones", 1200),
                new Product(3, "T-Shirt", 500)
            };

            Console.WriteLine("=== PRODUCT LIST ===");

            foreach (var p in products)
                p.Display();
        }
    }
}