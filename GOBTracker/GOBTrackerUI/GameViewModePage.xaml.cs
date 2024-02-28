using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;

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

            var games = await apiService.GetGamesAsync();
            selectedGame = games.FirstOrDefault(game => game.GameDateTime.Equals(selectedGameName));
            

            //load the players for the team
            //LoadTeamRoster(selectedTeam.Id);
            //AddPlayerButton.IsEnabled = true;
        }
    }
}