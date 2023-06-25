using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Linq1.Models;

public partial class Club
{
    [Key] 
    public string Serial { get; set; } = null!;

    public string Entitle { get; set; } = null!;
}
