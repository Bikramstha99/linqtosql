namespace Linq1.Models
{
    public class CourseAssignments
    {
        public int InstructorId { get; set; }  
        public Instructor? Instructor { get; set; }
        public int CourseID { get; set; }
        public Course? Course { get; set;}
    }
}
