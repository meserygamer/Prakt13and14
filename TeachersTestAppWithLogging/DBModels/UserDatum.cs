using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TeachersTestAppWithLogging;

public partial class UserDatum
{
    public int IdUser { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Patronymic { get; set; }

    public int IdGender { get; set; }

    public DateTime Birthdate { get; set; }

    public int Worktime { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public virtual UserGender IdGenderNavigation { get; set; } = null!;

    public virtual UserLogin IdUserNavigation { get; set; } = null!;
}
