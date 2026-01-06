using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly string _connectionString;

    public EmployeesController(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("Default")!;
    }

    // GET: api/employees
    [HttpGet]
    public IActionResult GetAll()
    {
        var list = new List<object>();

        using var con = new SqliteConnection(_connectionString);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText = "SELECT Id, Name, Position FROM Employees";

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            list.Add(new
            {
                id = reader.GetInt32(0),
                name = reader.GetString(1),
                position = reader.GetString(2)
            });
        }

        return Ok(list);
    }

    // POST: api/employees
    [HttpPost]
    public IActionResult Add([FromBody] EmployeeDto emp)
    {
        using var con = new SqliteConnection(_connectionString);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText =
            "INSERT INTO Employees (Name, Position) VALUES ($name, $position)";
        cmd.Parameters.AddWithValue("$name", emp.Name);
        cmd.Parameters.AddWithValue("$position", emp.Position);

        cmd.ExecuteNonQuery();

        return Ok();
    }

    // PUT: api/employees/{id}
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] EmployeeDto emp)
    {
        using var con = new SqliteConnection(_connectionString);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText =
            "UPDATE Employees SET Name=$name, Position=$position WHERE Id=$id";
        cmd.Parameters.AddWithValue("$name", emp.Name);
        cmd.Parameters.AddWithValue("$position", emp.Position);
        cmd.Parameters.AddWithValue("$id", id);

        cmd.ExecuteNonQuery();

        return Ok();
    }

    // DELETE: api/employees/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        using var con = new SqliteConnection(_connectionString);
        con.Open();

        var cmd = con.CreateCommand();
        cmd.CommandText = "DELETE FROM Employees WHERE Id=$id";
        cmd.Parameters.AddWithValue("$id", id);

        cmd.ExecuteNonQuery();

        return Ok();
    }
}

// Simple DTO (kept minimal on purpose)
public class EmployeeDto
{
    public string Name { get; set; } = "";
    public string Position { get; set; } = "";
}
