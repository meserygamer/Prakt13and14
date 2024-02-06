using System;
using System.Collections.Generic;
using System.Reactive;
using DynamicData.Binding;
using Microsoft.VisualBasic;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class PersonalCabinetUCViewModel : ViewModelBase
	{
        public PersonalCabinetUCViewModel()
        {
            CheckBoxCommand = ReactiveCommand.Create(() => { IsChecked = !IsChecked; });
        }

        public PersonalCabinetUCViewModel(int userID, IProjectDataSource source) 
        {
            CheckBoxCommand = ReactiveCommand.Create(() => { IsChecked = !IsChecked; });
            _source = source;
            _genderList = _source.GetAllGenders();
            SetUserDatum(userID);
            SetGender();
        }


        public UserDatum UserData
        {
            get => _userData;
            set => this.RaiseAndSetIfChanged(ref _userData, value);
        }

        public ICollection<UserGender> GenderList => _genderList;

        public string NewPassword
        {
            get => _newPassword;
            set => this.RaiseAndSetIfChanged(ref _newPassword, value);
        }
        public string NewPasswordConfirm
        {
            get => _newPasswordConfirm;
            set => this.RaiseAndSetIfChanged(ref _newPasswordConfirm, value);
        }

        public ReactiveCommand<Unit, Unit> CheckBoxCommand { get; }

        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }


        private UserDatum _userData;

        private IProjectDataSource _source;

        private ICollection<UserGender> _genderList;

        private string _newPassword;
        private string _newPasswordConfirm;

        private bool _isChecked = false;


        private void SetUserDatum(int userID) => UserData = _source.FindUserByIdSync(userID);

        private void SetGender()
        {
            foreach(UserGender gender in GenderList)
            {
                _userData.IdGenderNavigation = (gender.IdGender == _userData.IdGender)? gender : _userData.IdGenderNavigation;
            }
            this.RaisePropertyChanged(nameof(UserData));
        }

    }
}