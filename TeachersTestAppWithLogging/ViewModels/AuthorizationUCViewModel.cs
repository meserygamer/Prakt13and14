using System;
using System.Collections.Generic;
using ReactiveUI;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class AuthorizationUCViewModel : ReactiveObject
	{
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


        private string _login = "hello";
		private string _password = "world!";
	}
}