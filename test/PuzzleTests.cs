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
        [Fact]
        public void GivenPuzzleWithTitleWhenAsDatabaseValueThenReturnsPuzzleAsDictionary()
        {
            var puzzle = new Puzzle() {Title = "My Puzzle"};
            var dictionary = puzzle.AsDatabaseValue();
            Assert.True(dictionary.ContainsKey("Title"));
            Assert.Equal(dictionary["Title"].S, "My Puzzle");
        }

        [Fact]
        public void GivenPuzzle_WhenConvertToDBValueAndBackThenOriginalPuzzleReturned()
        {
            var puzzle = new Puzzle(){Title = "My Puzzle"};
            var dbValue = puzzle.AsDatabaseValue();
            var roundtripPuzzle = dbValue.AsAPuzzle();
            roundtripPuzzle.Should().BeEquivalentTo(puzzle);
        }

        [Fact]
        public void GivenPuzzleDtoWithTitle_WhenCreate_ThenHasTitle()
        {
            var puzzleDto = new PuzzleDto(){Title= "My Puzzle"};
            var puzzle = new Puzzle(puzzleDto);
            puzzle.Title.Should().Be("My Puzzle");
        }
    }
}