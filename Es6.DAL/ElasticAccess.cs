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
                var connection = new InMemoryConnection();
                var connectionPool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
                var settings = new ConnectionSettings(connectionPool, connection)
                                              .DefaultMappingFor<Customer>(i => i
                                              .IndexName("fa")
                                              .TypeName("customer")
                                              )
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

        
    }
}
