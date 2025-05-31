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

        [ObservableProperty]
        private bool _isNewLeaseViewOpen = false;
        [ObservableProperty]
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
            var addLeaseViewModel = new NewLeaseViewModel(this, RealEstateAssetModel);
            addLeaseViewModel.CloseAction = () => IsNewLeaseViewOpen = false;
            NewLeaseView = new NewLeaseView()
            {
                DataContext = addLeaseViewModel
            };

            IsNewLeaseViewOpen = true;
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

        [RelayCommand]
        public void CloseNewLeaseWindow()
        {
            if (NewLeaseView != null && IsNewLeaseViewOpen)
            {
                NewLeaseView = null;
                IsNewLeaseViewOpen = false;
            }
        }
    }
}
