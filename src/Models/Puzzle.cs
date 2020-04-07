using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace madlib_core.Models
{
    public class Puzzle
    {
        public Dictionary<string,AttributeValue> AsDatabaseValue()
        {
            return new Dictionary<string, AttributeValue>
            {
                {
                    "Title", 
                    new AttributeValue(Title)
                }
            };
        }

        public string Title { get; set; }
    }
}