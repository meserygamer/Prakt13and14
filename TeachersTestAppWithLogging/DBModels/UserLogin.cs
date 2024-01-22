using System;
using System.Collections.Generic;

namespace TeachersTestAppWithLogging;

public partial class UserLogin
{
    public int IdUser { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdRole { get; set; }

    public virtual UserRole IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<TeacherCource> TeacherCources { get; set; } = new List<TeacherCource>();

    public virtual UserDatum? UserDatum { get; set; }
}
