using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace HotFix.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : ControllerBase
    {
        private readonly string _connectionString;

        public RequestController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> Create(string description, string address, string type)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = """
            INSERT INTO requests (description, address, type) 
            VALUES (@Description, @Address, @Type);
            """;
            await connection.ExecuteAsync(query, new { Description = description, Address = address, Type = type });

            return Ok();
        }
    }
}
