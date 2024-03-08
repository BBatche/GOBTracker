using GOBTrackerUI.APIMethods;
using GOBTrackerUI.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using System.Linq;


namespace GOBTrackerUI
{
    public partial class ScoreKeepingPage : ContentPage
    {
        public Team selectedTeam;
        public ApiService apiService;
        private Schedule selectedSchedule;
        public TeamRoster selectedPlayer;
        public PlayerTeam selectedPlayerTeam;
        public Stat mostRecentStat;
        public List<Stat> allStats;
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
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var teams = await apiService.GetTeamsAsync();
                teams = teams.Where(t => t.Id == selectedSchedule.OurTeamId || t.Id == selectedSchedule.OpponentTeamId).ToList();

                MainThread.BeginInvokeOnMainThread(() =>
                {

                    teamPicker.ItemsSource = teams.Select(team => team.TeamName).ToList();
                    //foreach (var team in teams)
                    //{
                    //    teamPicker.Items.Add(team.TeamName);
                    //}
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        
        // Event handler for when the selected team in the picker changes
        private async void TeamPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
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
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }

        private async void Player_SelectionChanged(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                selectedPlayer = (TeamRoster)rosterCollectionView.SelectedItem;

                var playerTeams = await apiService.GetPlayerTeamsByPlayerID(selectedPlayer.PlayerId);

                selectedPlayerTeam = playerTeams.FirstOrDefault(team => team.PlayerId == selectedPlayer.PlayerId);

                Console.WriteLine(selectedPlayer.ToString());
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            

        }

        async private void LoadTeamRoster(int teamId)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var roster = await apiService.GetTeamRosterByIdAsync(teamId);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    rosterCollectionView.ItemsSource = roster;
                });
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }

        private async void Made2PTButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 1, StatValue = 1 };

                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("2PT Made added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }

            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
        }

           

        private async void Miss2PTButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 2, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("2PT Miss added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void Made3PTButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 3, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("3PT Made added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void Miss3PTButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 4, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("3PT Miss added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void OffRebButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 12, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("OffReb added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void AssistButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 9, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("Assist added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
           
        }
        private async void TurnoverButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 8, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("Turnover added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void StealButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 6, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("Steal added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void DefRebButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 13, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("DefReb added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void BlockButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 10, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("Block added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void FoulButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 11, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("Foul added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
           
        }
        private async void FreeThrowButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 14, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {

                    mostRecentStat = stat;
                    Debug.WriteLine("Foul added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void FTMissButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 15, StatValue = 1 };
                bool success = await apiService.AddStats(stat);
                if (success)
                {
                    mostRecentStat = stat;
                    Debug.WriteLine("Foul added successfully");
                }
                else
                {
                    Debug.WriteLine("Add Failed");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
            
        }
        private async void UndoButton_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                if (mostRecentStat != null)
                {
                    var allStats = await apiService.GetStats();
                    Stat stat = allStats.LastOrDefault(allStats => allStats.PlayerTeamId == mostRecentStat.PlayerTeamId && allStats.GameId == mostRecentStat.GameId && allStats.StatTypeId == mostRecentStat.StatTypeId);

                    bool success = await apiService.DeleteStatByIDasync(stat.Id);
                    if (success)
                    {
                        Debug.WriteLine("Stat Deleted Sucessfully");
                    }
                    else
                    {
                        Debug.WriteLine("Couldn't Delete Stat");
                    }
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Uh Oh!", "No Internet", "OK");
                return;
            }
        }


    }
    
}
