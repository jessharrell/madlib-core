using System;
using System.Collections.Generic;
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
            return new Dictionary<string, AttributeValue>
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
        }

        public string Title { get; set; }
        public Guid Id { get; }

        public PuzzleDto AsAPuzzleDto()
        {
            return new PuzzleDto()
            {
                Title = Title
            };
        }
    }
}