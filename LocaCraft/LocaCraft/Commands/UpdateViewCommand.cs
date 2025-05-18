using LocaCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LocaCraft.Commands
{
    public class UpdateViewCommand : ICommand
    {
        #region VARIABLES
        private MainWindowViewModel _mainWindowViewModel;
        #endregion

        #region CONSTRUCTOR
        public UpdateViewCommand(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }
        #endregion

        #region COMMANDS
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            if (parameter.ToString() == "RealEstateList")
            {
                _mainWindowViewModel.CurrentView = new RealEstateListViewModel();
            }
            else if (parameter.ToString() == "RealEstateDetail")
            {
                //_mainWindowViewModel.CurrentView = new RealEstateDetailsViewModel();
            }
        }
        #endregion
    }
}
