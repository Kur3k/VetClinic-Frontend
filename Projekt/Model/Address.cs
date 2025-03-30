using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace Projekt.Model;

public class Address : ObservableObject
{
    public string Street
    {
        get => _street;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Street cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!value.StartsWith("ul.", StringComparison.OrdinalIgnoreCase))
            {
                value = "ul." + value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower(); 
            }

            SetProperty(ref _street, value);
        }
    }

    public string HouseNumber
    {
        get => _houseNumber;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("House number cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetProperty(ref _houseNumber, value);
        }
    }

    public string City
    {
        get => _city;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("City cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetProperty(ref _city, value);
        }
    }

    public string Country
    {
        get => _country;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Country cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetProperty(ref _country, value);
        }
    }

    public Address()
    {
        _street = string.Empty;
        _houseNumber = string.Empty;
        _city = string.Empty;
        _country = string.Empty;
    }

    public Address(string street, string houseNumber, string city, string country)
    {
        Street = street;
        _houseNumber = houseNumber;
        _city = city;
        _country = country;
    }

    public override string ToString()
    {
        return $"{_street} {_houseNumber}, {_city}, {_country}";
    }

    private string _street;
    private string _houseNumber;
    private string _city;
    private string _country;
}
