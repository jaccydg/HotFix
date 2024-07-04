using Dapper;
using HotFix.API.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace HotFix.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InterventionsController : ControllerBase
    {
        private readonly string _connectionString;

        public InterventionsController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = "SELECT * FROM interventions";
            var response = await connection.QueryAsync(query);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = "SELECT * FROM interventions WHERE id = @Id";
            var response = await connection.QueryFirstOrDefaultAsync(query, new { Id = id });

            return Ok(response);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(DateTime interventionDate, string interventionTime, int requestId, int technicianId)
        {
            interventionDate = new DateTime(interventionDate.Year, interventionDate.Month, interventionDate.Day);
            var newInterventionTime = DateTime.Parse(interventionTime);

            using var connection = new NpgsqlConnection(_connectionString);
            string query = """
            INSERT INTO interventions (intervention_date, intervention_time, technician_id, request_id)
            VALUES (@InterventionDate, @InterventionTime, @TechnicianId, @RequestId);
            """;
            await connection.ExecuteAsync(query, new { InterventionDate = interventionDate, InterventionTime = newInterventionTime, TechnicianId = technicianId, RequestId = requestId });

            return Ok();
        }

        //delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string query = "DELETE FROM interventions WHERE id = @Id";
            await connection.ExecuteAsync(query, new { Id = id });

            return Ok();
        }
    }
}
