using Avalonia.Controls;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;
using TeachersTestAppWithLogging.Views;

namespace TeachersTestAppWithLogging.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() 
        {
            _projectDataSource = new Vorobiew2ContextAdapter();
            AuthorizationUCViewModel = new AuthorizationUCViewModel(_projectDataSource);
            AuthorizationUCViewModel.NotifyUserWasSuccessfulAuthorize += UserWasAuthorize;
            ActiveUC = new AuthorizationUC() { DataContext = AuthorizationUCViewModel };
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

        public UserControl ActiveUC
        {
            get => _activeUC;
            set => this.RaiseAndSetIfChanged(ref _activeUC, value);
        }


        private int _userID = -1;

        private IProjectDataSource _projectDataSource;

        private AuthorizationUCViewModel _authorizationUCViewModel;

        private PersonalCabinetUCViewModel _personalCabinetUCViewModel;

        private CourcesListUCViewModel _courcesListUCViewModel;

        private UserControl _activeUC;


        private void UserWasAuthorize(int userID)
        {
            _userID = userID;

            switch (_projectDataSource.GetUserRole(userID).IdRole)
            {
                case 1:
                    {
                        this.CourcesListUCViewModel = new CourcesListUCViewModel(_projectDataSource);
                        ActiveUC = new CourcesListUC() { DataContext = CourcesListUCViewModel };
                        break;
                    }
                case 2: 
                    {
                        this.PersonalCabinetUCViewModel = new PersonalCabinetUCViewModel(userID, _projectDataSource);
                        ActiveUC = new PersonalCabinetUC() { DataContext = PersonalCabinetUCViewModel };
                        break;
                    }
                default: 
                    {
                        return;
                    }
            }
        }
    }
}