using System;
using System.Collections.Generic;

namespace Linq1.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }

    public string ClubId { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
