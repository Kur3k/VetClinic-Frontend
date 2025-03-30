using Projekt.ViewModel;
using System.Windows;

namespace Projekt.View;

public partial class ChangePetWindow : Window
{
    public ChangePetWindow()
    {
        InitializeComponent();
        DataContext = new ChangePetViewModel(this);
    }
}
