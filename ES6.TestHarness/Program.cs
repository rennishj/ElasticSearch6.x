using Es6.DAL;
using ES6.POCO;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES6.TestHarness
{
    class Program
    {
        static void Main(string[] args)
        {
            //ElasticAccess.ConfigureIndex();
            //var cust = GenerateRandomCustomer();
            //ElasticAccess.AddCustomerDocument(cust);
            var customer = GenerateCustomer();
            ElasticAccess.AddCustomerDocument(customer);
            var order = GenerateOrders(customer.CustomerId);
            ElasticAccess.AddOrderDocument(order.ElementAt(0));
            var Packages = GeneratePackages(customer.CustomerId);
            //customer.OrderItems = GenerateOrderItems(customer.CustomerId);
            
            Console.ReadLine();
        }

        private static Customer GenerateCustomer()
        {
            var customer = new Customer()
            {
                CustomerId  =1,
                FirstName = "Rennish",
                LastName = "Joseph",
                Email  ="rennish@yahoo.com",
                CustomerJoinField = JoinField.Root<Customer>()
            };
            return customer;
        }

        private static List<Order> GenerateOrders(int customerId)
        {
            int parentId = customerId;
            var orders = new List<Order>
            {
                new Order{ OrderId  =1, Amount  =34.45m, CustomerId  = customerId,CustomerJoinField  =JoinField.Link<Order>(customerId)},
                new Order{ OrderId  =2, Amount  =20, CustomerId  = customerId,CustomerJoinField  =JoinField.Link<Order>(customerId)},
                new Order{ OrderId  =3, Amount  =10, CustomerId  = customerId,CustomerJoinField  =JoinField.Link<Order>(customerId)}
               // OrderId = 100,
               //// OrderDate = new DateTime(2017, 10, 11),
               // CustomerId = 1,
               // Amount = 23.45m,
               // OrderItems = new List<OrderItem> {

               //     new OrderItem{ OrderItemId  =1,Quantity = 2,UnitPrice = 12.23m},
               //     new OrderItem{ OrderItemId  =2,Quantity = 1,UnitPrice = 10.23m}
               // },
               // Packages = new List<Package>{
               //     new Package{ PackageId  =1,OrderId = 1,Qty =2,Weight  ="2.3"},
               //     new Package{ PackageId  =2,OrderId = 1,Qty =1,Weight  ="2.5"}
               // },
               // CustomerJoinField = JoinField.Link<Order>(parentId)
            };
            return orders;
        }

        private static List<OrderItem> GenerateOrderItems(int customerId)
        {
            int parentId = customerId;
            var orderItems = new List<OrderItem>
            {
                new OrderItem{ OrderId  =1, OrderItemId  =10, Quantity  = 10,UnitPrice = 20.45m,CustomerJoinField  =JoinField.Link<OrderItem>(customerId)},
                new OrderItem{ OrderId  =2, OrderItemId  =2, Quantity  = 1,UnitPrice = 10,CustomerJoinField  =JoinField.Link<OrderItem>(customerId)},
                new OrderItem{ OrderId  =3, OrderItemId  =1, Quantity  = 2,UnitPrice = 2.45m,CustomerJoinField  =JoinField.Link<OrderItem>(customerId)}
            };
            return orderItems;
        }
        private static List<Package> GeneratePackages(int customerId)
        {
            int parentId = customerId;
            var packages = new List<Package>
            {
                new Package { OrderId  =1,PackageId = 1, Qty  = 1, Weight  = "10", CustomerJoinField  =JoinField.Link<Package>(customerId)},
                new Package { OrderId  =2,PackageId = 10, Qty  = 10, Weight  = "05", CustomerJoinField  =JoinField.Link<Package>(customerId)},
                new Package { OrderId  =3,PackageId = 5, Qty  = 15, Weight  = "03", CustomerJoinField  =JoinField.Link<Package>(customerId)}
                
            };
            return packages;
        }
    }
}
