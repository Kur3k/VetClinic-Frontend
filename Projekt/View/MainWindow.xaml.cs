using Projekt.ViewModel;
using System.Windows;

namespace Projekt.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
