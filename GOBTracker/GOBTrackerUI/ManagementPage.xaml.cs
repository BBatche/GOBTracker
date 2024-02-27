using GOBTrackerUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq;

namespace GOBTrackerUI
{
    public partial class ManagementPage : ContentPage
    {

        public ManagementPage()
        {
            InitializeComponent();
            LoadTeams();
            //viewModel = new PlayerViewModel(); // Initialize the ViewModel
            //BindingContext = viewModel; // Set the BindingContext to the ViewModel
        }

        async private void LoadTeams()
        {

            var teams = await GetTeamsAsync();

            MainThread.BeginInvokeOnMainThread(() =>
            {
                
                teamPicker.ItemsSource = teams.Select(team => team.TeamName).ToList();
                //foreach (var team in teams)
                //{
                //    teamPicker.Items.Add(team.TeamName);
                //}
            });
        }

        async private Task<List<Team>> GetTeamsAsync()
        {
            string apiUrl = "https://localhost:7063/api/Teams";
            List<Team> teams = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        teams = JsonConvert.DeserializeObject<List<Team>>(jsonString);

                    }
                    else
                    {
                        Debug.WriteLine("API request failed with status code: " + response.StatusCode);
                        return null;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    return null;
                }
            }
            return teams;
        }


        // Event handler for when the selected team in the picker changes
        private async void TeamPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            
            if (selectedIndex != -1)
            {
                var selectedTeamName = picker.SelectedItem as String;
                var teams = await GetTeamsAsync();
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
            var roster = await GetTeamRosterByIdAsync(teamId);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                rosterCollectionView.ItemsSource = roster;
            });
        }

        async private Task<List<TeamRoster>> GetTeamRosterByIdAsync(int teamId)
        {
            string apiUrl = "https://localhost:7063/api/TeamRoster";
            List<TeamRoster> roster = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string urlWithId = $"{apiUrl}/{teamId}";
                    HttpResponseMessage response = await client.GetAsync(urlWithId);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        roster = JsonConvert.DeserializeObject<List<TeamRoster>>(jsonString);

                    }
                    else
                    {
                        Debug.WriteLine("API request failed with status code: " + response.StatusCode);
                        return null;

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error: " + ex.Message);
                    return null;
                }
            }
            return roster;
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
    


