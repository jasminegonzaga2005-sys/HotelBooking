namespace HotelBookingApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            string email = txtEmail.Text?.Trim() ?? "";
            string password = txtPassword.Text ?? "";

            if (email == "user@gmail.com" && password == "123456") //hardcoded credentials for demonstration purposes    
            {
                await DisplayAlert("Success", "Login Successful!", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

    }
}