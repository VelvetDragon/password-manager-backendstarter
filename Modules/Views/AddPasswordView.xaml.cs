/*
    Program Author:  Suwan Aryal
    USM ID: w10168297
    Assignment: Password Manager, Part 2, Back-End
    
    Description:
        This class deals the backend of the addpaswordview.xaml  for adding new passwords, either by entering existing ones ogenerating secure passwords.
*/
namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    bool generatedPassword;
    

    public AddPasswordView()
    {
        InitializeComponent();
        //Stores the original color of the text boxes. Used to revert a text box back
        //to its original color if it was "highlighted" during input validation.
        baseColorHighlight = (Color)Application.Current.Resources["ErrorEntryHighlightBG"];
        
        //Determines if a password was generated at least once.
        generatedPassword = false;

        //btnSubmitGen.IsEnabled = false; //disabling submit button at the start
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
        //Called when the Cancel button is clicked.
        Navigation.PopAsync();
    }

    private void ButtonSubmitExisting(object sender, EventArgs e)
    {
        //Called when the Submit button is clicked for a password manually
        //entered.  (i.e., existing password).
        lblErrorExistingPassword.IsVisible = false;

        
        if (string.IsNullOrWhiteSpace(txtNewPlatform.Text) ||
            string.IsNullOrWhiteSpace(txtNewUserName.Text) ||
            string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
            string.IsNullOrWhiteSpace(txtNewPasswordVerify.Text))
        {
            lblErrorExistingPassword.Text = "Fields are empty. Please fill out all the fields.";
            lblErrorExistingPassword.IsVisible = true;
            return;
        }
        
        if (txtNewPassword.Text != txtNewPasswordVerify.Text)
        {
            lblErrorExistingPassword.Text = "Incorrect Password. It does not match.";
            lblErrorExistingPassword.IsVisible = true;
            return;
        }
        App.PasswordController.AddPassword(txtNewPlatform.Text,txtNewUserName.Text, txtNewPassword.Text);

        Navigation.PushAsync(new PasswordListView());
    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        if (lblGenPassword.Text == "<No Password Generated>")
        {
            lblErrorGeneratedPassword.Text = "Fields are empty. Please fill out all the fields.";
            lblErrorGeneratedPassword.IsVisible = true;
            return;
        }
        
        if (string.IsNullOrWhiteSpace(txtNewPlatform.Text) ||
                string.IsNullOrWhiteSpace(txtNewUserName.Text) ||
                string.IsNullOrWhiteSpace(lblGenPassword.Text))
        {
            lblErrorGeneratedPassword.Text = "Fields are empty. Please fill out all the fields.";
            lblErrorGeneratedPassword.IsVisible = true;
            return;
        }

        App.PasswordController.AddPassword(txtNewPlatform.Text,txtNewUserName.Text,lblGenPassword.Text);
        Navigation.PushAsync(new PasswordListView());

        
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {

        lblErrorGeneratedPassword.IsVisible = false;

        bool includeUpperCase = chkUpperLetter.IsChecked;
        bool includeDigits = chkDigit.IsChecked;
        string requiredSymbols = string.Empty;
        int minimumLength = 6;

        if (chkSymbols.IsChecked)
        {
            requiredSymbols = txtReqSymbols.Text;
        }

        
        if (chkMinLength.IsChecked)
        {
            minimumLength = (int)sprPassLength.Value;
        }

        string generatedPasswordValue = PasswordGeneration.BuildPassword(upper_letter: includeUpperCase, digit: includeDigits, req_symbols: requiredSymbols, min_length: minimumLength);

        lblGenPassword.Text = generatedPasswordValue;
        generatedPassword = true;
        btnSubmitGen.IsEnabled = true;
    }
}