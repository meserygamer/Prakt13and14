using System;
using System.Collections.Generic;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class PersonalCabinetUCViewModel : ViewModelBase
	{
        public PersonalCabinetUCViewModel(int userID, IProjectDataSource source) 
        {
            _source = source;
            SetUserDatum(userID);
        }


        public UserDatumReactive UserData
        {
            get => _userData;
            set => this.RaiseAndSetIfChanged(ref _userData, value);
        }


        private UserDatumReactive _userData;

        private IProjectDataSource _source;


        private async void SetUserDatum(int userID) => _userData = new UserDatumReactive(await _source.FindUserByIdAsync(userID));

    }

	public class UserDatumReactive : ReactiveObject
	{
		public UserDatumReactive(UserDatum uData) => _userDatum = uData;


        public int IdUser 
        {
            get => _userDatum.IdUser;
            set
            {
                _userDatum.IdUser = value;
                this.RaisePropertyChanged();
            }
        }

        public string Surname 
        {
            get => _userDatum.Surname;
            set
            {
                _userDatum.Surname = value;
                this.RaisePropertyChanged();
            }
        }

        public string Name
        {
            get => _userDatum.Name;
            set
            {
                _userDatum.Name = value;
                this.RaisePropertyChanged();
            }
        }

        public string? Patronymic
        {
            get => _userDatum.Patronymic;
            set
            {
                _userDatum.Patronymic = value;
                this.RaisePropertyChanged();
            }
        }

        public int IdGender
        {
            get => _userDatum.IdGender;
            set
            {
                _userDatum.IdGender = value;
                this.RaisePropertyChanged();
            }
        }

        public DateTime Birthdate
        {
            get => _userDatum.Birthdate;
            set
            {
                _userDatum.Birthdate = value;
                this.RaisePropertyChanged();
            }
        }

        public int Worktime
        {
            get => _userDatum.Worktime;
            set
            {
                _userDatum.Worktime = value;
                this.RaisePropertyChanged();
            }
        }

        public string Email
        {
            get => _userDatum.Email;
            set
            {
                _userDatum.Email = value;
                this.RaisePropertyChanged();
            }
        }

        public string? PhoneNumber
        {
            get => _userDatum.PhoneNumber;
            set
            {
                _userDatum.PhoneNumber = value;
                this.RaisePropertyChanged();
            }
        }

        public virtual UserGender IdGenderNavigation
        {
            get => _userDatum.IdGenderNavigation;
            set
            {
                _userDatum.IdGenderNavigation = value;
                this.RaisePropertyChanged();
            }
        }

        public virtual UserLogin IdUserNavigation
        {
            get => _userDatum.IdUserNavigation;
            set
            {
                _userDatum.IdUserNavigation = value;
                this.RaisePropertyChanged();
            }
        }


        private readonly UserDatum _userDatum;
	}
}