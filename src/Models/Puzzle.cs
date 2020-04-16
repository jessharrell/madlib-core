using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;
using madlib_core.DTOs;

namespace madlib_core.Models
{
    public class Puzzle
    {
        public Puzzle(PuzzleDto puzzleDto)
        {
            Title = puzzleDto.Title;
            Id = Guid.NewGuid();
            TextComponents = puzzleDto.Texts == null 
                ? new List<TextComponent>() 
                : puzzleDto.Texts.Select(c => new TextComponent(c)).ToList();
        }

        public Puzzle()
        {
           Id = Guid.NewGuid(); 
        }

        public Puzzle(Guid id)
        {
            Id = id;
        }

        public Dictionary<string,AttributeValue> AsDatabaseValue()
        {
            var databaseValue = new Dictionary<string, AttributeValue>
            {
                {
                    "Title", 
                    new AttributeValue(Title)
                },
                {
                    "Id",
                    new AttributeValue(Id.ToString())
                }
            };
            if (TextComponents.Any())
            {
                databaseValue.Add("Texts",
                    new AttributeValue()
                    {
                        L = TextComponents.Select(c => c.AsAttributeValue()).ToList()
                    }
                );
            }

            return databaseValue;
        }

        public string Title { get; set; }
        public Guid Id { get; }
        public List<TextComponent> TextComponents { get; set; } = new List<TextComponent>();

        public PuzzleDto AsAPuzzleDto()
        {
            return new PuzzleDto()
            {
                Title = Title,
                Texts = TextComponents.Select(t => t.AsATextComponentDto())
            };
        }
    }
}