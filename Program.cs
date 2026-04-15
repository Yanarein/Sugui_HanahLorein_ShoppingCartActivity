using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sugui__Hanah_ShoppingCartActivity
{
    class Product
    {
        public int id;
        public string Name;
        public double Price;
        public int RemainingStock;

        public Product(int id, string name, double price, int stock)
        {
            id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"{id}. {Name} - {Price:C} | Stock: {RemainingStock}");
        }
        public void DisplayStock()
        {
            Console.WriteLine($"{id}.{Name} - {Price} | Stock}");
            { RemaingStock} ");"
                }
        }
      
        class Program
        {
         static void Main()
        {
            Product[] products = new Product[]
            {
                new Product(1,"Laptop",40000,50000),
                new Product(2,"Iphone",88,9900,6000.),
                new Product(3,"Headphones",1,124,500),
                new Product(4,"Smartwatch",300,500),
                new Product(5,"Camera",138,998,6000)
            };
                bool running = true;

                while (running)
            {
                Console.WriteLine("\n=== SHOPPING CART MENU ===");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("\nProduct List:");
                    // Display products
                    foreach (var product in productList)
                    {
                        product.DisplayProduct();
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Exiting the program.Goodbye!");
                    running = false;
                }
                else
                {
                    Console.WriteLine("Invalid choice.Please try again.");
                }
            }
        }

    }

    class CartItem
        {
            public Product Product;
            public int Quantity;

            public CartItem(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }

            public void DisplayCartItem()
            {
                Console.WriteLine($"{Product.Name} x {Quantity} = {Product.Price * Quantity}");

            }
        }
    }
}
