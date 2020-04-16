using System.Collections.Generic;
using System.Net.Mime;
using Amazon.DynamoDBv2.Model;
using madlib_core.DTOs;

namespace madlib_core.Models
{
    public class TextComponent
    {
        public TextComponent(int index, string text)
        {
            Index = index;
            Text = text;
        }
        public TextComponent(TextComponentDto textComponentDto)
        {
            Index = textComponentDto.Index;
            Text = textComponentDto.Text;
        }

        public int Index { get; set; }
        public string Text { get; set; }

        public AttributeValue AsAttributeValue()
        {
            return new AttributeValue()
            {
                M = new Dictionary<string, AttributeValue>
                {
                    {"Index", new AttributeValue{N = Index.ToString()}},
                    {"Text", new AttributeValue(Text)}
                }
            };
        }

        public TextComponentDto AsATextComponentDto()
        {
            return new TextComponentDto
            {
                Index = Index,
                Text = Text
            };
        }
    }
}