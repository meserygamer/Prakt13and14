using System;
using System.Collections.Generic;
using ReactiveUI;
using TeachersTestAppWithLogging.DBModels;

namespace TeachersTestAppWithLogging.ViewModels
{
	public class CourcesListUCViewModel : ReactiveObject
	{
		public CourcesListUCViewModel(IProjectDataSource source) 
		{
			_source = source;
			Cources = _source.GetAllCources();
		}


		public ICollection<Cource> Cources
		{
			get => _cources;
			set => this.RaiseAndSetIfChanged(ref _cources, value);
		}


		private ICollection<Cource> _cources;

		private IProjectDataSource _source;
	}
}