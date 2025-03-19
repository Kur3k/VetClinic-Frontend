using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Projekt.Model;
using System.Windows.Input;

namespace Projekt.ViewModel
{
    public class ChangeOwnerViewModel : ObservableObject
    {
        private OwnerManager _ownerManager = new();

        public string Country {  get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public int Id { get; set; }

        private List<string> _countries = new List<string>();
        public List<string> CountriesList
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        private string? _selectedCountry; 
        public string? SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }

        private Owner _owner = new Owner();
        public Owner Owner
        {
            get => _owner;
            set => SetProperty(ref _owner, value);
        }

        public ICommand AddOwnerCommand => new AsyncRelayCommand(async () =>
        {

            if (Name != null &&
                Surname != null &&
                PhoneNumber != null &&
                ValidateAddress())
            {
                var address = new Address()
                {
                    Street = Street,
                    City = City,
                    Country = Country,
                    HouseNumber = HouseNumber,
                };

                var owner = new Owner()
                {
                    Name = Name,
                    Surname = Surname,
                    PhoneNumber = PhoneNumber,
                    Address = address
                };

                await _ownerManager.AddOwner(owner);
            }
        });

        public ICommand UpdateOwnerCommand => new AsyncRelayCommand(async () => 
        {
            if (Owner.Id != 0 && 
                Name != null &&
                Surname != null &&
                PhoneNumber != null &&
                ValidateAddress())
            {
                var address = new Address(){
                    Street = Street,
                    City = City,
                    Country = Country,
                    HouseNumber = HouseNumber,
                };

                var updatedOwner = new Owner()
                {
                    Id = Owner.Id,
                    Name = Name,
                    Surname = Surname,
                    PhoneNumber = PhoneNumber,
                    Address = address
                };

                await _ownerManager.UpdateOwner(updatedOwner);
            }
        });

        private bool ValidateAddress()
        {
            if (string.IsNullOrWhiteSpace(Street) ||
                string.IsNullOrWhiteSpace(City)||
                string.IsNullOrWhiteSpace(Country)||
                string.IsNullOrWhiteSpace(HouseNumber))
            {
                return false;
            }

            return true;
        }

        public async Task GetCountries()
        {
            CountriesList = Countries.ListCountries;
        }

        public ChangeOwnerViewModel()//dodane
        {
            Task.Run(async () => await GetCountries()); 
        }
    }
}
