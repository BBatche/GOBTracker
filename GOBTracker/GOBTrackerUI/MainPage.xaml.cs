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

        private async void ScoreKeeping_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreKeepingPage());
        }

       
        private async void Statistics_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatsModePage());
        }

        private async void ViewSchedule_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TeamSchedulePage());
        }
    }

}
