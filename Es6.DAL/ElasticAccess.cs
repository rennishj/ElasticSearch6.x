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
                                              .DefaultMappingFor<Document>(i => i
                                              .IndexName("fa")
                                              .TypeName("customer"))
                                              .DefaultMappingFor<Customer>(i => i
                                              .IndexName("fa")
                                              .TypeName("customer"))                                              
                                              .DefaultMappingFor<Order>(i => i
                                              .IndexName("fa")
                                              .TypeName("customer"))                                              
                                              .EnableDebugMode()
                                              .PrettyJson()
                                              .RequestTimeout(TimeSpan.FromMinutes(2));
                                              
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
                     .Properties(props => props
                        .Join(j => j
                            .Name(p => p.CustomerJoinField)
                            .Relations(r => r
                                .Join<Customer, Order>()
                                )
                            )
                         )
                      )
                  )
             );

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
                var result = ESClient.Index<Document>(cust,
                     c => c
                     .Id(cust.CustomerId)//to avaoid random Ids
                     .Routing(cust.CustomerId)
                    );
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

                var result = ESClient.Index<Document>(order,
                     c => c
                     .Id(order.CustomerId)//to avaoid random Ids
                     .Routing(order.CustomerId)
                    );

                //var response = ESClient.IndexDocument<Order>(order);
            }
            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
