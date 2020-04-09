using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using madlib_core.Properties;

namespace madlib_core.Database
{
    public static class DatabaseHelper
    {
        public static async Task EnsureTableExists(IAmazonDynamoDB client,
            string tableName, CreateTableRequest creationRequest)
        {
            try
            {
                var request = new DescribeTableRequest
                {
                    TableName = tableName
                };

                await client.DescribeTableAsync(request);
                Console.WriteLine("\n*** {0} table already exists ***", tableName);
            }
            catch (ResourceNotFoundException e)
            {
                await client.CreateTableAsync(creationRequest);
                Console.WriteLine("\n*** Created {0} table ***", tableName);
            }
        }
    }
}