using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES6.POCO
{
    //https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/attribute-mapping.html
    [ElasticsearchType(Name = "_doc")]
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
        public string Email { get; set; }

        public List<Order> Orders { get; set; }
        public List<Package> Packages { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<ShippingAddress> ShippingAddress { get; set; }
        public List<BillingAddress> BillingAddress { get; set; }
    }

    [ElasticsearchType(Name = "order")]
    public class Order : Document
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("customerId")]
        public int CustomerId { get; set; }        

        [PropertyName("orderAmount")]
        public decimal Amount { get; set; }       

        //[Nested]
        //[PropertyName("packages")]
        //public List<Package> Packages { get; set; }

        //[Nested]
        //[PropertyName("orderItems")]
        //public List<OrderItem> OrderItems { get; set; }
    }

    [ElasticsearchType(Name = "package")]
    public class Package : Document
    {
        public int PackageId { get; set; }
        public int Qty { get; set; }
        public int OrderId { get; set; }
        public string Weight { get; set; }
    }

    [ElasticsearchType(Name = "orderItem")]
    public class OrderItem : Document
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("orderItemId")]
        public int OrderItemId { get; set; }

        [PropertyName("qty")]
        public int Quantity { get; set; }

        [PropertyName("unitPrice")]
        public decimal? UnitPrice { get; set; }
    }

    [ElasticsearchType(Name = "billingaddress")]
    public class BillingAddress :Document
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("address1")]
        public string Address1 { get; set; }

        [PropertyName("city")]
        public string City { get; set; }

        [PropertyName("state")]
        public string State { get; set; }

        [PropertyName("zip")]
        public string Zip { get; set; }
    }

    [ElasticsearchType(Name = "shippingaddress")]
    public class ShippingAddress : Document
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("address1")]
        public string Address1 { get; set; }

        [PropertyName("city")]
        public string City { get; set; }

        [PropertyName("state")]
        public string State { get; set; }

        [PropertyName("zip")]
        public string Zip { get; set; }
    }
}
