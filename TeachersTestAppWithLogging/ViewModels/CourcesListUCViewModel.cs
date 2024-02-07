using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class CourcesListUCViewModel : ReactiveObject
	{
		public CourcesListUCViewModel(IProjectDataSource source) 
		{
			_source = source;
			_courcesBeforeMagic = _source.GetAllCources();
			GetMagic();
		}


		public ObservableCollection<Cource> Cources
		{
			get => _courcesAfterMagic;
			set => this.RaiseAndSetIfChanged(ref _courcesAfterMagic, value);
		}

		public int SelectedFilterIndex
		{
			get => _selectedFilterIndex;
			set
			{
				this.RaiseAndSetIfChanged(ref _selectedFilterIndex, value);
				GetMagic();
			}
		}
        public int SelectedSortIndex
        {
            get => _selectedSortIndex;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedSortIndex, value);
                GetMagic();
            }
        }

        public string SearchQuarry
        {
            get => _searchQuarry;
            set
            {
                this.RaiseAndSetIfChanged(ref _searchQuarry, value);
                GetMagic();
            }
        }

        public bool IsSearchByName
        {
            get => _isSearchByName;
            set
            {
                this.RaiseAndSetIfChanged(ref _isSearchByName, value);
                GetMagic();
            }
        }
        public bool IsSortingABS
        {
            get => _isSortingABS;
            set
            {
                this.RaiseAndSetIfChanged(ref _isSortingABS, value);
                GetMagic();
            }
        }



        private ObservableCollection<Cource> _courcesAfterMagic = new ObservableCollection<Cource>();

		private List<Cource> _courcesBeforeMagic = new List<Cource>();

		private int _selectedFilterIndex = 0;
        private int _selectedSortIndex = 0;

        private string _searchQuarry = "";

		private bool _isSearchByName = true;
        private bool _isSortingABS = true;

		private IProjectDataSource _source;


		/// <summary>
		/// Поиск, сортировка и фильтрация
		/// </summary>
		private void GetMagic()
		{
			Cources.Clear();
			Sorting(Finding(Filtring()));
		}

		private List<Cource> Filtring()
		{
			List<Cource> FilteredCource = new List<Cource>();
			switch(SelectedFilterIndex)
			{
				case 0:
					{
                        foreach (Cource c in _courcesBeforeMagic)
                        {
                            FilteredCource.Add(c);
                        }
                        break;
					}
				case 1:
					{
						foreach(Cource c in _courcesBeforeMagic)
						{
							if(c.HoursNumber >= 0 && c.HoursNumber < 30)
							{
                                FilteredCource.Add(c);
							}
						}
                        break;
					}
                case 2:
                    {
                        foreach (Cource c in _courcesBeforeMagic)
                        {
                            if (c.HoursNumber >= 30 && c.HoursNumber < 60)
                            {
                                FilteredCource.Add(c);
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        foreach (Cource c in _courcesBeforeMagic)
                        {
                            if (c.HoursNumber >= 60 && c.HoursNumber < 120)
                            {
                                FilteredCource.Add(c);
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        foreach (Cource c in _courcesBeforeMagic)
                        {
                            if (c.HoursNumber >= 120)
                            {
                                FilteredCource.Add(c);
                            }
                        }
                        break;
                    }
            }
            return FilteredCource;

        }

		private List<Cource> Finding(List<Cource> NotSearchedCources)
		{
			if (SearchQuarry is null || SearchQuarry.Length == 0) return NotSearchedCources;
            List<Cource> SearchedCources = new List<Cource>();
			switch (_isSearchByName) 
			{
				case true: 
					{
						foreach(Cource c in NotSearchedCources)
						{
							if (Regex.IsMatch(c.Cource1, SearchQuarry)) SearchedCources.Add(c);
						}
						break;
					}
                case false:
                    {
                        foreach (Cource c in NotSearchedCources)
                        {
                            if (Regex.IsMatch(c.HoursNumber.ToString(), SearchQuarry)) SearchedCources.Add(c);
                        }
                        break;
                    }
            }
            return SearchedCources;

        }

        private void Sorting(List<Cource> NotSortedCources)
        {
            switch(IsSortingABS)
            {
                case true:
                    switch(SelectedSortIndex)
                    {
                        case 0:
                            Cources = new ObservableCollection<Cource>(NotSortedCources.OrderBy((a) => a.Cource1));
                            return;
                        case 1:
                            Cources = new ObservableCollection<Cource>(NotSortedCources.OrderBy((a) => a.HoursNumber));
                            return;
                    }
                    return;
                case false:
                    switch (SelectedSortIndex)
                    {
                        case 0:
                            Cources = new ObservableCollection<Cource>(NotSortedCources.OrderByDescending((a) => a.Cource1));
                            return;
                        case 1:
                            Cources = new ObservableCollection<Cource>(NotSortedCources.OrderByDescending((a) => a.HoursNumber));
                            return;
                    }
                    return;
            }
        }
	}
}