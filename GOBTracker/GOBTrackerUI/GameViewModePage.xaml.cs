using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Windows.Networking;

namespace GOBTrackerUI;

public partial class GameViewModePage : ContentPage
{

    public Game selectedGame;
    public ApiService apiService;

    public GameViewModePage()
	{
		InitializeComponent();
        apiService = new ApiService();
        LoadGames();
    }

    async private void LoadGames()
    {
        var games = await apiService.GetGamesAsync();

        MainThread.BeginInvokeOnMainThread(() =>
        {

            gamePicker.ItemsSource = games.Select(game => game.GameDateTime).ToList();
            //foreach (var team in teams)
            //{
            //    teamPicker.Items.Add(team.TeamName);
            //}
        });
    }

    private async void gamePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            var selectedGameName = picker.SelectedItem as String;

            //call api method
            var playerStatsInThisGame = await apiService.GetGameStatsAsync(selectedGameName);


            MainThread.BeginInvokeOnMainThread(() => { Team1StatCollectionView.ItemsSource = playerStatsInThisGame; });
            MainThread.BeginInvokeOnMainThread(() => { Team2StatCollectionView.ItemsSource = playerStatsInThisGame; });



        }
    }
}