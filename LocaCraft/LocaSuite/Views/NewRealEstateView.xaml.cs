using LocaCraft.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace LocaCraft.Views
{
    /// <summary>
    /// Logique d'interaction pour NewRealEstateView.xaml
    /// </summary>
    public partial class NewRealEstateView : Window
    {
        public NewRealEstateView()
        {
            InitializeComponent();
            NewRealEstateViewModel viewModel = new NewRealEstateViewModel()
            {
                CloseAction = Close
            };
            DataContext = viewModel;
        }        
    }


}
