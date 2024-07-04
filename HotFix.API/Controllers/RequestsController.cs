using Dapper;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace HotFix.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly string _connectionString;

        public RequestsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = """
            SELECT * FROM requests;
            """;
            var result = await connection.QueryAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = """
            SELECT * FROM requests WHERE id = @Id;
            """;
            var result = await connection.QueryAsync(query, new { Id = id });
            return Ok(result);
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

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = """
            DELETE FROM requests WHERE id = @Id;
            """;
            await connection.ExecuteAsync(query, new { Id = id });

            return Ok();
        }
    }
}
