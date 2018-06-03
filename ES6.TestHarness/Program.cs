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
            var orders = GenerateRandomOrders();            
            ElasticAccess.AddOrderDocument(orders);
            Console.ReadLine();
        }

        private static Customer GenerateRandomCustomer()
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

        private static Order GenerateRandomOrders()
        {
            int parentId = 1;
            var orders = new Order
            {
                OrderId = 1,
               // OrderDate = new DateTime(2017, 10, 11),
                CustomerId = 1,
                Amount = 23.45m,
                OrderItems = new List<OrderItem> {

                    new OrderItem{ OrderItemId  =1,Quantity = 2,UnitPrice = 12.23m},
                    new OrderItem{ OrderItemId  =2,Quantity = 1,UnitPrice = 10.23m}
                },
                Packages = new List<Package>{
                    new Package{ PackageId  =1,OrderId = 1,Qty =2,Weight  ="2.3"},
                    new Package{ PackageId  =2,OrderId = 1,Qty =1,Weight  ="2.5"}
                },
                CustomerJoinField = JoinField.Link<Order>(parentId)
            };
            return orders;
        }
    }
}
