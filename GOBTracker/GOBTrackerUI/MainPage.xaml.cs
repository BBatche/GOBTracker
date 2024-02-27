using System.Runtime.Loader;

namespace GOBTrackerUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Management_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ManagementPage());
        }

        private void ScoreKeeping_Clicked(object sender, EventArgs e)
        {

        }

        private void GameView_Clicked(object sender, EventArgs e)
        {

        }

        private async void Statistics_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatsModePage());
        }
    }

}
