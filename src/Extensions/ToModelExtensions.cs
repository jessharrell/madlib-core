using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;
using madlib_core.Models;

namespace madlib_core.Extensions
{
    public static class ToModelExtensions
    {
        public static Puzzle AsAPuzzle(this Dictionary<string, AttributeValue> dbItem)
        {
            return new Puzzle(Guid.Parse(dbItem["Id"].S))
            {
                Title = dbItem["Title"].S,
                TextComponents = dbItem.ContainsKey("Texts" ) 
                    ? dbItem["Texts"].L.Select(t => t.M.AsATextComponent()).ToList()
                    : new List<TextComponent>()
            };
        }

        private static TextComponent AsATextComponent(this Dictionary<string, AttributeValue> dbTextItem)
        {
            return new TextComponent( int.Parse(dbTextItem["Index"].N), dbTextItem["Text"].S );
        }
    }
}