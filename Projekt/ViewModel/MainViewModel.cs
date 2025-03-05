using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.View;
using System.Windows.Input;
namespace Projekt.ViewModel;

public class MainViewModel : ObservableObject
{

    public ICommand GoToPetsCommand => new RelayCommand(() =>
    {
        new PetWindow().ShowDialog();
    });

    public ICommand GoToOwnersCommand => new RelayCommand(() =>
    {
        new OwnerWindow().ShowDialog();
    });

}
