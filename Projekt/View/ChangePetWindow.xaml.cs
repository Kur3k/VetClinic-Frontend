using Projekt.ViewModel;
using System.Windows;

namespace Projekt.View;

public partial class ChangePetWindow : Window
{
    public ChangePetWindow()
    {
        InitializeComponent();
        var changePetViewModel = new ChangePetViewModel(this); 
        DataContext = changePetViewModel;

        Loaded += async (s, e) =>
        {
            await changePetViewModel.GetOwners();
            cbxOwners.ItemsSource = changePetViewModel.OwnerList;
        };
    }
}
