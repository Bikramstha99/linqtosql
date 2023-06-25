using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Linq1.Models;

public partial class Faculty
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? SupervisorId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
