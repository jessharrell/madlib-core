using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace madlib_core.Properties
{
    public static class PuzzleDatabaseTable
    {
        public static readonly string TableName = "Puzzle";
        public static readonly CreateTableRequest TableCreationRequest = new CreateTableRequest
        {
            AttributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                    AttributeName = "Id",
                    AttributeType = "S"
                },
                new AttributeDefinition
                {
                    AttributeName = "Title",
                    AttributeType = "S"
                },
            },
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Id",
                    KeyType = "HASH" //Partition key
                },
                new KeySchemaElement
                {
                    AttributeName = "Title",
                    KeyType = "RANGE" //Sort key
                }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 5,
                WriteCapacityUnits = 6
            },
            TableName = TableName
        };
    }
}