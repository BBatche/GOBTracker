using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GOBTrackerUI
{
    public partial class ManagementPage : ContentPage
    {
        public ApiService apiService;
        public ManagementPage()
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
                Team selectedTeam = teams.FirstOrDefault(team => team.TeamName == selectedTeamName);
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
        { }

        // Event handler for adding a new player
        private void AddPlayer_Clicked(object sender, EventArgs e)
        {
            // Implement your logic for adding a new player
        }

        // Event handler for editing an existing player
        private void EditPlayer_Clicked(object sender, EventArgs e)
        {
            // Implement your logic for editing an existing player
        }

        // Event handler for deleting a player
        private void DeletePlayer_Clicked(object sender, EventArgs e)
        {
            // Implement your logic for deleting a player
        }
    }
}
    


