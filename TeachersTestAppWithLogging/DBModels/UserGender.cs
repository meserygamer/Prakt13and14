using System;
using System.Collections.Generic;

namespace TeachersTestAppWithLogging;

public partial class UserGender
{
    public int IdGender { get; set; }

    public string Gender { get; set; } = null!;

    public virtual ICollection<UserDatum> UserData { get; set; } = new List<UserDatum>();


    public override string ToString() => Gender;
}
