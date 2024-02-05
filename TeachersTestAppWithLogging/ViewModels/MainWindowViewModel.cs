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

        public bool IsCourcesPage
        {
            get => _isCourcesPage;
            set => this.RaiseAndSetIfChanged(ref _isCourcesPage, value);
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

        public CourcesListUCViewModel CourcesListUCViewModel
        {
            get => _courcesListUCViewModel;
            set => this.RaiseAndSetIfChanged(ref _courcesListUCViewModel, value);
        }


        private int _userID = -1;

        private bool _isAuthPageOpen = true;

        private bool _isPersonalPage = false;

        private bool _isCourcesPage = false;

        private IProjectDataSource _projectDataSource;

        private AuthorizationUCViewModel _authorizationUCViewModel;

        private PersonalCabinetUCViewModel _personalCabinetUCViewModel;

        private CourcesListUCViewModel _courcesListUCViewModel;


        private void UserWasAuthorize(int userID)
        {
            _userID = userID;

            switch (_projectDataSource.GetUserRole(userID).IdRole)
            {
                case 1:
                    {
                        this.CourcesListUCViewModel = new CourcesListUCViewModel(_projectDataSource);
                        IsCourcesPage = true;
                        break;
                    }
                case 2: 
                    {
                        this.PersonalCabinetUCViewModel = new PersonalCabinetUCViewModel(userID, _projectDataSource);
                        IsPersonalPage = true;
                        break;
                    }
                default: 
                    {
                        return;
                    }
            }
            IsAuthPageOpen = false;
        }
    }
}