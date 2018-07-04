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
        //[PropertyName("customer_join_field")]
        //public JoinField CustomerJoinField { get; set; }
    }
    
    [ElasticsearchType(Name ="customer")]
    public class Customer 
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
        [Text]
        public string Email { get; set; }

        [PropertyName("customer_join_field")]
        public JoinField CustomerJoinField { get; set; }
    }

    [ElasticsearchType(Name = "order")]
    public class Order : Customer
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; } 

        [PropertyName("orderAmount")]
        public decimal Amount { get; set; }

        [PropertyName("orderDate")]
        public string OrderDate { get; set; }       
    }

    [ElasticsearchType(Name = "package")]
    public class Package : Customer
    {
        [PropertyName("packageId")]
        public int PackageId { get; set; }

        [PropertyName("qty")]
        public int Qty { get; set; }

        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("weight")]
        [Text]
        public string Weight { get; set; }
    }

    [ElasticsearchType(Name = "orderItem")]
    public class OrderItem : Customer
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

    [ElasticsearchType(Name = "address")]
    public class Address :Customer
    {
        [PropertyName("orderId")]
        public int OrderId { get; set; }

        [PropertyName("address1")]
        [Text]
        public string Address1 { get; set; }

        [PropertyName("city")]
        [Text]
        public string City { get; set; }

        [PropertyName("state")]
        [Text]
        public string State { get; set; }

        [PropertyName("zip")]
        [Text]
        public string Zip { get; set; }

        [PropertyName("addressType")]
        [Text]
        public string AddressType { get; set; }
    }

    public class SearchResult
    {
        [PropertyName("customer")]
        public Customer Customer { get; set; }

        [PropertyName("order")]
        public List<Order> Orders { get; set; }

        [PropertyName("orderItem")]

        public List<OrderItem> OrderItems { get; set; }
    }
    
}
