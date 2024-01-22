using ReactiveUI;

namespace TeachersTestAppWithLogging.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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


        private bool _isAuthPageOpen = true;

        private AuthorizationUCViewModel _authorizationUCViewModel = new AuthorizationUCViewModel();
    }
}