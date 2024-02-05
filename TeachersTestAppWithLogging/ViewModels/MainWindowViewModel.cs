using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() 
        {
            _projectDataSource = new Vorobiew2ContextAdapter();
            AuthorizationUCViewModel = new AuthorizationUCViewModel(_projectDataSource);
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

        private IProjectDataSource _projectDataSource;

        private AuthorizationUCViewModel _authorizationUCViewModel;

        private PersonalCabinetUCViewModel _personalCabinetUCViewModel;


        private void UserWasAuthorize(int userID)
        {
            _userID = userID;

            this.PersonalCabinetUCViewModel = new PersonalCabinetUCViewModel(userID, _projectDataSource);

            IsPersonalPage = true;
            IsAuthPageOpen = false;
        }
    }
}