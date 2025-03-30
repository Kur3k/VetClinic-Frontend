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
    public ObservableCollection<Owner> Owners
    {
        get => _owners;
        set => SetProperty(ref _owners, value);
    }

    public Owner? SelectedOwner { get; set; }

    public async Task LoadOwner()
    {
        var ownerMenagrer = new OwnerManager();
        List<Owner>? list = await ownerMenagrer.GetOwners();
        if (list is not null)
        {
            Owners = new ObservableCollection<Owner>(list);
        }
    }

    public ICommand ChangeOwnerWindowCommand => new RelayCommand(async () =>
    {
        var changeWindow = new ChangeOwnerWindow();
        changeWindow.ShowDialog();
        await LoadOwner();
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

    public ICommand ViewLoadedCommand => new RelayCommand(async () =>
    {
        await LoadOwner();
    });

    private ObservableCollection<Owner> _owners = new();

}
