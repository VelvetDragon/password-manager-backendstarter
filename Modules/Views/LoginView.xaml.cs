using CSC317PassManagerP2Starter.Modules.Controllers;
namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class LoginView : ContentPage
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void ProcessLogin(object sender, EventArgs e)
    {
        //Complete Process Login Functionality.  Called by Submit Button
        string username = txtUserName.Text;
        string password = txtPassword.Text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            ShowErrorMessage("Please fill out all fields.");
            return;
        }

        var verifyMsg = App.LoginController.Authenticate(username, password);

        if (verifyMsg == AuthenticationError.NONE)
        {
            Navigation.PushAsync(new PasswordListView());

        }
        else
        {
            if (verifyMsg == AuthenticationError.INVALIDUSERNAME)
            {
                ShowErrorMessage("Invalid username.");
            }
            else
            {
                ShowErrorMessage("Invalid password.");
            }
        }
    }

    private void ShowErrorMessage(string message)
    {
        //Complete ShowError Message functionality.  Supports ProcessLogin.
        lblError.Text = message;
        lblError.IsVisible = true;
    }
}