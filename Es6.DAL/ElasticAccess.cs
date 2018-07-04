using Elasticsearch.Net;
using ES6.POCO;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es6.DAL
{
    public class ElasticAccess
    {
        public static ElasticClient ESClient
        {
            get
            {
               
                var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
                var settings = new ConnectionSettings(connectionPool)
                                              .DefaultMappingFor<Document>(m => m.IndexName("fa").TypeName("_doc"))
                                              .DefaultMappingFor<Customer>(m => m.IndexName("fa").TypeName("_doc"))
                                              .DefaultMappingFor<Order>(m => m.IndexName("fa").TypeName("_doc"))
                                              .DefaultMappingFor<Package>(m => m.IndexName("fa").TypeName("_doc"))
                                              .DefaultMappingFor<OrderItem>(m => m.IndexName("fa").TypeName("_doc"))
                                              .DefaultMappingFor<Address>(m => m.IndexName("fa").TypeName("_doc"))
                                              .EnableDebugMode()
                                              .PrettyJson();

                return new ElasticClient(settings);
            }
        }

        public static void Search()
        {

            try
            {                
                var result = ESClient.Search<Customer>(s => s
                                      .Query(q => q
                                      .MatchAll()

                                      ) );
                if(result != null && result.Hits.Count > 0)
                {

                }
                                       
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static void ConfigureIndex()
        {
            try
            {
                var createIndexResponse = ESClient.CreateIndex("fa", c => c
                     .Index<Document>()
                     .Mappings(ms => ms
                     .Map<Document>(m => m
                     .RoutingField(r => r.Required())
                     .AutoMap<Customer>()
                     .AutoMap<Order>()
                     .AutoMap<Package>()
                     .AutoMap<OrderItem>()
                     .AutoMap<Address>()
                     .Properties(props => props
                        .Join(j => j
                            .Name(p => p.CustomerJoinField)
                            .Relations(r => r
                                .Join("customer","order,package,orderItem,address")
                                )
                            )                            
                         )                         
                      )                      
             ));

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Purpose: Adds  the Parent Document
        /// https://stackoverflow.com/questions/25933235/set-routing-in-elasticsearch-using-nest-and-attribute-mapping
        /// https://www.elastic.co/guide/en/elasticsearch/client/net-api/current/parent-child-relationships.html // check the Indexing parent or children
        /// </summary>
        /// <param name="cust"></param>
        public static void AddCustomerDocument(Customer cust)
        {
            try
            {
                var result = ESClient.Index<Customer>(cust,
                     c => c
                     .Id(cust.CustomerId)//to avaoid random Ids
                     //.Routing(cust.CustomerId)
                    );

                //var response = ESClient.Index(cust, i => i.Routing(Routing.From(cust)));
            }
            catch ( Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Adds Child Document(Order)
        /// </summary>
        /// <param name="cust"></param>
        public static void AddOrderDocument(Order order)
        {
            try
            {
                //var descriptor = new BulkDescriptor();
                //descriptor.CreateMany<Document>(orders, (bd, o) => bd.Id(o.CustomerId)).Index("fa");
                //var result = ESClient.Bulk(
                //     c => c
                //     .Id(order.CustomerId)//to avaoid random Ids
                //     .Routing(order.CustomerId)
                //    );

                var result = ESClient.Index<Order>(order,
                     c => c
                     .Id(order.OrderId)//to avaoid random Ids
                     .Routing(order.CustomerId)
                    );

                //var response = ESClient.IndexDocument<Order>(order);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #region Search Region

        public static void OrdersByCustomerId(string customerId)
        {
            try
            {
                var searchResult = ESClient.Search<Document>(s => s
                                          .From(0)
                                          .Size(100)
                                          .Type<Order>()
                                          .Query(q =>
                                          q.HasParent<Customer>(c =>
                                                   c.ter

                   ));
                                            


                );
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion Search


    }
}
