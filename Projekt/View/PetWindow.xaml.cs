using Projekt.Model;
using Projekt.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Projekt;

public partial class PetWindow : Window
{
    public PetWindow()
    {
       InitializeComponent();
       var viewModel = new PetViewModel();
       DataContext = viewModel;
       Loaded += async (s, e) =>
       {
          await viewModel.LoadPets();
       };
    }
}