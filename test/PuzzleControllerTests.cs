using System;
using System.Linq;
using FluentAssertions;
using madlib_core.Controllers;
using madlib_core.Extensions;
using madlib_core.Models;
using madlib_core_tests.Fakes;
using Xunit;

namespace madlib_core_tests
{
    public class PuzzleControllerTests
    {
        [Fact]
        public async void GivenNewPuzzleWhenCreateThenRecordIsAddedToDatabase()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            await controller.Create(new Puzzle());

            Assert.Single(dynamoFake.PutItems);
        }
        
        [Fact]
        public async void GivenNewPuzzleWhenCreateThenRequestedRecordContainsPuzzle()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            var givenPuzzle = new Puzzle();
            await controller.Create(givenPuzzle);

            dynamoFake.PutItems.First().Item.AsAPuzzle().Should().BeEquivalentTo(givenPuzzle);
        }
    }
}
