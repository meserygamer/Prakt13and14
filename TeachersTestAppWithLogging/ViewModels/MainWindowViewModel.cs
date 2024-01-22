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

        public AuthorizationUCViewModel AuthorizationUCViewModel 
        {
            get => _authorizationUCViewModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _authorizationUCViewModel, value);
            }
        }


        private int _userID = -1;

        private bool _isAuthPageOpen = true;

        private AuthorizationUCViewModel _authorizationUCViewModel;


        private void UserWasAuthorize(int userID)
        {
            _userID = userID;

            IsAuthPageOpen = false;
        }
    }
}