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


        public UserDatum UserData
        {
            get => _userData;
            set => this.RaiseAndSetIfChanged(ref _userData, value);
        }


        private UserDatum _userData;

        private IProjectDataSource _source;


        private void SetUserDatum(int userID) => UserData = _source.FindUserByIdSync(userID);

    }
}