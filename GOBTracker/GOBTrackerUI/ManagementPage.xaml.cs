using GOBTrackerUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;

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
                teamlistview.ItemsSource = teams;
                teamPicker.ItemsSource = teams;
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
        private void TeamPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                //viewModel.SelectedTeam = viewModel.Teams[selectedIndex];
                //// Update the list of players based on the selected team
                //viewModel.UpdatePlayersByTeam();
            }
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
    


