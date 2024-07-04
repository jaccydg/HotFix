using Dapper;
using HotFix.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace HotFix.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TechniciansController : Controller
    {
        private readonly string _connectionString;

        public TechniciansController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        [HttpGet("")]
        public async Task<ActionResult<List<Technician>>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = "SELECT * FROM technicians";
            var response = await connection.QueryAsync<Technician>(query);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Technician>> GetById([FromRoute] int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = "SELECT * FROM technicians WHERE id = @Id";
            var response = await connection.QueryFirstOrDefaultAsync<Technician>(query, new { Id = id });

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(string name, string specialization, int experience, bool availability)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = """
            INSERT INTO technicians (name, specialization, experience, availability)
            VALUES (@Name, @Specialization, @Experience, @Availability);
            """;
            await connection.ExecuteAsync(query, new { Name = name, Specialization = specialization, Experience = experience, Availability = availability });

            return Ok();
        }

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = "DELETE FROM technicians WHERE id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });

            return Ok();
        }
    }
}
