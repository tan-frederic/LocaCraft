using LocaCraft.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LocaCraft.Views
{
    /// <summary>
    /// Logique d'interaction pour RealEstateDetailsView.xaml
    /// </summary>
    public partial class RealEstateDetailsView : UserControl
    {
        public RealEstateDetailsView()
        {
            InitializeComponent();
        }

        private void Border_DragOver(object sender, DragEventArgs e)
        {
            if (DataContext is RealEstateDetailsViewModel vm)
            {
                vm.Border_DragOver(sender, e);
            }
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            if (DataContext is RealEstateDetailsViewModel vm)
            {
                vm.Border_Drop(sender, e);
            }
        }
    }
}
