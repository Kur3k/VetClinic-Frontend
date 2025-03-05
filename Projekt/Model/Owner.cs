using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace Projekt.Model;

public class Owner : ObservableObject
{
    private string _name;
    private string _surname;
    private string _phoneNumber;
    private Address _address; 

    private int _id;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrEmpty(value) || !value.All((s) => char.IsLetter(s) || s == ' ' || s == '-'))
            {
                MessageBox.Show($"Wrong Name: {value}!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetProperty(ref _name, value);
        }
    }

    public string Surname
    {
        get => _surname;
        set
        {
            if (string.IsNullOrEmpty(value) || !value.All((s) => char.IsLetter(s) || s == ' ' || s == '-'))
            {
                MessageBox.Show($"Wrong Surname: {value}!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetProperty(ref _surname, value);
        }
    }

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            if (string.IsNullOrEmpty(value) || !value.All(char.IsDigit))
            {
                MessageBox.Show($"Wrong Phone Number: {value}!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetProperty(ref _phoneNumber, value);
        }
    }

    public Address Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }

    public Owner()
    {
        _name = string.Empty;
        _surname = string.Empty;
        _phoneNumber = string.Empty;
        _address = new Address(); 
    }

    public Owner(string name, string surname, string phoneNumber, Address address)
    {
        _name = name;
        _surname = surname;
        _phoneNumber = phoneNumber;
        _address = address; 
    }

    public override string ToString()
    {
       return $"{_name} {_surname}, Phone: {_phoneNumber}, Address: {_address}";
    }
}
