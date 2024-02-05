using System;
using System.Collections.Generic;
using DynamicData.Binding;
using Microsoft.VisualBasic;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class PersonalCabinetUCViewModel : ViewModelBase
	{
        public PersonalCabinetUCViewModel(int userID, IProjectDataSource source) 
        {
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


        private UserDatum _userData;

        private IProjectDataSource _source;

        private ICollection<UserGender> _genderList;


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