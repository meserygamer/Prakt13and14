using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersTestAppWithLogging
{
    public partial class UserDatum
    {
        [NotMapped]
        public DateTimeOffset? BirthDayOffset
        {
            get => new DateTimeOffset(Birthdate);
            set
            {
                if (value is not null)
                {
                    Birthdate = ((DateTimeOffset)value).DateTime;
                }
            }
        }
    }
}
