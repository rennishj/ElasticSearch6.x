using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES6.POCO
{
    [ElasticsearchType(Name ="customer")]
    public class Customer
    {
        [PropertyName("customerId")]
        public int CustomerId { get; set; }

        [PropertyName("firstName")]
        public string FirstName { get; set; }

        [PropertyName("lastName")]
        public string LastName { get; set; }

        [PropertyName("email")]
        public string Email { get; set; }      

    }

    [ElasticsearchType(Name = "customer")]
    public class Order
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("orderDate")]
        public DateTime? OrderDate { get; set; }

        [PropertyName("orderAmount")]
        public decimal Amount { get; set; }

        [PropertyName("packageIds")]
        public List<int> PackageIds { get; set; }

        [PropertyName("orderItemIds")]
        public List<int> OrderItemIds { get; set; }
    }

    public class Package
    {
        public int PackageId { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
    }
}
