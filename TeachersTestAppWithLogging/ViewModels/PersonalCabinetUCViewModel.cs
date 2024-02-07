using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text.RegularExpressions;
using DynamicData.Binding;
using Microsoft.VisualBasic;
using MsBox.Avalonia;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;
using TeachersTestAppWithLogging.Models;
using Tmds.DBus.Protocol;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class PersonalCabinetUCViewModel : ViewModelBase
	{
        public PersonalCabinetUCViewModel()
        {
            CheckBoxCommand = ReactiveCommand.Create(() => { IsChecked = !IsChecked; });
        }

        public PersonalCabinetUCViewModel(int userID, IProjectDataSource source) 
        {
            _model = new PersonalCabinetUCModel();
            CheckBoxCommand = ReactiveCommand.Create(() => { IsChecked = !IsChecked; });
            ConfirmChanges = ReactiveCommand.Create(ConfirmChangesCommand);
            _source = source;
            _genderList = _source.GetAllGenders();
            SetUserDatum(userID);
            SetWorkTime();
            SetGender();
        }


        public UserDatum UserData
        {
            get => _userData;
            set => this.RaiseAndSetIfChanged(ref _userData, value);
        }

        public ICollection<UserGender> GenderList => _genderList;

        public string NewPassword
        {
            get => _newPassword;
            set => this.RaiseAndSetIfChanged(ref _newPassword, value);
        }
        public string NewPasswordConfirm
        {
            get => _newPasswordConfirm;
            set => this.RaiseAndSetIfChanged(ref _newPasswordConfirm, value);
        }

        public ReactiveCommand<Unit, Unit> CheckBoxCommand { get; }
        public ReactiveCommand<Unit, Unit> ConfirmChanges { get; }

        public bool IsChecked
        {
            get => _isChecked;
            set => this.RaiseAndSetIfChanged(ref _isChecked, value);
        }
        public bool WorkTimeIsFocused
        {
            set
            {
                if (value is false && _workTimeIsFocused == true)
                {
                    try
                    {
                        double year = Convert.ToDouble(WorkTimeInYear);
                        SetWorkTime((int)(year * 12));
                    }
                    catch
                    {
                        SetWorkTime(0);
                    }
                }
                _workTimeIsFocused = value;
            }
        }

        public string? WorkTimeInYear
        {
            get => _workTimeInYear;
            set => this.RaiseAndSetIfChanged(ref _workTimeInYear, value);
        }


        public void ConfirmChangesCommand()
        {
            if(UserData.Surname.Length == 0 || UserData.Surname is null)
            {
                MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Поле фамилии пустое!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                return;
            }
            if (UserData.Name.Length == 0 || UserData.Name is null)
            {
                MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Поле имени пустое!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                return;
            }
            if (UserData.Patronymic is null || UserData.Patronymic.Length == 0)
            {
                MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Поле отчества пустое!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                return;
            }
            if (UserData.IdUserNavigation.Login.Length < 3 || UserData.IdUserNavigation.Login is null)
            {
                MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Логин должен содежать не менее 3 символов!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                return;
            }
            if (UserData.Birthdate >= DateTime.Now)
            {
                MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Вы ещё не родились!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                return;
            }
            if (UserData.Email.Length > 0 && !Regex.IsMatch(UserData.Email,
                "(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])"))
            {
                MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Некорректный Email!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                return;
            }

            if (UserData.PhoneNumber is null || UserData.PhoneNumber.Length == 0)
            {
                MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Поле телефона не заполнено!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                return;
            }

            if (!(UserData.PhoneNumber is null || UserData.PhoneNumber.Length == 0))
            {
                if (!Regex.IsMatch(UserData.PhoneNumber, "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$"))
                {
                    MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Поле телефона заполнено, но введенный номер некорректен!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                    return;
                }

                UserData.PhoneNumber = _model.ConvertPhoneNumberTo89Format(UserData.PhoneNumber);
            }

            if (IsChecked)
            {
                if(NewPassword is null || NewPassword.Length == 0)
                {
                    MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Вы выбрали изменение пароля, но не заполнили поле нового пароля!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                    return;
                }
                if(NewPassword != NewPasswordConfirm)
                {
                    MessageBoxManager.GetMessageBoxStandard("Не удалось сохранить изменения!"
                    , "Новый пароль не совпадает с полем подтверждения!"
                    , MsBox.Avalonia.Enums.ButtonEnum.Ok)
                    .ShowAsync();
                    return;
                }
                UserData.IdUserNavigation.Password = NewPassword;
            }

            _source.UpdateUser(UserData);
        }


        private UserDatum _userData;

        private IProjectDataSource _source;

        private ICollection<UserGender> _genderList;

        private string _newPassword;
        private string _newPasswordConfirm;

        private bool _isChecked = false;
        private bool _workTimeIsFocused = false;

        private string? _workTimeInYear;

        private PersonalCabinetUCModel _model;


        private void SetUserDatum(int userID) => UserData = _source.FindUserByIdSync(userID);

        private void SetGender()
        {
            foreach(UserGender gender in GenderList)
            {
                _userData.IdGenderNavigation = (gender.IdGender == _userData.IdGender)? gender : _userData.IdGenderNavigation;
            }
            this.RaisePropertyChanged(nameof(UserData));
        }

        private void SetWorkTime(int? Months = null)
        {
            if(Months is not null)
            {
                UserData.Worktime = (int)Months;
            }
            WorkTimeInYear = ((double)UserData.Worktime / 12).ToString();
        }

    }
}