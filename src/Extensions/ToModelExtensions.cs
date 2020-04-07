using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using madlib_core.Models;

namespace madlib_core.Extensions
{
    public static class ToModelExtensions
    {
        public static Puzzle AsAPuzzle(this Dictionary<string, AttributeValue> dbItem)
        {
            return new Puzzle()
            {
                Title = dbItem["Title"].S
            };
        }
    }
}