using CommunityToolkit.Mvvm.ComponentModel;
using LocaCraft.Commands;
using LocaCraft.DataServices;
using LocaCraft.Models;
using LocaCraft.Services;
using System.Windows;
using System.Windows.Input;

namespace LocaCraft.ViewModels
{
    public partial class RealEstateDetailsViewModel : BaseViewModel
    {
        #region ATTRIBUTES

        public RealEstateAssetModel RealEstateAssetModel { get; private set; }

        private RealEstateDataService _service = new RealEstateDataService();

        [ObservableProperty]
        private string _dropText;

        public ICommand NavigateToHome { get; }
        #endregion

        #region CONSTRUCTOR
        public RealEstateDetailsViewModel(RealEstateAssetModel realEstateAssetModel, NavigationStore navigationStore)
        {
            RealEstateAssetModel = realEstateAssetModel;
            NavigateToHome = new CommandNavigation<RealEstateListViewModel>(new NavigationService<RealEstateListViewModel>(navigationStore, () => new RealEstateListViewModel(navigationStore)));
        }
        #endregion

        public void Border_DragOver(object sender, DragEventArgs e)
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

        public void Border_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
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
}
