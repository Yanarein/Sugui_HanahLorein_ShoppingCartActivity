using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Policy;
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
            this.id = id;
            Name = name;
            Price = price;
            RemainingStock = stock;
        }

        public void DisplayProduct()
        {
            Console.WriteLine($"{id}. {Name} - {Price} | Stock: {RemainingStock}");
        }

        public bool HasEnoughStock(int quantity)
        {
            return quantity <= RemainingStock;
        }

        public void DeductStock(int quantity)
        {
            RemainingStock -= quantity;
        }

        public double CalculateTotalPrice(int quantity)

        {
            return Price * quantity;
        }
    }

    class Program
    {
        static void Main()
        {
            Product[] products = new Product[]
            {
                new Product(1,"Laptop",40000,50000),
                new Product(2,"Iphone",88998,6000)
                new Product(3,"Headphones",1124,500),
                new Product(4,"Smartwatch",300,500),
                new Product(5,"Camera",138998,6000)
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
                    foreach (var product in products)
                    {
                        product.DisplayProduct();

                        Console.Write("Enter product number; ");
                        if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > products.Length)
                        {
                            Console.WriteLine("Invalid product number. Please try again.");
                            continue;

                        }
                        Console.Write("Enter quantity;");
                        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)

                        {
                            Console.WriteLine("Invalid quantity. Please try again.");
                            continue;

                        }
                        Product selectedProduct = products[number - 1];

                        if (selectedProduct.RemainingStock <= 0)
                        {
                            Console.WriteLine("Out of stock.");
                        }
                        else if (!selectedProduct.HasEnoughStock(quantity))
                        {
                            Console.WriteLine("Not enough stock.");
                        }
                        else
                        {
                            //Update cart and stock

                            if (cart.ContainsKey(number))
                                cart[number] += quantity;
                        }
                        else
                        {
                            cart.Add(number, quantity);
                            selectedProduct.DeductStock(quantity);
                            Console.WriteLine("Added to cart successfully!");
                        }
                        else if (choice == "2"))
                        {
                            running = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid option. Please try again.");
                        }
                    }
                    //CHECKOUT
                    Console.WriteLine("\n=== CHECKOUT ===");
                    double grandTotal = 0;

                    foreach (var item in cart)
                    {
                        Product product = products[item.Key - 1];
                        double total = product.CalculateTotalPrice(item.Value);
                        grandTotal += total;
                        Console.WriteLine($"{product.Name} x {item.Value} = {total}");
                    }

                    Console.WriteLine($"Grand Total: {grandTotal}");
                    double discount = 0;
                    if (grandTotal > 5000)
                    {
                        discount = grandTotal * 0.10;

                        Console.WriteLine($"Discount (10%): {discount}");
                    }

                    double finalTotal = grandTotal - discount;
                    Console.WriteLine($"Final Total: {finalTotal}");

                    Console.WriteLine("Thank you for shopping with us!");
                }
            )
        }





                        