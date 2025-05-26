using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LocaCraft.Commands;
using LocaCraft.DataServices;
using LocaCraft.Models;
using LocaCraft.Services;
using LocaCraft.Views;
using System.Windows;
using System.Windows.Input;

namespace LocaCraft.ViewModels
{
    public partial class RealEstateDetailsViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        public IRelayCommand<IDataObject> DropFilesCommand { get; }
        public IRelayCommand<DragEventArgs> DragOverCommand { get; }

        public RealEstateAssetModel RealEstateAssetModel { get; private set; }

        private RealEstateDataService _service = new RealEstateDataService();

        private NewLeaseView? _newLeaseView;

        [ObservableProperty]
        private string _dropText;

        public ICommand NavigateToHome { get; }
        #endregion

        #region CONSTRUCTOR
        public RealEstateDetailsViewModel()
        {
            _dropText = "Drop your file here";
            RealEstateAssetModel = new RealEstateAssetModel();
            NavigateToHome = new CommandNavigation<RealEstateListViewModel>(new NavigationService<RealEstateListViewModel>(new NavigationStore(), () => new RealEstateListViewModel(new NavigationStore())));
            DropFilesCommand = new RelayCommand<IDataObject>(HandleDropCommand);
            DragOverCommand = new RelayCommand<DragEventArgs>(HandleDragOverCommand);
        }

        public RealEstateDetailsViewModel(RealEstateAssetModel realEstateAssetModel, NavigationStore navigationStore)
        {
            _dropText = "Drop your file here";
            RealEstateAssetModel = realEstateAssetModel;
            NavigateToHome = new CommandNavigation<RealEstateListViewModel>(new NavigationService<RealEstateListViewModel>(navigationStore, () => new RealEstateListViewModel(navigationStore)));
            DropFilesCommand = new RelayCommand<IDataObject>(HandleDropCommand);
            DragOverCommand = new RelayCommand<DragEventArgs>(HandleDragOverCommand);
        }
        #endregion

        [RelayCommand]
        public void OpentNewLeaseWindow()
        {
            // Logic to open a new lease window can be added here
            // For example, you could navigate to a new view model or open a new window
            if (_newLeaseView == null || !_newLeaseView.IsLoaded )
            {
                _newLeaseView = new NewLeaseView();
                _newLeaseView.Closed += (s, e) => _newLeaseView = null; // Add event handler to set _newLeaseView to null when closed
                _newLeaseView.Show(); // Show the new window
                _newLeaseView.Focus(); // Set focus to the new window
            }
            else
            {
                _newLeaseView.Focus(); // Set focus to the existing window if it is already open
            }
        }

        public void HandleDragOverCommand(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                DropText = "File on dropping";
            }
            else
            {
                e.Effects = DragDropEffects.None;
                DropText = "Drop your file here";
            }
        }

        public void HandleDropCommand(IDataObject dataObject)
        {
            if (!dataObject.GetDataPresent(DataFormats.FileDrop))
                return;

            string[] files = (string[])dataObject.GetData(DataFormats.FileDrop)!;
            if (files.Length > 0)
            {
                // Handle the dropped file(s) here
                string filePath = files[0];
                DropText = $"File dropped: {filePath}";
                // Do something with the file path, e.g., display it or process it
            }
        }
    }
}
