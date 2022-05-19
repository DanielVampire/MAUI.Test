namespace MauiProjectExp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFirstGameStarted(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameOne());
        }

        private async void OnSecondGameStarted(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameTwo());
        }
        private async void OnThirdGameStarted(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameThree());
        }
    }
}