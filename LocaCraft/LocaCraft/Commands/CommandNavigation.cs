using LocaCraft.Models;
using LocaCraft.Services;
using LocaCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaCraft.Commands
{
    internal class CommandNavigation<TViewModel> : BaseCommand where TViewModel : BaseViewModel
    {
        NavigationService<TViewModel> _navigationServices;

        #region CONSTRUCTOR
        public CommandNavigation(NavigationService<TViewModel> navigationServices)
        {
            _navigationServices = navigationServices;
        }
        #endregion

        public override void Execute(object? parameter)
        {
            _navigationServices.Navigate();
        }
    }
}
