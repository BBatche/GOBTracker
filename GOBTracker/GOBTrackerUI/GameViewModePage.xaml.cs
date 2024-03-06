using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;

namespace GOBTrackerUI;

public partial class GameViewModePage : ContentPage
{

    public int selectedGameId;
    public ApiService apiService;
    public Schedule thisSchedule;


    public GameViewModePage(Schedule _schedule)
    {
        InitializeComponent();
        apiService = new ApiService();
        //LoadGames();

        Team1StatLabel.IsVisible = false;
        Team2StatLabel.IsVisible = false;

        thisSchedule = _schedule;

        InitializeData();

        StartUpdateTimer();



    }

    private void StartUpdateTimer()
    {
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

    private async Task InitializeData()
    {
        //call api method
        var homePlayerStatsRawInGame = await apiService.GetHomeRawTeamStatsFromGameAsync(thisSchedule.GameId);
        var awayPlayerStatsRawInGame = await apiService.GetAwayRawTeamStatsFromGameAsync(thisSchedule.GameId);

        var teamsScoreInThisGame = await apiService.GetTeamGameScore(thisSchedule.GameId);

        bool view1Status = Team1StatCollectionView.IsVisible;
        bool view2Status = Team2StatCollectionView.IsVisible;

        Team1StatButton.Text = thisSchedule.OurTeam;
        Team2StatButton.Text = thisSchedule.Opponent;

        Team1StatLabel.Text = thisSchedule.OurTeam;
        Team2StatLabel.Text = thisSchedule.Opponent;


        MainThread.BeginInvokeOnMainThread(() => { Team1StatCollectionView.ItemsSource = homePlayerStatsRawInGame; });
        MainThread.BeginInvokeOnMainThread(() => { Team2StatCollectionView.ItemsSource = awayPlayerStatsRawInGame; });

        MainThread.BeginInvokeOnMainThread(() => { ScoreCollectionView.ItemsSource = teamsScoreInThisGame; });

        


        UpdateCollectionViewStatus(true, false);

        Team2StatButton.IsEnabled = true;
        Team1StatButton.IsEnabled = false;

        Team1StatLabel.IsVisible = true;
        Team2StatLabel.IsVisible = false;






    }


    private async Task UpdateData()
    {
        //call api method
        var homePlayerStatsRawInGame = await apiService.GetHomeRawTeamStatsFromGameAsync(thisSchedule.GameId);
        var awayPlayerStatsRawInGame = await apiService.GetAwayRawTeamStatsFromGameAsync(thisSchedule.GameId);

        var teamsScoreInThisGame = await apiService.GetTeamGameScore(thisSchedule.GameId);


        bool view1Status = Team1StatCollectionView.IsVisible;
        bool view2Status = Team2StatCollectionView.IsVisible;


        MainThread.BeginInvokeOnMainThread(() => { Team1StatCollectionView.ItemsSource = homePlayerStatsRawInGame; });
        MainThread.BeginInvokeOnMainThread(() => { Team2StatCollectionView.ItemsSource = awayPlayerStatsRawInGame; });

        MainThread.BeginInvokeOnMainThread(() => { ScoreCollectionView.ItemsSource = teamsScoreInThisGame; });



        UpdateCollectionViewStatus(view1Status, view2Status);

        


    }

    private void UpdateCollectionViewStatus(bool view1Status, bool view2Status)
    {
        Team1StatCollectionView.IsVisible = view1Status;  
        Team2StatCollectionView.IsVisible = view2Status;    
 
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