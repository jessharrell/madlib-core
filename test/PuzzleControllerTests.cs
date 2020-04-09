using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.Model;
using FluentAssertions;
using madlib_core.Controllers;
using madlib_core.DTOs;
using madlib_core.Extensions;
using madlib_core.Models;
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

            await controller.Create(new PuzzleDto());

            Assert.Single(dynamoFake.PutItems);
        }
        
        [Fact]
        public async void GivenNewPuzzleWhenCreateThenRequestedRecordContainsPuzzle()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            var givenPuzzle = new PuzzleDto{Title = "Incoming puzzle"};
            await controller.Create(givenPuzzle);

            dynamoFake.PutItems.First().Item.AsAPuzzle().Title.Should().Be("Incoming puzzle");
        }
        
        [Fact]
        public async void GivenNewPuzzleWhenCreateThenRecordCreatedInPuzzleTable()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            await controller.Create(new PuzzleDto{Title = "Incoming puzzle"});

            dynamoFake.PutItems.First().TableName.Should().Be(PuzzleDatabaseTable.TableName);
        }

        [Fact]
        public async void GivenNoPuzzleTableWhenCreateThenPuzzleTableIsCreated()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            await controller.Create(new PuzzleDto{Title = "Incoming puzzle"});

            dynamoFake.CreatedTables.Count(r => r.TableName == PuzzleDatabaseTable.TableName).Should().Be(1);
        }
        
        [Fact]
        public async void GivenPuzzleTableAlreadyCreatedWhenCreateThenPuzzleTableIsNotRecreated()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);
            
            dynamoFake.CreatedTables.Add(new CreateTableRequest(PuzzleDatabaseTable.TableName, new List<KeySchemaElement>()));
            await controller.Create(new PuzzleDto{Title = "Incoming puzzle"});

            dynamoFake.CreatedTables.Count(r => r.TableName == PuzzleDatabaseTable.TableName).Should().Be(1);
        }

        [Fact]
        public async void GivenOnePuzzleInTableAlreadyWhenAllThenReturnsSinglePuzzle()
        {
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            dynamoFake.Table.Add(PuzzleDatabaseTable.TableName, new List<Dictionary<string, AttributeValue>>());
            dynamoFake.Table[PuzzleDatabaseTable.TableName].Add(new Puzzle(){Title = "One Puzzle"}.AsDatabaseValue());

            var allPuzzleResponse = (await controller.All()).ToList();

            allPuzzleResponse.Count().Should().Be(1);
            allPuzzleResponse.First().Title.Should().Be("One Puzzle");
        }

        [Fact]
        public async void GivenNoPuzzleTableWhenAllThenCreateTableAndReturnsEmptyArray()
        {
            
            var dynamoFake = new DynamoClientFake();
            var controller = new PuzzleController(dynamoFake);

            var allPuzzleResponse = (await controller.All()).ToList();

            dynamoFake.CreatedTables.Count(r => r.TableName == PuzzleDatabaseTable.TableName).Should().Be(1);
            allPuzzleResponse.Count().Should().Be(0);
        }
    }
}
