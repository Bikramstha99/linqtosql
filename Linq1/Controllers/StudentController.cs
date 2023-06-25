using Linq1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Linq1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ClubDb clubdb;

        public StudentController(ClubDb clubdb)
        {
            this.clubdb = clubdb;
        }

        [HttpGet("Query1")]
        public IActionResult Query1()
        {
            var faculties = clubdb.Faculties.ToList();
            var name = (from f in faculties select f.Name).Count();
            return Ok(name);
        }
        [HttpGet("Query2")]
        public IActionResult Query2()
        {
            var Query2 = (
            from f in clubdb.Faculties
            join i in clubdb.Instructors on f.SupervisorId equals i.Id
            select new
        {
            FacultyName = f.Name,
            InstructorName = i.Name
        }
        ).ToList();
            var name = from q in Query2 select q;
            return Ok(name);
        }
        [HttpGet("Query3")]
        public IActionResult Query3()
        {
            var Query3 = (
           from f in clubdb.Faculties
           join i in clubdb.Instructors on f.SupervisorId equals i.Id into newlist
           from n in newlist.DefaultIfEmpty()
           select new
           {
               FacultyName = f.Name,
               InstructorName = n.Name
           }
           ).ToList();
            var name = from q in Query3 select q;
            return Ok(name);

        }
        }
    }

