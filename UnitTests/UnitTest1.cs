using TeachersTestAppWithLogging.Models;

namespace UnitTests
{
    public class PersonalCabinetUCModelUnitTests
    {
        [Fact]
        public void ConvertPhoneNumberTo89FormatTest1()
        {
            Assert.Equal("89261234567", (new PersonalCabinetUCModel()).ConvertPhoneNumberTo89Format("+79261234567"));
        }

        [Fact]
        public void ConvertPhoneNumberTo89FormatTest2()
        {
            Assert.Equal("89261234567", (new PersonalCabinetUCModel()).ConvertPhoneNumberTo89Format("89261234567"));
        }

        [Fact]
        public void ConvertPhoneNumberTo89FormatTest3()
        {
            Assert.Equal("89261234567", (new PersonalCabinetUCModel()).ConvertPhoneNumberTo89Format("79261234567"));
        }

        [Fact]
        public void ConvertPhoneNumberTo89FormatTest4()
        {
            Assert.Equal("89261234567", (new PersonalCabinetUCModel()).ConvertPhoneNumberTo89Format("+7 926 123 45 67"));
        }

        [Fact]
        public void ConvertPhoneNumberTo89FormatTest5()
        {
            Assert.Equal("89261234567", (new PersonalCabinetUCModel()).ConvertPhoneNumberTo89Format("8(926)123-45-67"));
        }

        [Fact]
        public void ConvertPhoneNumberTo89FormatTest6()
        {
            Assert.Equal("89261234567", (new PersonalCabinetUCModel()).ConvertPhoneNumberTo89Format("+8(926)123-45-67"));
        }
    }
}