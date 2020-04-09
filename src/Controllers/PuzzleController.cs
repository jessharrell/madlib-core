using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using madlib_core.Database;
using madlib_core.DTOs;
using madlib_core.Models;
using madlib_core.Properties;
using Microsoft.AspNetCore.Mvc;

namespace madlib_core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PuzzleController
    {
        private readonly IAmazonDynamoDB _dynamoClient;
        
        public PuzzleController(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }

        [HttpGet("all")]
        public IEnumerable<PuzzleDto> Get()
        {
            return new List<PuzzleDto>{new PuzzleDto{Title = "Foo"}}.ToArray();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(PuzzleDto puzzleDto)
        {
            await DatabaseHelper.EnsureTableExists(_dynamoClient, PuzzleDatabaseTable.TableName, PuzzleDatabaseTable.TableCreationRequest);
            await CreatePuzzleRecordAsync(puzzleDto);
            return new AcceptedResult();
        }

        private async Task CreatePuzzleRecordAsync(PuzzleDto puzzleDto)
        {
            var puzzle = new Puzzle(puzzleDto);
            var request = new PutItemRequest(PuzzleDatabaseTable.TableName, puzzle.AsDatabaseValue());
            await _dynamoClient.PutItemAsync(request, CancellationToken.None);
        }
    }
}