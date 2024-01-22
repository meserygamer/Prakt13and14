using System;
using System.Collections.Generic;

namespace TeachersTestAppWithLogging;

public partial class UserRole
{
    public int IdRole { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();
}
