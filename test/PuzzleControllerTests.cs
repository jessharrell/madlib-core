using System.Linq;
using FluentAssertions;
using madlib_core.Controllers;
using madlib_core.DTOs;
using madlib_core.Extensions;
using madlib_core.Properties;
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

            controller.Create(new PuzzleDto());

            Assert.Single(dynamoFake.PutItems);
        }
        
        [Fact]
        public async void GivenNewPuzzleWhenCreateThenRequestedRecordContainsPuzzle()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            var givenPuzzle = new PuzzleDto{Title = "Incoming puzzle"};
            controller.Create(givenPuzzle);

            dynamoFake.PutItems.First().Item.AsAPuzzle().Title.Should().Be("Incoming puzzle");
        }
        
        [Fact]
        public async void GivenNewPuzzleWhenCreateThenRecordCreatedInPuzzleTable()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            var givenPuzzle = new PuzzleDto{Title = "Incoming puzzle"};
            controller.Create(givenPuzzle);

            dynamoFake.PutItems.First().TableName.Should().Be(PuzzleDatabaseTable.TableName);
        }
    }
}
