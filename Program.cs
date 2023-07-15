using System;
using System.Collections.Generic;
namespace OnlineShop { 
class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}

class ShoppingCartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}

class OnlineShoppingApp
{
    static List<Product> products = new List<Product>();
    static Dictionary<int, ShoppingCartItem> shoppingCart = new Dictionary<int, ShoppingCartItem>();
    static Queue<Dictionary<int, ShoppingCartItem>> orderHistory = new Queue<Dictionary<int, ShoppingCartItem>>();

    static void Main()
    {
        Console.WriteLine("Welcome to the Online Shopping App!");

        // Adding some sample products
        AddProduct(1, "Laptop", 1000);
        AddProduct(2, "Smartphone", 500);
        AddProduct(3, "Headphones", 50);

        while (true)
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Product to Cart");
            Console.WriteLine("3. View Shopping Cart");
            Console.WriteLine("4. Checkout");
            Console.WriteLine("5. View Order History");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewProducts();
                    break;
                case "2":
                    AddToCart();
                    break;
                case "3":
                    ViewShoppingCart();
                    break;
                case "4":
                    Checkout();
                    break;
                case "5":
                    ViewOrderHistory();
                    break;
                case "6":
                    Console.WriteLine("Exiting the application.");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void AddProduct(int productId, string name, double price)
    {
        Product newProduct = new Product
        {
            ProductId = productId,
            Name = name,
            Price = price
        };

        products.Add(newProduct);
    }

    static void ViewProducts()
    {
        Console.WriteLine("Available Products:");
        foreach (var product in products)
        {
            Console.WriteLine($"{product.ProductId}. {product.Name} - ${product.Price}");
        }
    }

    static void AddToCart()
    {
        Console.Write("Enter the product ID to add to cart: ");
        if (int.TryParse(Console.ReadLine(), out int productId))
        {
            var product = products.Find(p => p.ProductId == productId);
            if (product != null)
            {
                Console.Write("Enter quantity: ");
                if (int.TryParse(Console.ReadLine(), out int quantity))
                {
                    if (shoppingCart.ContainsKey(productId))
                    {
                        shoppingCart[productId].Quantity += quantity;
                    }
                    else
                    {
                        ShoppingCartItem cartItem = new ShoppingCartItem
                        {
                            Product = product,
                            Quantity = quantity
                        };
                        shoppingCart.Add(productId, cartItem);
                    }

                    Console.WriteLine("Product added to cart!");
                }
                else
                {
                    Console.WriteLine("Invalid quantity. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid product ID.");
        }
    }

    static void ViewShoppingCart()
    {
        Console.WriteLine("Shopping Cart:");
        foreach (var item in shoppingCart)
        {
            Console.WriteLine($"{item.Value.Product.Name} - Quantity: {item.Value.Quantity}, Price: ${item.Value.Product.Price * item.Value.Quantity}");
        }
    }

    static void Checkout()
    {
        if (shoppingCart.Count > 0)
        {
            Console.WriteLine("Checkout successful!");
            orderHistory.Enqueue(new Dictionary<int, ShoppingCartItem>(shoppingCart));
            shoppingCart.Clear();
        }
        else
        {
            Console.WriteLine("Shopping cart is empty. Nothing to checkout.");
        }
    }

    static void ViewOrderHistory()
    {
        Console.WriteLine("Order History:");
        int orderNumber = 1;
        foreach (var order in orderHistory)
        {
            Console.WriteLine($"Order {orderNumber++}:");
            foreach (var item in order)
            {
                Console.WriteLine($"{item.Value.Product.Name} - Quantity: {item.Value.Quantity}, Price: ${item.Value.Product.Price * item.Value.Quantity}");
            }
            Console.WriteLine("-------------");
        }
    }
}
}