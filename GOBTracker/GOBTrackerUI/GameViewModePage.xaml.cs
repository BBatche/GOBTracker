using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Windows.Networking;

namespace GOBTrackerUI;

public partial class GameViewModePage : ContentPage
{

    public int selectedGameId;
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
            gamePicker.ItemsSource = games.Select(game => game.Id).ToList();
            
            
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
            //var selectedGameName = picker.SelectedItem as String;

            selectedGameId = (int)picker.SelectedItem;

            //call api method
            var homePlayerStatsRawInGame = await apiService.GetHomeRawTeamStatsFromGameAsync(selectedGameId);
            var awayPlayerStatsRawInGame = await apiService.GetAwayRawTeamStatsFromGameAsync(selectedGameId);



            MainThread.BeginInvokeOnMainThread(() => { Team1StatCollectionView.ItemsSource = homePlayerStatsRawInGame; });
            MainThread.BeginInvokeOnMainThread(() => { Team2StatCollectionView.ItemsSource = awayPlayerStatsRawInGame; });



        }
    }
}