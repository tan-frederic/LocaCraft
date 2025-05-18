using CommunityToolkit.Mvvm.ComponentModel;
using LocaCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocaCraft.Services
{
    public partial class NavigationStore : INotifyPropertyChanged
    {
        public event Action CurrentViewModelChanged;

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel 
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            } 
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
