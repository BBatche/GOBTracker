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
        public ScoreKeepingPage()
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
        /*private async void AddPlayer_Clicked(object sender, EventArgs e)
       {
           var firstNameEntry = new Entry { Placeholder = "First Name" };
           var lastNameEntry = new Entry { Placeholder = "Last Name" };

           var saveButton = new Button { Text = "Save" };
           saveButton.Clicked += async (s, args) =>
           {
               string firstName = firstNameEntry.Text;
               string lastName = lastNameEntry.Text;

               // Check if both fields are filled
               if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
               {
                   // Proceed with adding the player
                   Player newPlayer = new Player { FirstName=firstName, LastName=lastName };
                   string playerName = $"{firstName} {lastName}";
                   // Call the AddPlayerAsync method to attempt to add the customer
                   bool success = await apiService.AddPlayerAsync(newPlayer);

                   // Check if adding the plyaer was successful
                   if (success)
                   {
                       // Player added successfully, you can show a message or perform any other action here
                       Debug.WriteLine("Player added successfully");



                       //Assign Player to current team
                       //get the player you just added
                       var players = await apiService.GetPlayersAsync();
                       Player addedPlayer = players.FirstOrDefault( player => player.FirstName == firstNameEntry.Text );
                       //players = players.Where(x => x.LastName.Trim() == lastName ).ToList();
                       //Player addedPlayer = players[0];

                       //Grab the selected team

                       PlayerTeam newPlayerTeam = new PlayerTeam { PlayerId = addedPlayer.Id, TeamId = selectedTeam.Id };

                       bool success2 = await apiService.AddPlayerTeamAsync(newPlayerTeam);

                       if (success2)
                       {
                           Debug.WriteLine("PlayerTeam added successfully");
                           await DisplayAlert("Player Added", $"Player '{playerName}' added successfully!", "OK");
                       }
                       else
                       {
                           Debug.WriteLine("Failed to add PlayerTeam");
                       }



                   }
                   else
                   {
                       // Failed to add player
                       Debug.WriteLine("Failed to add player");
                   }


                   // Dismiss the popup
                   await Navigation.PopModalAsync();
                   LoadTeams();
               }
               else
               {
                   // Display an alert if any field is empty
                   await DisplayAlert("Error", "Please fill in both first name and last name.", "OK");
               }
           };

           var stackLayout = new StackLayout
           {
               Children =
               {
                   new Label { Text = "Enter Player Details" },
                   firstNameEntry,
                   lastNameEntry,
                   saveButton
               }
           };

           var contentPage = new ContentPage
           {
               Content = stackLayout
           };

           await Navigation.PushModalAsync(contentPage);
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
       */
    }

}
    


