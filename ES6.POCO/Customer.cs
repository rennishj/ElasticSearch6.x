using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES6.POCO
{
    //https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/attribute-mapping.html
    [ElasticsearchType(Name = "customer")]
    public class Document
    {
        [PropertyName("customer_join_field")]
        public JoinField CustomerJoinField { get; set; }
    }
    
    [ElasticsearchType(Name ="customer")]
    public class Customer :Document
    {
        [PropertyName("customerId")]
        public int CustomerId { get; set; }

        [PropertyName("firstName")]
        [Text]
        public string FirstName { get; set; }

        [PropertyName("lastName")]
        [Text]
        public string LastName { get; set; }

        [PropertyName("email")]
        [Keyword]
        public string Email { get; set; }
    }

    [ElasticsearchType(Name = "order")]
    public class Order : Document
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("customerId")]
        public int CustomerId { get; set; }

        //[PropertyName("orderDate")]
        //[Date(Format = "MMddyyyy")]
        //public DateTime? OrderDate { get; set; }

        [PropertyName("orderAmount")]
        public decimal Amount { get; set; }       

        [Nested]
        [PropertyName("packages")]
        public List<Package> Packages { get; set; }

        [Nested]
        [PropertyName("orderItems")]
        public List<OrderItem> OrderItems { get; set; }
    }

    public class Package
    {
        public int PackageId { get; set; }
        public int Qty { get; set; }
        public int OrderId { get; set; }
        public string Weight { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
