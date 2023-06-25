using Linq1.Data;
using Linq1.Models;
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
        [HttpGet("Query4")]
        public IActionResult Query4()
        {
            var Query4 = (
          from c in clubdb.Courses
          join f in clubdb.Faculties on c.FacultyId equals f.Id into newlist
          from n in newlist.DefaultIfEmpty()
          select new
          {
              CourseName = c.Title,
              FacultyName = n.Name
          }
          ).ToList();
            var name = from q in Query4 select q;
            return Ok(name);

        }
        [HttpGet("Query5")]
        public IActionResult Query5()
        {
            var Query5= clubdb.Courses.ToList();
            var name = from q in Query5 orderby q.Credits descending select q;
            return Ok(Query5);
        }
        [HttpGet("Query6")]
        public IActionResult Query6()
        {
            var Query6 = (
            from c in clubdb.Courses
            join f in clubdb.Faculties on c.FacultyId equals f.Id
            join i in clubdb.Instructors on f.SupervisorId equals i.Id into newlist
            from n in newlist.DefaultIfEmpty()
            select new
            {
                CourseName = c.Title,
                InstructorName = n.Name
            }
        ).ToList();
            var countbycourse = (
            from q in Query6
            group q by q.CourseName into g
            select new
            {
             CourseName = g.Key,
             InstructorCount = g.Count()
            }
        ).ToList();

            var result = from q in countbycourse select q;

            return Ok(result);


        }
    }
    }

