using Dapper;
using Microsoft.AspNetCore.Mvc;
using StudentRegAPI.Models.Entities;
using StudentRegAPI.Services.IServices;
using System.Data;

namespace StudentRegAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public StudentsController(IDbService dbService)
        {
            _dbConnection = dbService.ConnectDatabase();
        }

        // GET: api/Students/list?pageNumber=1&pageSize=10
        [HttpGet("list")]
        public async Task<IActionResult> Get(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest(
                    "Page number must be greater than 0 and page size must be between 1 and 100.");
            }

            const string countQuery = @"
                SELECT COUNT(*)
                FROM Students;";

            const string query = @"
                SELECT *
                FROM Students
                ORDER BY Id
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;";

            var totalRecords =
                await _dbConnection.ExecuteScalarAsync<int>(countQuery);

            var students = await _dbConnection.QueryAsync<Student>(
                query,
                new
                {
                    Offset = (pageNumber - 1) * pageSize,
                    PageSize = pageSize
                });

            return Ok(new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(
                    (double)totalRecords / pageSize),
                Data = students
            });
        }

        // GET: api/Students/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            const string query = @"
                SELECT *
                FROM Students
                WHERE Id = @Id;";

            var student = await _dbConnection.QueryFirstOrDefaultAsync<Student>(
                query,
                new { Id = id });

            if (student == null)
            {
                return NotFound(new
                {
                    Message = $"Student with ID {id} was not found."
                });
            }

            return Ok(student);
        }

        // POST: api/Students/add
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] Student student)
        {
            const string query = @"
                INSERT INTO Students
                    (StudetCardID, Name, Father, Mother, DOB)
                OUTPUT INSERTED.*
                VALUES
                    (@StudetCardID, @Name, @Father, @Mother, @DOB);";

            var newStudent = await _dbConnection.QuerySingleAsync<Student>(
                query,
                student);

            return CreatedAtAction(
                nameof(GetById),
                new { id = newStudent.Id },
                newStudent);
        }

        // PUT: api/Students/edit/5
        [HttpPut("edit/{id:int}")]
        public async Task<IActionResult> Edit(
            int id,
            [FromBody] Student student)
        {
            const string query = @"
                UPDATE Students
                SET
                    StudetCardID = @StudetCardID,
                    Name = @Name,
                    Father = @Father,
                    Mother = @Mother,
                    DOB = @DOB
                WHERE Id = @Id;";

            var affectedRows = await _dbConnection.ExecuteAsync(
                query,
                new
                {
                    Id = id,
                    student.StudetCardID,
                    student.Name,
                    student.Father,
                    student.Mother,
                    student.DOB
                });

            if (affectedRows == 0)
            {
                return NotFound(new
                {
                    Message = $"Student with ID {id} was not found."
                });
            }

            return Ok(new
            {
                Message = "Student updated successfully."
            });
        }

        // DELETE: api/Students/delete/5
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            const string query = @"
                DELETE FROM Students
                WHERE Id = @Id;";

            var affectedRows = await _dbConnection.ExecuteAsync(
                query,
                new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound(new
                {
                    Message = $"Student with ID {id} was not found."
                });
            }

            return Ok(new
            {
                Message = "Student deleted successfully."
            });
        }
    }
}