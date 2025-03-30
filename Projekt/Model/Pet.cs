using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace Projekt.Model;

public class Pet : ObservableObject
{    
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
                MessageBox.Show("Wrong Name!", "error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SetProperty(ref _name, value);

        }
    }
    public string Species
    {
        get => _species;
        set
        {
            if (string.IsNullOrEmpty(value) || !value.All((s) => char.IsLetter(s) || s == ' ' || s == '-'))
            {
                MessageBox.Show("Wrong Species!", "error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                SetProperty(ref _species, value);
            }
        }
    }
    public string Gender
    {
        get => _gender;
        set
        {
            if (string.IsNullOrEmpty(value) || !value.All((s) => char.IsLetter(s)))
            {
                MessageBox.Show("Wrong Gender!", "error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                SetProperty(ref _gender, value);
            }
        }
    }

    public DateTime YearOfBirth
    {
        get => _yearOfBirth;
        set => SetProperty(ref _yearOfBirth, value);
    }

    public Owner Owner
    {
        get => _owner;
        set => SetProperty(ref _owner, value);
    }

    private string _name = string.Empty;
    private string _species = string.Empty;
    private string _gender = string.Empty;
    private DateTime _yearOfBirth;
    private int _id;
    private Owner _owner = new();
}
