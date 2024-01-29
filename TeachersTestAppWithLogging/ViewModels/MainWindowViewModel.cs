using ReactiveUI;

namespace TeachersTestAppWithLogging.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() 
        {
            AuthorizationUCViewModel = new AuthorizationUCViewModel();
            AuthorizationUCViewModel.NotifyUserWasSuccessfulAuthorize += UserWasAuthorize;
        }


        public bool IsAuthPageOpen
        {
            get => _isAuthPageOpen;
            set
            {
                this.RaiseAndSetIfChanged(ref _isAuthPageOpen, value);
            }
        }

        public bool IsPersonalPage
        {
            get => _isPersonalPage;
            set => this.RaiseAndSetIfChanged(ref _isPersonalPage, value);
        }

        public AuthorizationUCViewModel AuthorizationUCViewModel 
        {
            get => _authorizationUCViewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _authorizationUCViewModel, value);
            }
        }

        public PersonalCabinetUCViewModel PersonalCabinetUCViewModel
        {
            get => _personalCabinetUCViewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _personalCabinetUCViewModel, value);
            }
        }


        private int _userID = -1;

        private bool _isAuthPageOpen = true;

        private bool _isPersonalPage = false;

        private AuthorizationUCViewModel _authorizationUCViewModel;

        private PersonalCabinetUCViewModel _personalCabinetUCViewModel;


        private void UserWasAuthorize(int userID)
        {
            _userID = userID;

            this.PersonalCabinetUCViewModel = new PersonalCabinetUCViewModel();

            IsPersonalPage = true;
            IsAuthPageOpen = false;
        }
    }
}