using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;

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
        try
        {
            var games = await apiService.GetGamesAsync();
            if (games != null)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    gamePicker.ItemsSource = games.Select(game => game.Id).ToList();


                });
            }
            
        }
        catch (Exception ex)
        {

        }
        
    }

    private async void gamePicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        InitializeData();


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

    private async Task InitializeData()
    {
        //call api method
        var homePlayerStatsRawInGame = await apiService.GetHomeRawTeamStatsFromGameAsync(selectedGameId);
        var awayPlayerStatsRawInGame = await apiService.GetAwayRawTeamStatsFromGameAsync(selectedGameId);


        bool view1Status = Team1StatCollectionView.IsVisible;
        bool view2Status = Team2StatCollectionView.IsVisible;


        MainThread.BeginInvokeOnMainThread(() => { Team1StatCollectionView.ItemsSource = homePlayerStatsRawInGame; });
        MainThread.BeginInvokeOnMainThread(() => { Team2StatCollectionView.ItemsSource = awayPlayerStatsRawInGame; });


        UpdateCollectionViewStatus(true, false);

        Team2StatButton.IsEnabled = true;
        Team1StatButton.IsEnabled = false;

        Team1StatLabel.IsVisible = true;
        Team2StatLabel.IsVisible = false;






    }


    private async Task UpdateData()
    {
        //call api method
        var homePlayerStatsRawInGame = await apiService.GetHomeRawTeamStatsFromGameAsync(selectedGameId);
        var awayPlayerStatsRawInGame = await apiService.GetAwayRawTeamStatsFromGameAsync(selectedGameId);


        bool view1Status = Team1StatCollectionView.IsVisible;
        bool view2Status = Team2StatCollectionView.IsVisible;


        MainThread.BeginInvokeOnMainThread(() => { Team1StatCollectionView.ItemsSource = homePlayerStatsRawInGame; });
        MainThread.BeginInvokeOnMainThread(() => { Team2StatCollectionView.ItemsSource = awayPlayerStatsRawInGame; });


        UpdateCollectionViewStatus(view1Status, view2Status);

        


    }

    private void UpdateCollectionViewStatus(bool view1Status, bool view2Status)
    {
        Team1StatCollectionView.IsVisible = view1Status;  //not the issue
        Team2StatCollectionView.IsVisible = view2Status;    //not the issue

        //Team1StatLabel.IsVisible = view1Status;
        //Team2StatLabel.IsVisible = view2Status;  
    }

    private void ToggleTeam1StatsVisibility_Clicked(object sender, EventArgs e)
    {
        Team2StatButton.IsEnabled = true;
        Team1StatButton.IsEnabled = false;

        Team1StatLabel.IsVisible = true;
        Team2StatLabel.IsVisible = false;


        Team1StatCollectionView.IsVisible = true;
        Team2StatCollectionView.IsVisible = false;


    }

    private void ToggleTeam2StatsVisibility_Clicked(object sender, EventArgs e)
    {
        Team2StatButton.IsEnabled = false;
        Team1StatButton.IsEnabled = true;

        Team2StatLabel.IsVisible = true;
        Team1StatLabel.IsVisible = false;


        Team1StatCollectionView.IsVisible = false;
        Team2StatCollectionView.IsVisible = true;

    }
}