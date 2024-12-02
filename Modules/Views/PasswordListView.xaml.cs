/*
    Program Author:  Suwan Aryal
    USM ID: w10168297
    Assignment: Password Manager, Part 2, Back-End
    
    Description:
        This class displays the list of passwords and other functions like copy,edit or delete.
*/
using System.Collections.ObjectModel;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class PasswordListView : ContentPage
{
    private ObservableCollection<PasswordRow> _rows = new ObservableCollection<PasswordRow>();

    public PasswordListView()
    {
        InitializeComponent();

        //once logged in, generate a set of test passwords for the user.
        App.PasswordController.GenTestPasswords();

        //Populates the list of shown passwords  This should also be called in the search
        //bar event method to implement the search filter.
        App.PasswordController.PopulatePasswordView(_rows);

        //Binds the Collection View to the password rows.
        collPasswords.ItemsSource = _rows;

        lblName.Text = $"Welcome, {App.LoginController.GetCurrentUser().FirstName}!";

        
    }

    private void TextSearchBar(object sender, TextChangedEventArgs e)
    {
        //Is binded to the Search Bar.  Calls PopulatePasswords from the Password Controller.
        //to update the list of shown passwords.
        string search = e.NewTextValue;
        App.PasswordController.PopulatePasswordView(_rows, search);
    }

    private async void CopyPassToClipboard(object sender, EventArgs e)
    {
        //Is called when Copy is clicked.  Looks up the password by its ID
        //and copies it to the clipboard. 
        if (sender is Button button && button.CommandParameter is int passwordId)
        {
            var pw = App.PasswordController.GetPassword(passwordId);
            var curr = App.LoginController.GetCurrentUser();

            if (pw != null && curr != null)
            {
                var realPw = PasswordCrypto.Decrypt(pw.PasswordText, Tuple.Create(curr.Key, curr.IV));
                await Clipboard.Default.SetTextAsync(realPw);
                await DisplayAlert("Copied", "Password copied to clipboard!", "OK");
            }
            
        }
    }

    private void EditPassword(object sender, EventArgs e)
    {
        var button = (Button)sender;
        int id = Convert.ToInt32(button.CommandParameter);
        PasswordRow row_to_search = null ;

        
        foreach (var found_row in _rows)
        {
            if (found_row.PasswordID == id)
            {
                row_to_search = found_row;
                break;
            }
        }

        if (row_to_search != null)
        {
            if (row_to_search.Editing)
            {
                row_to_search.SavePassword();
                row_to_search.Editing = false;
                button.Text = "Edit";
                
            }
            else
            {
                row_to_search.Editing = true;
                button.Text = "Save";
            }
        }
    }


    private void DeletePassword(object sender, EventArgs e)
    {
        //Called when Delete is clicked.
        int id = Convert.ToInt32((sender as Button).CommandParameter);

        bool removed = App.PasswordController.RemovePassword(id);

        if (removed)
        {
            PasswordRow row_to_search = null;

            foreach (var row in _rows)
            {
                if (row.PasswordID == id)
                {
                    row_to_search = row;
                    break;
                }
            }
            if (row_to_search != null)
            {
                _rows.Remove(row_to_search);
            }
        }
    }
    private async void ButtonAddPassword(object sender, EventArgs e)
    {
        //Called when Add Password is clicked.  
        await Navigation.PushAsync(new AddPasswordView());
    }
}