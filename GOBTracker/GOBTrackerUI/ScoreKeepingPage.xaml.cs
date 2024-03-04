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
            teams = teams.Where(t => t.Id == selectedSchedule.OurTeamId ||  t.Id == selectedSchedule.OpponentTeamId).ToList();

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
            selectedPlayer = (TeamRoster)rosterCollectionView.SelectedItem;

            var playerTeams = await apiService.GetPlayerTeamsByPlayerID(selectedPlayer.PlayerId);

            selectedPlayerTeam = playerTeams.FirstOrDefault(team => team.PlayerId == selectedPlayer.PlayerId);

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

        private async void Made2PTButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id , StatTypeId = 1, StatValue = 1};
            bool success = await apiService.AddStats(stat);
            if(success)
            {
                Debug.WriteLine("2PT Made added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }

        private async void Miss2PTButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 2, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("2PT Miss added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void Made3PTButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 3, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("3PT Made added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void Miss3PTButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 4, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("3PT Miss added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void OffRebButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 12, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("OffReb added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void AssistButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 9, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("Assist added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void TurnoverButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 8, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("Turnover added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void StealButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 6, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("Steal added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void DefRebButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 13, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("DefReb added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void BlockButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 10, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("Block added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private async void FoulButton_Clicked(object sender, EventArgs e)
        {
            Stat stat = new Stat { GameId = selectedSchedule.GameId, PlayerTeamId = selectedPlayerTeam.Id, StatTypeId = 11, StatValue = 1 };
            bool success = await apiService.AddStats(stat);
            if (success)
            {
                Debug.WriteLine("Foul added successfully");
            }
            else
            {
                Debug.WriteLine("Add Failed");
            }
        }
        private void UndoButton_Clicked(object sender, EventArgs e)
        {
            
        }
    }

}