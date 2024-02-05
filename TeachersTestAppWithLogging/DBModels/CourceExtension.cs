using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersTestAppWithLogging
{
    public partial class Cource
    {
        [NotMapped]
        public int PeopleCompleteNumber => TeacherCources.Count;
    }
}
