using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    class Product
    {
        public int Id;
        public string Name;
        public double Price;
        public int Stock;
        public string Category;

        public Product(int id, string name, double price, int stock, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Stock = stock;
            Category = category;
        }

        public void Display()
        {
            Console.WriteLine($"[{Id}] {Name,-15} | Price: ₱{Price,8:F2} | Stock: {Stock,-3} | {Category}");
        }
    }

    class Program
    {
        static List<string> orderHistory = new List<string>();
        static int receiptNo = 1;

        static void Main()
        {
            List<Product> products = new List<Product>
            {
                new Product(1, "Laptop", 40000, 1000, "Electronics"),
                new Product(2, "iPhone", 88998, 1800, "Electronics"),
                new Product(3, "Headphones", 1124, 1500, "Electronics"),
                new Product(4, "T-Shirt", 500, 2000, "Clothing"),
                new Product(5, "Bread", 60, 600, "Food")
            };

            Dictionary<int, int> cart = new Dictionary<int, int>();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== SHOPPING CART SYSTEM =====");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Search Product");
                Console.WriteLine("3. Filter Category");
                Console.WriteLine("4. Cart Menu");
                Console.WriteLine("5. Order History");
                Console.WriteLine("6. Exit");

                int choice = ReadInt("Select: ");

                switch (choice)
                {
                    case 1: AddToCart(products, cart); break;
                    case 2: Search(products); break;
                    case 3: Filter(products); break;
                    case 4: CartMenu(products, cart); break;
                    case 5: ShowHistory(); break;
                    case 6: return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        // ================= INPUT =================

        static int ReadInt(string msg)
        {
            int val;
            Console.Write(msg);

            while (!int.TryParse(Console.ReadLine(), out val))
                Console.Write("Invalid input. Enter number: ");

            return val;
        }

        static double ReadDouble(string msg)
        {
            double val;
            Console.Write(msg);

            while (!double.TryParse(Console.ReadLine(), out val))
                Console.Write("Invalid input. Enter number: ");

            return val;
        }

        static string ReadYesNo(string msg)
        {
            while (true)
            {
                Console.Write(msg);
                string input = Console.ReadLine().Trim().ToUpper();

                if (input == "Y" || input == "N")
                    return input;

                Console.WriteLine("Enter Y or N only.");
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        static Product FindProduct(List<Product> products, int id)
        {
            foreach (var p in products)
                if (p.Id == id)
                    return p;

            return null;
        }

        // ================= FEATURES =================

        static void AddToCart(List<Product> products, Dictionary<int, int> cart)
        {
            foreach (var p in products)
                p.Display();

            int id = ReadInt("Enter Product ID: ");
            Product pdt = FindProduct(products, id);

            if (pdt == null)
            {
                Console.WriteLine("Product not found.");
                Pause();
                return;
            }

            int qty = ReadInt("Enter Quantity: ");

            if (qty <= 0 || qty > pdt.Stock)
            {
                Console.WriteLine("Invalid quantity or insufficient stock.");
                Pause();
                return;
            }

            if (cart.ContainsKey(id))
                cart[id] += qty;
            else
                cart[id] = qty;

            pdt.Stock -= qty;

            Console.WriteLine("Added to cart.");
            Pause();
        }

        static void Search(List<Product> products)
        {
            Console.Write("Search product: ");
            string key = Console.ReadLine().ToLower().Trim();

            bool found = false;

            foreach (var p in products)
            {
                if (p.Name.ToLower().Contains(key))
                {
                    p.Display();
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("No product found.");

            Pause();
        }

        static void Filter(List<Product> products)
        {
            Console.WriteLine("Categories: Electronics | Clothing | Food");
            Console.Write("Enter Category: ");
            string cat = Console.ReadLine().Trim();

            bool found = false;

            foreach (var p in products)
            {
                if (p.Category.Equals(cat, StringComparison.OrdinalIgnoreCase))
                {
                    p.Display();
                    found = true;
                }
            }

            if (!found)
                Console.WriteLine("No products found.");

            Pause();
        }

        // ================= CART MENU =================

        static void CartMenu(List<Product> products, Dictionary<int, int> cart)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== CART MENU =====");
                Console.WriteLine("1. View Cart");
                Console.WriteLine("2. Remove Item");
                Console.WriteLine("3. Update Quantity");
                Console.WriteLine("4. Clear Cart");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Back");

                int choice = ReadInt("Select: ");

                switch (choice)
                {
                    case 1: ViewCart(products, cart); Pause(); break;
                    case 2: RemoveItem(products, cart); Pause(); break;
                    case 3: UpdateItem(products, cart); Pause(); break;
                    case 4: ClearCart(products, cart); Pause(); break;
                    case 5: Checkout(products, cart); break;
                    case 6: return;
                }
            }
        }

        static void ViewCart(List<Product> products, Dictionary<int, int> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                return;
            }

            double total = 0;

            foreach (var item in cart)
            {
                Product p = FindProduct(products, item.Key);
                double sub = p.Price * item.Value;
                total += sub;

                Console.WriteLine($"{p.Name} x{item.Value} = ₱{sub:F2}");
            }

            Console.WriteLine($"Total: ₱{total:F2}");
        }

        static void RemoveItem(List<Product> products, Dictionary<int, int> cart)
        {
            int id = ReadInt("Enter ID: ");

            if (!cart.ContainsKey(id))
            {
                Console.WriteLine("Item not in cart.");
                return;
            }

            Product p = FindProduct(products, id);
            p.Stock += cart[id];
            cart.Remove(id);

            Console.WriteLine("Removed.");
        }

        static void UpdateItem(List<Product> products, Dictionary<int, int> cart)
        {
            int id = ReadInt("Enter ID: ");

            if (!cart.ContainsKey(id))
            {
                Console.WriteLine("Item not in cart.");
                return;
            }

            int newQty = ReadInt("New Quantity: ");

            if (newQty <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            Product p = FindProduct(products, id);
            int diff = newQty - cart[id];

            if (diff > 0 && diff > p.Stock)
            {
                Console.WriteLine("Not enough stock.");
                return;
            }

            p.Stock -= diff;
            cart[id] = newQty;

            Console.WriteLine("Updated.");
        }

        static void ClearCart(List<Product> products, Dictionary<int, int> cart)
        {
            foreach (var item in cart)
            {
                Product p = FindProduct(products, item.Key);
                p.Stock += item.Value;
            }

            cart.Clear();
            Console.WriteLine("Cart cleared.");
        }

        // ================= CHECKOUT =================

        static void Checkout(List<Product> products, Dictionary<int, int> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Cart is empty.");
                Pause();
                return;
            }

            Console.Clear();
            Console.WriteLine("=================================");
            Console.WriteLine("         OFFICIAL RECEIPT        ");
            Console.WriteLine("=================================");
            Console.WriteLine($"Receipt No : #{receiptNo:D4}");
            Console.WriteLine($"Date       : {DateTime.Now}");
            Console.WriteLine("---------------------------------");

            double total = 0;

            foreach (var item in cart)
            {
                Product p = FindProduct(products, item.Key);
                double sub = p.Price * item.Value;
                total += sub;

                Console.WriteLine($"{p.Name} x{item.Value} = ₱{sub:F2}");
            }

            double discount = total >= 5000 ? total * 0.10 : 0;
            double finalTotal = total - discount;

            Console.WriteLine("---------------------------------");
            Console.WriteLine($"Subtotal  : ₱{total:F2}");
            Console.WriteLine($"Discount  : ₱{discount:F2}");
            Console.WriteLine($"TOTAL     : ₱{finalTotal:F2}");

            double payment;

            do
            {
                payment = ReadDouble("Enter Payment Amount (₱): ");

                if (payment < finalTotal)
                    Console.WriteLine("ERROR: Insufficient payment.");
            }
            while (payment < finalTotal);

            Console.WriteLine($"Change    : ₱{payment - finalTotal:F2}");

            orderHistory.Add(
                $"Receipt #{receiptNo:D4} | {DateTime.Now} | Total: ₱{finalTotal:F2}"
            );

            receiptNo++;
            cart.Clear();

            Console.WriteLine("\nSTOCK ALERT REPORT");
            Console.WriteLine("---------------------------------");

            bool hasLowStock = false;

            foreach (var p in products)
            {
                if (p.Stock <= 5)
                {
                    Console.WriteLine($"REORDER REQUIRED: {p.Name} | Stock: {p.Stock}");
                    hasLowStock = true;
                }
            }

            if (!hasLowStock)
                Console.WriteLine("All products are sufficiently stocked.");

            string again = ReadYesNo("\nNew transaction? (Y/N): ");

            if (again == "N")
            {
                Console.WriteLine("Thank you!");
                Pause();
                Environment.Exit(0);
            }
        }

        static void ShowHistory()
        {
            Console.Clear();
            Console.WriteLine("===== ORDER HISTORY =====\n");

            if (orderHistory.Count == 0)
                Console.WriteLine("No orders yet.");
            else
                foreach (var o in orderHistory)
                    Console.WriteLine(o + "\n");

            Pause();
        }
    }
}
