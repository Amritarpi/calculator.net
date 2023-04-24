using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YourAppName.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly IMongoClient _mongoClient;

        public CalculatorController(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        [HttpPost]
        public async Task<ActionResult<double>> Calculate(double number1, double number2, string operation)
        {
            // Calculate the result based on the operation
            double result;
            switch (operation)
            {
                case "add":
                    result = number1 + number2;
                    break;
                case "sub":
                    result = number1 - number2;
                    break;
                case "div":
                    result = number1 / number2;
                    break;
                case "mul":
                    result = number1 * number2;
                    break;
                default:
                    return BadRequest("Invalid operation");
            }

            // Save the inputs and result to MongoDB
            var collection = _mongoClient.GetDatabase("YourDatabaseName").GetCollection<BsonDocument>("calculations");
            var document = new BsonDocument
            {
                {"Number1", number1},
                {"Number2", number2},
                {"Operation", operation},
                {"Result", result},
                {"Timestamp", DateTime.UtcNow}
            };
            await collection.InsertOneAsync(document);

            // Return the result
            return result;
        }
    }
}
