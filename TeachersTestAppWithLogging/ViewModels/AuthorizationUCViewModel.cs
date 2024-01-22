using System;
using System.Collections.Generic;
using System.Reactive;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class AuthorizationUCViewModel : ReactiveObject
    {
		public AuthorizationUCViewModel() 
		{
			AuthorizeCommand = ReactiveCommand.Create<string>(AuthorizeInSystemCommandMethod);
        }


		public event Action<int> NotifyUserWasSuccessfulAuthorize;


		public string Login
		{
			get => _login;
			set => this.RaiseAndSetIfChanged(ref _login, value);
		}
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

		public ReactiveCommand<string, Unit> AuthorizeCommand { get; }


        private string _login;
		private string _password;


		private async void AuthorizeInSystemCommandMethod(string param)
		{
			Vorobiew2ContextAdapter vorobiew2ContextAdapter = new Vorobiew2ContextAdapter();
			int userID = await vorobiew2ContextAdapter.AuthorizeUserInSystemAsync(Login, Password);
            if (userID == -1)
			{
                _ = MessageBoxManager.GetMessageBoxStandard("ѕользователь не найден!"
                                                     , "ѕользователь с таким логином и паролем не найден в системе"
                                                     , ButtonEnum.Ok)
                                 .ShowAsync();
				return;
            }
			NotifyUserWasSuccessfulAuthorize(userID);
		}
	}
}