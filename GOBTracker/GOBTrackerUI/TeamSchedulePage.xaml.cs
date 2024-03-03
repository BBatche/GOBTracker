using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;


namespace GOBTrackerUI
{
    public partial class TeamSchedulePage : ContentPage
    {
        public Team selectedTeam;
        public Schedule selectedGame;
        public ApiService apiService;
        public TeamSchedulePage()
        {
            InitializeComponent();
            apiService = new ApiService();
            LoadTeams();

            
        }

        async private void LoadTeams()
        {
            var teams = await apiService.GetTeamsAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                
                teamPicker.ItemsSource = teams.Select(team => team.TeamName).ToList();
                //foreach (var team in teams)
                //{
                //    teamPicker.Items.Add(team.TeamName);
                //}
            });
        }

        
        // Event handler for when the selected team in the picker changes
        private async void TeamPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            

            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            
            if (selectedIndex != -1)
            {
                var selectedTeamName = picker.SelectedItem as String;
                var teams = await apiService.GetTeamsAsync();
                selectedTeam = teams.FirstOrDefault(team => team.TeamName == selectedTeamName);

                //load the schedule for the team

                LoadTeamSchedule(selectedTeam.Id);
                scheduleCollectionView.IsVisible = true;
                
                EnterScoreKeeperModeButton.IsEnabled = false;
                EnterGameViewModeButton.IsEnabled = false;
            }

            
            
        }

        private void Game_SelectionChanged(object sender, EventArgs e)
        {
            selectedGame = (Schedule)scheduleCollectionView.SelectedItem;
            Debug.WriteLine(selectedGame.OurTeam.ToString() + " vs " + selectedGame.Opponent + " selected");
            EnterScoreKeeperModeButton.IsEnabled = true;
            EnterGameViewModeButton.IsEnabled = true;

        }

        async private void LoadTeamSchedule(int teamId)
        {
            var schedule = await apiService.GetTeamScheduleByIdAsync(teamId);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                scheduleCollectionView.ItemsSource = schedule;
            });
        }

        private async void ScoreGame_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreKeepingPage(selectedSchedule));
        }


    }
}
    


