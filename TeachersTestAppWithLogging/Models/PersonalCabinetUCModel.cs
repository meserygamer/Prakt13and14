using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeachersTestAppWithLogging.Models
{
    public class PersonalCabinetUCModel
    {
        public string ConvertPhoneNumberTo89Format(string phone)
        {
            StringBuilder ConvertedPhone = new StringBuilder("89");

            int NumberOfDigits = 0;

            foreach(char ch in phone) if(char.IsDigit(ch)) NumberOfDigits++; //Подсчет количества цифр

            int counter = -9 + NumberOfDigits;

            foreach (char ch in phone) if (char.IsDigit(ch)) if(counter-- <= 0) ConvertedPhone.Append(ch);

            return ConvertedPhone.ToString();
        }
    }
}
