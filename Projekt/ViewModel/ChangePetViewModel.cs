using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Model;
using Projekt.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Projekt.ViewModel;

public class ChangePetViewModel(Window changePetWindow) : ObservableObject
{
    private PetMenager _petMenager = new();
    private OwnerManager _ownerManager = new();

    public string? Name { get; set; }
    public string? Species { get; set; }
    public string? Gender { get; set; }
    public DateTime YearOfBirth { get; set; } = DateTime.Now;
    public string? Index { get; set; }

    private ObservableCollection<Owner> _ownerList = [];
    public ObservableCollection<Owner> OwnerList
    {
        get => _ownerList;
        set => SetProperty(ref _ownerList, value);
    }

    private Owner _owner = new();
    public Owner Owner
    {
        get => _owner;
        set => SetProperty(ref _owner, value);
    }

    public ICommand AddPetCommand => new AsyncRelayCommand(async () =>
    {
        if (Name != null &&
            Species != null &&
            Gender != null)
        {
            var pet = new Pet()
            {
                Name = Name,
                Species = Species,
                Gender = Gender,
                YearOfBirth = YearOfBirth,
                Owner = Owner
            };

            await _petMenager.AddPet(pet);
        }
        changePetWindow.Close();

    });

    public async Task GetOwners()
    {
        var list = await _ownerManager.GetOwners();
        if (list is not null)
        {
            list.ForEach(o => OwnerList.Add(o));
            //OwnerList = list;
        }
    }
}
