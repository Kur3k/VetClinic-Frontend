using Projekt.ViewModel;
using System.Windows;

namespace Projekt;

public partial class PetWindow : Window
{
    public PetWindow()
    {
       InitializeComponent();
       DataContext = new PetViewModel();
    }
}