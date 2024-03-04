using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;


namespace GOBTrackerUI
{
    public partial class ManagementPage : ContentPage
    {
        public Team selectedTeam;
        public Player selectedPlayer;
        public ApiService apiService;
        public Schedule selectedGame;
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
                selectedTeam = teams.FirstOrDefault(team => team.TeamName == selectedTeamName);
                //load the players for the team

                LoadTeamRoster(selectedTeam.Id);
                rosterCollectionView.IsVisible = true;
                AddPlayerButton.IsEnabled = true;
                EditTeamButton.IsEnabled = true;
                DeleteTeamButton.IsEnabled = true;
                EditPlayerButton.IsEnabled = false;
                DeletePlayerButton.IsEnabled = false;



                //load the schedule
                LoadTeamSchedule(selectedTeam.Id);
                scheduleCollectionView.IsVisible = true;
                AddGameButton.IsEnabled = true;
                DeleteGameButton.IsEnabled = false;



            }

            
            
        }

        private void Game_SelectionChanged(object sender, EventArgs e)
        {
            selectedGame = (Schedule)scheduleCollectionView.SelectedItem;
            Debug.WriteLine(selectedGame.OurTeam.ToString() + " vs " + selectedGame.Opponent + " selected");
            
            DeleteGameButton.IsEnabled = true;

        }

        private async void Player_SelectionChanged(object sender, EventArgs e)
        {
            TeamRoster selectedRoster = (TeamRoster)rosterCollectionView.SelectedItem;
            var players = await apiService.GetPlayersAsync();
            selectedPlayer = players.FirstOrDefault(player => player.LastName == selectedRoster.LastName);
            Debug.WriteLine(selectedPlayer.FirstName.ToString() + " selected");
            EditPlayerButton.IsEnabled = true;
            DeletePlayerButton.IsEnabled = true;

        }

        async private void LoadTeamRoster(int teamId)
        {
            var roster = await apiService.GetTeamRosterByIdAsync(teamId);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                rosterCollectionView.ItemsSource = roster;
            });
        }

        async private void LoadTeamSchedule(int teamId)
        {
            var schedule = await apiService.GetTeamScheduleByIdAsync(teamId);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                scheduleCollectionView.ItemsSource = schedule;
            });
        }



        // Event handler for adding a new player
        private async void AddPlayer_Clicked(object sender, EventArgs e)
        {
            rosterCollectionView.IsVisible = false;
            var firstNameEntry = new Entry { Placeholder = "First Name" };
            var lastNameEntry = new Entry { Placeholder = "Last Name" };

            var saveButton = new Button { Text = "Save" };
            var exitButton = new Button { Text = "Exit Without Saving" };
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
                    LoadTeamRoster(selectedTeam.Id);
                }
                else
                {
                    // Display an alert if any field is empty
                    await DisplayAlert("Error", "Please fill in both first name and last name.", "OK");
                }
            };

            exitButton.Clicked += async (s, args) =>
            {
                await Navigation.PopModalAsync();
            };

            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Enter Player Details" },
                    firstNameEntry,
                    lastNameEntry,
                    saveButton,
                    exitButton
                }
            };

            var contentPage = new ContentPage
            {
                Content = stackLayout
            };

            await Navigation.PushModalAsync(contentPage);
        }



        private async void AddTeam_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("AddTeam_Clicked");

            var teamNameEntry = new Entry { Placeholder = "Team Name" };

            var saveButton = new Button { Text = "Save" };
            var exitButton = new Button { Text = "Exit Without Saving" };
            saveButton.Clicked += async (s, args) =>
            {
                string newTeamName = teamNameEntry.Text;

                // Check if fields are filled
                if (!string.IsNullOrWhiteSpace(newTeamName))
                {
                    // Proceed with adding the team
                    Team newTeam = new Team { TeamName = newTeamName };

                    // Call the AddTeamAsync method to attempt to add the team
                    bool success = await apiService.AddTeamAsync(newTeam);

                    // Check if adding the plyaer was successful
                    if (success)
                    {
                        // Player added successfully, you can show a message or perform any other action here
                        Debug.WriteLine("Team added successfully");
                        await DisplayAlert("Team Added", $"Team '{newTeamName}' added successfully!", "OK");

                    }
                    else
                    {
                        // Failed to add player
                        Debug.WriteLine("Failed to add team");
                    }


                    // Dismiss the popup
                    await Navigation.PopModalAsync();
                    LoadTeams();
                }
                else
                {
                    // Display an alert if any field is empty
                    await DisplayAlert("Error", "Please fill in team name.", "OK");
                }
            };

            exitButton.Clicked += async (s, args) =>
            {
                await Navigation.PopModalAsync();
            };

            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Enter Team Name" },
                    teamNameEntry,
                    saveButton,
                    exitButton
                }
            };

            var contentPage = new ContentPage
            {
                Content = stackLayout
            };

            await Navigation.PushModalAsync(contentPage);

        }

        private async void EditTeam_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("EditTeam_Clicked");

            var editedTeamNameEntry = new Entry { Placeholder = selectedTeam.TeamName.ToString() };

            var saveButton = new Button { Text = "Save" };
            var exitButton = new Button { Text = "Exit Without Saving" };
            saveButton.Clicked += async (s, args) =>
            {
                string editedTeamName = editedTeamNameEntry.Text;

                // Check if both fields are filled
                if (!string.IsNullOrWhiteSpace(editedTeamName))
                {
                    // Proceed with adding the player
                    Team editedTeam = new Team { Id = selectedTeam.Id, TeamName = editedTeamName };

                    // Call the AddPlayerAsync method to attempt to add the customer
                    bool success = await apiService.EditTeamByIdAsync(selectedTeam.Id, editedTeam);

                    // Check if adding the plyaer was successful
                    if (success)
                    {
                        // Player added successfully, you can show a message or perform any other action here
                        Debug.WriteLine("Team edited successfully");
                        await DisplayAlert("Team Edited", $"Team '{editedTeamName}' updated successfully!", "OK");

                    }
                    else
                    {
                        // Failed to add player
                        Debug.WriteLine("Failed to edit team");
                    }


                    // Dismiss the popup
                    await Navigation.PopModalAsync();
                    LoadTeams();
                }
                else
                {
                    // Display an alert if any field is empty
                    await DisplayAlert("Error", "Please fill in team name.", "OK");
                }
            };

            exitButton.Clicked += async (s, args) =>
            {
                await Navigation.PopModalAsync();
            };

            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Edit Team Name" },
                    editedTeamNameEntry,
                    saveButton,
                    exitButton
                }
            };

            var contentPage = new ContentPage
            {
                Content = stackLayout
            };

            await Navigation.PushModalAsync(contentPage);
            LoadTeamRoster(selectedTeam.Id);
        }


        private void DeleteTeam_Clicked(object sender, EventArgs e)
        {
            
        }

        // Event handler for editing an existing player
        private async void EditPlayer_Clicked(object sender, EventArgs e)
        {
            rosterCollectionView.IsVisible = false;
            var firstNameEntry = new Entry { Placeholder = selectedPlayer.FirstName.ToString() };
            var lastNameEntry = new Entry { Placeholder = selectedPlayer.LastName.ToString() };

            var saveButton = new Button { Text = "Save" };
            var exitButton = new Button { Text = "Exit Without Saving" };
            saveButton.Clicked += async (s, args) =>
            {
                string firstName = firstNameEntry.Text;
                string lastName = lastNameEntry.Text;

                // Check if both fields are filled
                if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
                {
                    // Proceed with adding the player
                    Player editedPlayer = new Player {Id = selectedPlayer.Id, FirstName = firstName, LastName = lastName };
                    string playerName = $"{firstName} {lastName}";
                    // Call the AddPlayerAsync method to attempt to add the customer
                    bool success = await apiService.EditPlayerByIdAsync(selectedPlayer.Id, editedPlayer);

                    // Check if adding the plyaer was successful
                    if (success)
                    {
                        // Player added successfully, you can show a message or perform any other action here
                        Debug.WriteLine("Player edited successfully");
                        await DisplayAlert("Player Edited", $"Player '{playerName}' updated successfully!", "OK");

                    }
                    else
                    {
                        // Failed to add player
                        Debug.WriteLine("Failed to edit player");
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

            exitButton.Clicked += async (s, args) =>
            {
                await Navigation.PopModalAsync();
            };

            var stackLayout = new StackLayout
            {
                Children =
                {
                    new Label { Text = "Edit Player Details" },
                    firstNameEntry,
                    lastNameEntry,
                    saveButton,
                    exitButton
                }
            };

            var contentPage = new ContentPage
            {
                Content = stackLayout
            };

            await Navigation.PushModalAsync(contentPage);
            LoadTeamRoster(selectedTeam.Id);
        }
    

        // Event handler for deleting a player
        private void DeletePlayer_Clicked(object sender, EventArgs e)
        {
            // Implement your logic for deleting a player
        }


        private async void AddGame_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLine("AddGame_Clicked");



            var locationEntry = new Entry { Placeholder = "Location", Margin = 20};

            var allTeams = await apiService.GetTeamsAsync();

            var homeTeamPicker = new Picker { ItemsSource = allTeams.Select(team => team.TeamName).ToList(), Margin =20 };

            var awayTeamPicker = new Picker { ItemsSource = allTeams.Select(team => team.TeamName).ToList(), Margin=20 };

            var datePicker = new DatePicker();
            datePicker.Margin=20;
            var timePicker = new TimePicker();
            timePicker.Margin=20;

            var saveButton = new Button { Text = "Save" };
            var exitButton = new Button { Text = "Exit Without Saving" };
            
            saveButton.Clicked += async (s, args) =>
            {
                String selectedHomeTeamName = homeTeamPicker.SelectedItem as String;
                Team selectedHomeTeamFromPicker = selectedTeam = allTeams.FirstOrDefault(team => team.TeamName == selectedHomeTeamName);

                String selectedAwayTeamName = awayTeamPicker.SelectedItem as String;
                Team selectedAwayTeamFromPicker = selectedTeam = allTeams.FirstOrDefault(team => team.TeamName == selectedAwayTeamName);

                DateTime selectedDate = datePicker.Date;
                TimeSpan selectedTime = timePicker.Time;

                DateTime selectedDateTime = selectedDate + selectedTime;

                string newLocation = locationEntry.Text;
                
                // Check if fields are filled
                if (!string.IsNullOrWhiteSpace(newLocation) || selectedHomeTeamName != null || selectedAwayTeamName != null)
                {
                    // Proceed with adding the game
                    
                    Game newGame = new Game { OurTeamId=selectedHomeTeamFromPicker.Id, OpponentTeamId=selectedAwayTeamFromPicker.Id, GameDateTime=selectedDateTime, Location=newLocation };

                    // Call the AddTeamAsync method to attempt to add the game
                    bool success = await apiService.AddGameAsync(newGame);

                    // Check if adding the game was successful
                    if (success)
                    {
                        // Player added successfully, you can show a message or perform any other action here
                        Debug.WriteLine("Game added successfully");
                        await DisplayAlert("Game Added", $"Game '{selectedHomeTeamName}' vs '{selectedAwayTeamName}' added successfully!", "OK");

                    }
                    else
                    {
                        // Failed to add player
                        Debug.WriteLine("Failed to add game");
                    }


                    // Dismiss the popup
                    await Navigation.PopModalAsync();
                    LoadTeams();
                }
                else
                {
                    // Display an alert if any field is empty
                    await DisplayAlert("Error", "Please fill in all fields.", "OK");
                }
            };

            exitButton.Clicked += async (s, args) =>
            {
                await Navigation.PopModalAsync();
            };

            var stackLayout = new StackLayout
            {
                
                Children =
                {
                    new Label { Text = "Enter Game Information", FontSize=40, Margin = 20, VerticalOptions=LayoutOptions.Center },

                    new HorizontalStackLayout
                    {
                        Children =
                        {
                            new Label {Text = "Select Home Team:", FontAttributes=FontAttributes.Bold, Margin=20, VerticalOptions=LayoutOptions.Center},
                            homeTeamPicker,

                            new Label {Text = "Select Away Team:", FontAttributes=FontAttributes.Bold, Margin=20, VerticalOptions = LayoutOptions.Center},
                            awayTeamPicker,
                        }
                    },

                    

                    new HorizontalStackLayout
                    {
                        
                        Children =
                        {
                            new Label {Text = "Select Date and Time:", FontAttributes=FontAttributes.Bold, Margin=20, VerticalOptions=LayoutOptions.Center },
                            datePicker,
                            timePicker,
                        }
                    },
                    
                    locationEntry,
                    saveButton,
                    exitButton,

                    
                }

                
            };

            var contentPage = new ContentPage
            {
                Content = stackLayout
            };

            await Navigation.PushModalAsync(contentPage);

        }

    }
}
    


