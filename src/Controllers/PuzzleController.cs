using System.Threading;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using madlib_core.DTOs;
using madlib_core.Models;
using Microsoft.AspNetCore.Mvc;

namespace madlib_core.Controllers
{
    public class PuzzleController
    {
        private readonly IAmazonDynamoDB _dynamoClient;
        
        public PuzzleController(IAmazonDynamoDB dynamoClient)
        {
            _dynamoClient = dynamoClient;
        }

        public async Task<IActionResult> Create(PuzzleDto puzzleDto)
        {
            var puzzle = new Puzzle(puzzleDto);
            var request = new PutItemRequest("foo", puzzle.AsDatabaseValue());
            await _dynamoClient.PutItemAsync(request, CancellationToken.None);
            return new AcceptedResult();
        }
    }
}