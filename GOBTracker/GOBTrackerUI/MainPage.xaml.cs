namespace GOBTrackerUI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

<<<<<<< Updated upstream
            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
=======
        private void ScoreKeeping_Clicked(object sender, EventArgs e)
        {

        }

        private void GameView_Clicked(object sender, EventArgs e)
        {

        }

        private async void Statistics_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatModePage());
>>>>>>> Stashed changes
        }
    }

}
