using System;
using System.Collections.Generic;

namespace TeachersTestAppWithLogging;

public partial class TeacherCource
{
    public int IdRecord { get; set; }

    public int IdTeacher { get; set; }

    public int IdCource { get; set; }

    public virtual Cource IdCourceNavigation { get; set; } = null!;

    public virtual UserLogin IdTeacherNavigation { get; set; } = null!;
}
