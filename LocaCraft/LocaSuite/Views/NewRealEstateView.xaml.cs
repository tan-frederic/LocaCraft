using LocaSuite.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace LocaSuite.Views
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
