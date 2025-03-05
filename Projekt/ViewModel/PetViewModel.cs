using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Model;
using Projekt.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModel;

public class PetViewModel : ObservableObject
{
    private ObservableCollection<Pet> _pets;

    public ObservableCollection<Pet> Pets
    { 
        get => _pets; 
        set => SetProperty(ref _pets, value); 
    }

    public Pet? SelectedPet { get; set; }

    public async Task LoadPets()
    {
        var petMenager = new PetMenager();
        var list = await petMenager.GetPet();
        if (list is not null)
        {
            Pets = new ObservableCollection<Pet>(list);
        }
    }

    public ICommand ChangePetWindowCommand => new RelayCommand(() =>
    {
        var changeWindow = new ChangePetWindow();

        var viewModel = new ChangePetViewModel(changeWindow);
        changeWindow.DataContext = viewModel; 

        changeWindow.ShowDialog();

        LoadPets();
    });

    public ICommand RemovePetCommand => new RelayCommand(() =>
    {
        if (SelectedPet is null)
        {
            return;
        }

        var response = MessageBox.Show("Do you want to remove selected pet?",
                        "Attention",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Information);

        if (response == MessageBoxResult.Yes)
        {
            //remove pet
        }

    });
}
