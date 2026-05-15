using System;
using System.Collections.Generic;

namespace DAY_3
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
    // CART ITEM CLASS (NEW)
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

        // VIEW CART
        public void ViewCart()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("         SHOPPING CART RECEIPT");
            Console.WriteLine("======================================\n");

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
    }

    // =========================
    // MAIN PROGRAM
    // =========================
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();

            // PRODUCTS WITH QUANTITY
            cart.AddProduct(new Product(1, "Puma Shoes", 7600), 1);
            cart.AddProduct(new Product(2, "Penshoppe Shirt", 1500), 2);
            cart.AddProduct(new Product(3, "Denim Jeans", 1500), 1);
            cart.AddProduct(new Product(4, "Cap", 199), 3);
            cart.AddProduct(new Product(5, "Socks", 99.99), 5);
            cart.AddProduct(new Product(6, "Jacket", 1500), 1);
            cart.AddProduct(new Product(7, "Hoodie", 1200), 1);

            // REMOVE SAMPLE
            cart.RemoveProduct(4);

            // DISPLAY CART
            cart.ViewCart();
        }
    }
}