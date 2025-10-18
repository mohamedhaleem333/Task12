using ConsoleApp16.Data;
using ConsoleApp16.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp16
{

    internal class Program
    {
        static void Main(string[] args)
        {

            ApplicationDbContext db = new();
            //1-List all customers' first and last names along with their email addresses.
            /*  var Customers = db.Customers.AsQueryable();

            foreach (var item in Customers)
            {
                Console.WriteLine($"Name:{item.FirstName}{item.LastName}  Email:{item.Email}");
            }
           */
            //2- Retrieve all orders processed by a specific staff member (e.g., staff_id = 3).
            /* var orders = db.Orders
              .Where(o => o.StaffId == 3)
              .AsQueryable();

             foreach (var item in orders)
             {
                 Console.WriteLine($"Order ID: {item.OrderId}  Staff ID: {item.StaffId}  Order Date: {item.OrderDate}");
             }
            */
            // 3 - Get all products that belong to a category named "Mountain Bikes".
            /*var category = db.Categories
              .Where(p => p.CategoryName == "Mountain Bikes")
              .AsQueryable();

            foreach (var item in category)
            {
                Console.WriteLine($"id:{item.CategoryId} name:{item.CategoryName}");
            }
            */
            //   4 - Count the total number of orders per store.
            /* var ordersPerStore = db.Orders
              .GroupBy(p => p.StoreId)
              .Select(p => new
              {p.Key,
             cooo =p.Count()

              });

          foreach (var item in ordersPerStore)
          {
              Console.WriteLine($"Store ID: {item.Key}  Total Orders: {item.cooo}");
          }*/
            // 5 - List all orders that have not been shipped yet(shipped_date is null)
            /*var order = db.Orders.
                Where(p => p.ShippedDate == null)
                .AsQueryable();

            foreach (var item in order)
            {
                Console.WriteLine($"id:{item.OrderId}  Date:{item.ShippedDate}");
            }

            */
            //6- Display each customer’s full name and the number of orders they have placed.
            /*  var cousmerandorder = db.Orders.

                Select(o => new
                {
                    o.Customer.FirstName,
                    o.Customer.LastName,
                    OrderCount = db.Orders.Count(c => c.CustomerId == o.CustomerId)
                }
                )
               .Distinct()
                .AsQueryable()
                ;
            foreach (var item in cousmerandorder)
            {
                Console.WriteLine($"Name: {item.FirstName}{item.LastName}   Orders:{item.OrderCount}");
            }

            */
            //7- List all products that have never been ordered (not found in order_items).
            /*   var prodactontorderd = db.Products
                 .Select(p => new
             {
                 p.ProductId,
                 p.OrderItems,
                 p.ProductName

             })
              .Where(p => p.OrderItems==null)
              .AsQueryable();
             foreach (var item in prodactontorderd)
             {
                 Console.WriteLine($"Product ID: {item.ProductId}  Name: {item.ProductName}");
             }
          */
            //8- Display products that have a quantity of less than 5 in any store stock.
            /* var prodactontorderd = db.Stocks
               .Select(p => new
               {
                   p.ProductId,
                   p.Quantity,
                   p.Product.ProductName

               })
            .Where(p => p.Quantity <5 )
            .AsQueryable();
            foreach (var item in prodactontorderd)
            {
                Console.WriteLine($"Product ID:{item.ProductId}   Name:{item.ProductName}  Quantity:{item.Quantity}");
            }
            */
            //9- Retrieve the first product from the products table.
            /*  var product2 = db.Products
               .FirstOrDefault(P => P.ProductName != null);
            Console.WriteLine($"Name: {product2.ProductName}  Price: {product2.ListPrice}");
          */
            //10- Retrieve all products from the products table with a certain model year
            /* var product1 = db.Products
                .Where(p => p.ModelYear == 2016)
                .AsQueryable();
            foreach (var item in product1)
            {
                Console.WriteLine($"Product ID: {item.ProductId}  Name: {item.ProductName}");
            }*/
            //11- Display each product with the number of times it was ordered.
            /*  var product = db.OrderItems
                .GroupBy(p => p.ProductId)
                 .Select(p => new
                 {
                     p.Key,
                     p.FirstOrDefault().Product.ProductName,
                     cooo = p.Count()

                 })
                .ToList();
             foreach (var item in product)
             {
                 Console.WriteLine($"Product: {item.ProductName}, Ordered: {item.cooo} ");
             }*/
            //12- Count the number of products in a specific category.
            /* var catigory = db.Products
                 .Where(p => p.CategoryId == 5)
                 .Count();
             Console.WriteLine($"Category ID: {5}  Total Products: {catigory}");

            */
            //13- Calculate the average list price of products.
            /* var averagePrice = db.Products
                 .Average(x => x.ListPrice);
             Console.WriteLine($"Average List Price of all products: {averagePrice}");

             */
            //14- Retrieve a specific product from the products table by ID.
            /*  var productById = db.Products
               .SingleOrDefault(p => p.ProductId == 5);
          
            Console.WriteLine($"Product ID: {productById.ProductId}  Name: {productById.ProductName}  Price: {productById.ListPrice}");
            */


            //15- List all products that were ordered with a quantity greater than 3 in any order.

            var Productorderd = db.OrderItems.Where(oi => oi.Quantity > 3).Select(oi => new
            {
                oi.ProductId,
                oi.Quantity,
                oi.Product.ProductName

            })
            .Distinct()
            .AsQueryable();
            foreach (var item in Productorderd)
            {
                Console.WriteLine($"id {item.ProductId} , quantity {item.Quantity} , product name {item.ProductName}");
            }

            //16 - Display each staff member’s name and how many orders they processed.

            var staffOrders = db.Staffs.Include(s => s.Orders).Select(s => new
            {
                s.FirstName,
                s.LastName,
                count = s.Orders.Count()


            });
            foreach (var item in staffOrders)
            {
                Console.WriteLine($"Firstname {item.FirstName}, last name {item.LastName} count {item.count}");
            }


            //17 - List active staff members only(active = true) along with their phone numbers.
            var activestaf = db.Staffs.Where(s => s.Active == 1).Select(s => new { s.FirstName, s.LastName, s.Phone }).AsQueryable();
            foreach (var item in activestaf)
            {
                Console.WriteLine($" FirstName{item.FirstName} , LastName{item.LastName} , phone {item.Phone}");
            }


            //18- List all products with their brand name and category name.
            var productandbrandandcatigory = db.Products.Select(e => new
            {
                e.ProductName,
                e.Brand.BrandName,
                e.Category.CategoryName,

            })
            .AsQueryable();


            foreach (var item in productandbrandandcatigory)
            {
                Console.WriteLine($"ProductName: {item.ProductName}  BrandName: {item.BrandName}   CategoryName: {item.CategoryName}");

            }

            //19 - Retrieve orders that are completed.
            var completedOrders = db.Orders.Where(o => o.OrderStatus == 3).ToList();


            //20 - List each product with the total quantity sold (sum of quantity from order_items)
            var productTotals = db.OrderItems.GroupBy(oi => oi.ProductId).Select(g => new
            {
                ProductId = g.Key,
                g.FirstOrDefault().Product.ProductName,

                sum = g.Sum(x => x.Quantity)
            }).AsQueryable();

            foreach (var item in productTotals)
            {
                Console.WriteLine($"Product ID: {item.ProductId}  Name: {item.ProductName}  Total Quantity Sold: {item.sum}");
            }






        }
    }
}
