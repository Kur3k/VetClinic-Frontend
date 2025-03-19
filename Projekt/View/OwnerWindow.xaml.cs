using Projekt.ViewModel;
using System.Windows;

namespace Projekt.View
{
    public partial class OwnerWindow : Window
    {
        public OwnerWindow()
        {
            InitializeComponent();
            var viewModel = new OwnerViewModel();
            DataContext = viewModel;
            Loaded += async (s, e) =>
            {
                await viewModel.LoadOwner();
            };
        }


    }
}
