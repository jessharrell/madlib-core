using System;
using madlib_core.Controllers;
using madlib_core.Models;
using madlib_core_tests.Fakes;
using Xunit;

namespace madlib_core_tests
{
    public class PuzzleControllerTests
    {
        [Fact]
        public void GivenNewPuzzleWhenCreateThenRecordIsAddedToDatabase()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            controller.Create(new Puzzle());

            Assert.Single(dynamoFake.PutItems);
        }
    }
}
