using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Model;
using Projekt.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModel;

class OwnerViewModel : ObservableObject
{
    private ObservableCollection<Owner> _owners = new();

    public ObservableCollection<Owner> Owners
    {
        get => _owners;
        set => SetProperty(ref _owners, value);
    }

    public Owner? SelectedOwner { get; set; }

    public async Task LoadOwner()
    {
        var ownerMenagrer = new OwnerManager();
        var list = await ownerMenagrer.GetOwners();
        if (list is not null)
        {
            Owners = new ObservableCollection<Owner>(list);
        }
    }

    public ICommand ChangeOwnerWindowCommand => new RelayCommand(() =>
    {
        var changeWindow = new ChangeOwnerWindow();
        changeWindow.ShowDialog();
        LoadOwner();
    });

    public ICommand RemoveOwnerCommand => new RelayCommand(() =>
    {
        if (SelectedOwner is null) return;

        var response = MessageBox.Show("Do you want to remove selected owner?",
                        "Attention",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information);

        if (response == MessageBoxResult.Yes)
        {
            //remove owner
        }
    });
}
