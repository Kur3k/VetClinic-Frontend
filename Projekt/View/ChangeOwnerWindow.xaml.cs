using Projekt.ViewModel;
using System.Windows;

namespace Projekt.View;


public partial class ChangeOwnerWindow : Window
{
    public ChangeOwnerWindow()
    {
        InitializeComponent();
        var changeOwnerViewModel = new ChangeOwnerViewModel();
        DataContext = changeOwnerViewModel;
        Loaded += async (s, e) =>
        {
            await changeOwnerViewModel.GetCountries();
        };
    }
}
