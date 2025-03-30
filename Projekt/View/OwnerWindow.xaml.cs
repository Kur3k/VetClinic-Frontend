using Projekt.ViewModel;
using System.Windows;

namespace Projekt.View
{
    public partial class OwnerWindow : Window
    {
        public OwnerWindow()
        {
            InitializeComponent();
            DataContext = new OwnerViewModel();
        }
    }
}
