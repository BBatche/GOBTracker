using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;


namespace GOBTrackerUI
{
    public partial class ScoreKeepingPage : ContentPage
    {
        public Team selectedTeam;
        public ApiService apiService;
        private Schedule selectedSchedule;
        public ScoreKeepingPage()
        {
            InitializeComponent();
            apiService = new ApiService();
            LoadTeams();
        }

        public ScoreKeepingPage(Schedule _selectedSchedule)
        {
            InitializeComponent();
            apiService= new ApiService();
            LoadTeams();
            selectedSchedule = _selectedSchedule;

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
                //load the players for the team

                LoadTeamRoster(selectedTeam.Id);
                
            }       
        }

        private async void Player_SelectionChanged(object sender, EventArgs e)
        {
            TeamRoster selectedPlayer = (TeamRoster)rosterCollectionView.SelectedItem;
           
            Console.WriteLine(selectedPlayer.ToString());

        }

        async private void LoadTeamRoster(int teamId)
        {
            var roster = await apiService.GetTeamRosterByIdAsync(teamId);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                rosterCollectionView.ItemsSource = roster;
            });
        }

        private void PlayerCollectionView_SelectionChanged()
        { 
            
        }

        private void Made2PTButton_Clicked(object sender, EventArgs e)
        {

        }

        private void Miss2PTButton_Clicked(object sender, EventArgs e)
        {

        }
        private void Made3PTButton_Clicked(object sender, EventArgs e)
        {

        }
        private void Miss3PTButton_Clicked(object sender, EventArgs e)
        {

        }
        private void OffRebButton_Clicked(object sender, EventArgs e)
        {

        }
        private void AssistButton_Clicked(object sender, EventArgs e)
        {

        }
        private void TurnoverButton_Clicked(object sender, EventArgs e)
        {

        }
        private void StealButton_Clicked(object sender, EventArgs e)
        {

        }
        private void DefRebButton_Clicked(object sender, EventArgs e)
        {

        }
        private void BlockButton_Clicked(object sender, EventArgs e)
        {

        }
        private void FoulButton_Clicked(object sender, EventArgs e)
        {

        }
        private void UndoButton_Clicked(object sender, EventArgs e)
        {

        }
    }

}