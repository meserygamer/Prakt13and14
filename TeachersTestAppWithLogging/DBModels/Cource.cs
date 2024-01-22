using System;
using System.Collections.Generic;

namespace TeachersTestAppWithLogging;

public partial class Cource
{
    public int IdCource { get; set; }

    public string Cource1 { get; set; } = null!;

    public int HoursNumber { get; set; }

    public virtual ICollection<TeacherCource> TeacherCources { get; set; } = new List<TeacherCource>();
}
