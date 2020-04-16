using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using Amazon.DynamoDBv2.Model;
using FluentAssertions;
using madlib_core.DTOs;
using madlib_core.Extensions;
using madlib_core.Models;
using Xunit;

namespace madlib_core_tests
{
    public class PuzzleTests
    {
        private readonly Puzzle _completePuzzle;

        public PuzzleTests()
        {
            _completePuzzle = new Puzzle() {Title = "My Puzzle", TextComponents = new List<TextComponent>{new TextComponent(0, "Hello World")}};
        }
            
        [Fact]
        public void GivenPuzzleWithTitleWhenAsDatabaseValueThenReturnsPuzzleAsDictionary()
        {
            var dictionary = _completePuzzle.AsDatabaseValue();
           
            Assert.True(dictionary.ContainsKey("Title"));
            Assert.Equal(dictionary["Title"].S, "My Puzzle");
            Assert.True(dictionary.ContainsKey("Texts"));
            var firstText = dictionary["Texts"].L.First(); 
            Assert.Equal(firstText.M["Index"].N, "0");
            Assert.Equal(firstText.M["Text"].S ,"Hello World");
        }

        [Fact]
        public void GivenPuzzle_WhenConvertToDBValueAndBackThenOriginalPuzzleReturned()
        {
            var dbValue = _completePuzzle.AsDatabaseValue();
            var roundtripPuzzle = dbValue.AsAPuzzle();
            roundtripPuzzle.Should().BeEquivalentTo(_completePuzzle);
        }

        [Fact]
        public void GivenPuzzleDtoWithTitle_WhenCreate_ThenHasTitle()
        {
            var puzzleDto = new PuzzleDto(){Title= "My Puzzle"};
            var puzzle = new Puzzle(puzzleDto);
            puzzle.Title.Should().Be("My Puzzle");
        }

        [Fact]
        public void GivenPuzzleDtoWithTextComponents_whenCreate_ThenHasTextComponents()
        {
            var puzzleDto = new PuzzleDto(){Texts = new List<TextComponentDto>
                {
                    new TextComponentDto{Index = 1, Text= "bar"},
                    new TextComponentDto{Index = 0, Text="foo"}  
                }
            };
            var puzzle = new Puzzle(puzzleDto);
            puzzle.TextComponents.Count.Should().Be(2);
            puzzle.TextComponents.First(c => c.Index == 0).Text.Should().Be("foo");
            puzzle.TextComponents.First(c => c.Index == 1).Text.Should().Be("bar");
        }

        [Fact]
        public void GivenPuzzleWithoutTexts_WhenAsAPuzzleDto_ThenDtoHasEmptyTextList()
        {
            var puzzle = new Puzzle();
            var dto = puzzle.AsAPuzzleDto();
            dto.Texts.Should().BeEquivalentTo(new List<TextComponentDto>());
        }
    }
}