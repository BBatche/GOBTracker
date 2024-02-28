using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Windows.Networking;

namespace GOBTrackerUI;

public partial class GameViewModePage : ContentPage
{

    public int selectedGameId;
    public ApiService apiService;

    //public var homePlayerStatsRawInGame;
    //public var awayPlayerStatsRawInGame;

    public GameViewModePage()
	{
		InitializeComponent();
        apiService = new ApiService();
        LoadGames();

        Team1StatLabel.IsVisible = false;
        Team2StatLabel.IsVisible = false;



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

            selectedGameId = (int)picker.SelectedItem;


            await UpdateData();

            // Start a timer to refresh data every, for example, 30 seconds
            Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            {
                // Update data periodically
                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await UpdateData();
                });

                // Continue the timer
                return true;
            });


        }
    }



    private async Task UpdateData()
    {
        //call api method
        var homePlayerStatsRawInGame = await apiService.GetHomeRawTeamStatsFromGameAsync(selectedGameId);
        var awayPlayerStatsRawInGame = await apiService.GetAwayRawTeamStatsFromGameAsync(selectedGameId);


        bool view1Status = Team1StatCollectionView.IsEnabled;
        bool view2Status = Team2StatCollectionView.IsEnabled;


        MainThread.BeginInvokeOnMainThread(() => { Team1StatCollectionView.ItemsSource = homePlayerStatsRawInGame; });
        MainThread.BeginInvokeOnMainThread(() => { Team2StatCollectionView.ItemsSource = awayPlayerStatsRawInGame; });

        Team1StatCollectionView.IsEnabled = view1Status;
        Team2StatCollectionView.IsEnabled = view2Status;


    }

    private void ToggleTeam1StatsVisibility_Clicked(object sender, EventArgs e)
    {
        Team2StatButton.IsEnabled = true;
        Team1StatButton.IsEnabled = false;

        Team1StatLabel.IsVisible = true;

        Team1StatCollectionView.IsEnabled = true;
        Team2StatCollectionView.IsEnabled = false;


    }

    private void ToggleTeam2StatsVisibility_Clicked(object sender, EventArgs e)
    {
        Team2StatButton.IsEnabled = false;
        Team1StatButton.IsEnabled = true;

        Team2StatLabel.IsVisible = true;

        Team1StatCollectionView.IsEnabled = false;
        Team2StatCollectionView.IsEnabled = true;

    }
}