using Linq1.Data;
using Linq1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
            var Query5 = clubdb.Courses.ToList();
            var name = from q in Query5 orderby q.Credits descending select q;
            return Ok(Query5);
        }
        [HttpGet("Query6")]
        public IActionResult Query6()
        {
            var Query6 = (
            from c in clubdb.Courses
            join ca in clubdb.CourseAssignments on c.Id equals ca.CourseID
            join i in clubdb.Instructors on ca.InstructorId equals i.Id into newlist
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
            return Ok(countbycourse);


        }
        [HttpGet("Query7")]
        public IActionResult Query7()
        {
            var Query7 = (
          from c in clubdb.Courses
          join f in clubdb.Faculties on c.FacultyId equals f.Id
          join i in clubdb.Instructors on f.SupervisorId equals i.Id into newlist
          from n in newlist.DefaultIfEmpty()
          select new
          {
              FacultyName = c.Title,
              InstructorName = n.Name
          }
        ).ToList();
            var name = from q in Query7 select q;
            return Ok(name);
        }
        [HttpGet("Query8")]
        public IActionResult Query8()
        {
            var Query7 = (
         from c in clubdb.Courses
         join e in clubdb.Enrollments on c.Id equals e.CourseId
         join s in clubdb.Students on e.StudentId equals s.Id into newlist
         from n in newlist.DefaultIfEmpty()
         select new
         {
             Title = c.Title,
             StudentName = n.Name
         }
       ).ToList();
            var name = from q in Query7 select q;
            return Ok(name);
        }
        [HttpGet("Query9")]
        public IActionResult Query9()
        {
            var Query9 = (
        from c in clubdb.Courses
        join e in clubdb.Enrollments on c.Id equals e.CourseId
        select new
        {
            Course= c.Title,
            Marks = e.Marks
        }
        ).ToList();
            var name = (from q in Query9
                        group q by q.Course into g
                        select new
                        {
                            Course = from q in g select q.Course,
                            MinMarks = (from q in g select q.Marks).Min(),
                            MaxMarks = (from q in g select q.Marks).Max(),
                            AvgMarks = (from q in g select q.Marks).Average(),
                        }
        );
            return Ok(name);


        }
        [HttpGet("Query10")]
        
        [HttpGet("Query11")]
        public IActionResult Query11()
        {
            var Query11 = (
        from cl in clubdb.Clubs
        join s in clubdb.Students on cl.Serial equals s.ClubId into newlist
        from n in newlist.DefaultIfEmpty()
        select new
        {
            Club = cl.Entitle,
            Student = n.Name
        }
        ).ToList();
            var name1 = from q in Query11
                       group q by q.Club into g
                       select new
                       {
                           club = from q in g select q.Club,
                           noofstu = (from q in g select q.Student).Count(),

                        };
            return Ok(name1);

        }
        [HttpGet("Query12")]
        public IActionResult Query12()
        {
            var Query12 = (
        from s in clubdb.Students
        join e in clubdb.Enrollments on s.Id equals e.StudentId into newlist
        from n in newlist.DefaultIfEmpty()
        select new
        {
            courseid = n.CourseId,
            studentname =s.Name,
            marks=n.Marks
        }
        ).ToList();
            var name1 = from q in Query12
                        join c in clubdb.Courses on q.courseid equals c.Id into newlist
                        from n in newlist.DefaultIfEmpty()
                        select new
                        {
                            studentname=from q in Query12 select q.studentname,
                            marks=from q in Query12 select q.marks,
                            coursename = from q in newlist select q.Title
                        };

                        
            return Ok(name1);

        }
        [HttpGet("Query13")]
        public IActionResult Query13()
        {

            var Query13 = (
        from s in clubdb.Students
        join cl in clubdb.Clubs on s.ClubId equals cl.Serial into newlist
        from n in newlist.DefaultIfEmpty()
        select new
        {
            studentid=s.Id,
            Name= s.Name,
            clubname=n.Entitle
        }).ToList();

            var name1 = from q in Query13
                        join e in clubdb.Enrollments on q.studentid equals e.StudentId into newlist
                        from n in newlist.DefaultIfEmpty()
                        select new
                        {
                            Courseid= n.CourseId,
                            Name = q.Name,
                            clubname = q.clubname,
                        };
            var name2 = from q in name1
                        join c in clubdb.Courses on q.Courseid equals c.Id into newlist
                        from n in newlist.DefaultIfEmpty()
                        select new
                        {
                            facultyid=n.FacultyId,
                            Name = q.Name,
                            clubname = q.clubname,
                            coursesname = n.Title
                        };
            var name = from q in name2
                       join f in clubdb.Faculties on q.facultyid equals f.Id into newlist
                       from n in newlist.DefaultIfEmpty()
                       select new
                       {
                           Name = q.Name,
                           clubname = q.clubname,
                           coursename = q.coursesname,
                           facultyname = n.Name
                       };
                        

            return Ok(name);

        }
        [HttpGet("Query14")]
        public IActionResult Query14()
        {
            var Query12 = (
        from s in clubdb.Students
        join e in clubdb.Enrollments on s.Id equals e.StudentId into newlist
        from n in newlist.DefaultIfEmpty()
        select new
        {
            studentid= s.Id,
            courseid = n.CourseId,
            studentname = s.Name,
            marks = n.Marks
        }
        ).ToList();
            var name = from q in Query12
                       group q by q.studentname into newlist
                       select new
                       {
                           courseId= from q in newlist select q.courseid,
                           studentname = from q in newlist select q.studentname,
                           maxmark = (from q in newlist select q.marks).Max(),
                           minmarks = (from q in newlist select q.marks).Min(),
                       };
            var name1 = from q in name
                        join e in clubdb.Enrollments on q.maxmark equals e.Marks
                        join c in clubdb.Courses on e.CourseId equals c.Id into g
                        select new
                        {
                            studentname = q.studentname,
                            maxmarks = q.maxmark,
                            minmarks = q.minmarks,
                            highsubject = from q in g select q.Title,


                        };
            var name2 = from q in name1
                        join e in clubdb.Enrollments on q.minmarks equals e.Marks
                        join c in clubdb.Courses on e.CourseId equals c.Id into g
                        select new
                        {
                            studentname = q.studentname,
                            maxmarks = q.maxmarks,
                            minmarks = q.minmarks,
                            highsubject =q.highsubject,
                            lowsubject=from q in g select q.Title,


                        };
            return Ok(name2);
        }

    }
}

