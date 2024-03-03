using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GOBTrackerUI.Models;
using System.Diagnostics;

namespace GOBTrackerUI.APIMethods
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        string teamsApiUrl = "https://localhost:7063/api/Teams";
        string teamRosterApiUrl = "https://localhost:7063/api/TeamRoster";
        string playersApiUrl = "https://localhost:7063/api/Players";
        string playerTeamsApiUrl = "https://localhost:7063/api/PlayerTeams";
        string playerGameStatsApiUrl = "https://localhost:7063/api/PlayerGameStats";
        string schedulesApiUrl = "https://localhost:7063/api/Schedules";
        //string teamsApiUrl = "http://localhost:5123/api/Teams";
        //string teamRosterApiUrl = "http://localhost:5123/api/TeamRoster";
        //string playersApiUrl = "http://localhost:5123/api/Players";
        //string playerTeamsApiUrl = "http://localhost:5123/api/PlayerTeams";
        //string playerGameStatsApiUrl = "http://localhost:5123/api/PlayerGameStats";

        public ApiService()
        {
            _httpClient = new HttpClient();

            
        }

        async public Task<List<Team>> GetTeamsAsync()
        {
            string apiUrl = teamsApiUrl;
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

        async public Task<bool> AddTeamAsync(Team newTeam)
        {
            string apiUrl = teamsApiUrl;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonTeam = JsonConvert.SerializeObject(newTeam);
                    HttpContent content = new StringContent(jsonTeam, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Team added successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to add team. Status code: " + response.StatusCode);
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        async public Task<bool> EditTeamByIdAsync(int teamId, Team editedTeam)
        {
            string apiUrl = teamsApiUrl;

            try
            {
                string urlWithId = $"{apiUrl}/{teamId}";

                using (HttpClient client = new HttpClient())
                {
                    string jsonTeam = JsonConvert.SerializeObject(editedTeam);
                    HttpContent content = new StringContent(jsonTeam, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(urlWithId, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Team edited successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to edit team. Status code: " + response.StatusCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return false;
            }
        }


        async public Task<List<Player>> GetPlayersAsync()
        {
            string apiUrl = playersApiUrl;
            List<Player> players = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        players = JsonConvert.DeserializeObject<List<Player>>(jsonString);

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
            return players;
        }

        async public Task<bool> AddPlayerAsync(Player player)
        {
            string apiUrl = playersApiUrl;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonPlayer = JsonConvert.SerializeObject(player);
                    HttpContent content = new StringContent(jsonPlayer, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Player added successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to add player. Status code: " + response.StatusCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return false;
            }
        }

        async public Task<bool> EditPlayerByIdAsync(int playerId, Player updatedPlayer)
        {
            string apiUrl = playersApiUrl;

            try
            {
                string urlWithId = $"{apiUrl}/{playerId}";

                using (HttpClient client = new HttpClient())
                {
                    string jsonPlayer = JsonConvert.SerializeObject(updatedPlayer);
                    HttpContent content = new StringContent(jsonPlayer, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PutAsync(urlWithId, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Player edited successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to edit player. Status code: " + response.StatusCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return false;
            }
        }







        async public Task<List<TeamRoster>> GetTeamRosterByIdAsync(int teamId)
        {
            string apiUrl = teamRosterApiUrl;
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

        async public Task<bool> AddPlayerTeamAsync(PlayerTeam playerTeam)
        {
            string apiUrl = playerTeamsApiUrl;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonCustomer = JsonConvert.SerializeObject(playerTeam);
                    HttpContent content = new StringContent(jsonCustomer, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("PlayerTeam added successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to add playerTeam. Status code: " + response.StatusCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return false;
            }
        }
        async public Task<List<PlayerGameStat>> GetRawStatsAsync(String lastNameSearch)
        {
            string apiUrl = playerGameStatsApiUrl;
            List<PlayerGameStat> rawStats = null;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        rawStats = JsonConvert.DeserializeObject<List<PlayerGameStat>>(jsonString);
                        
                        rawStats = rawStats.Where(x => x.LastName.Equals(lastNameSearch)).ToList();
                        //rawStats = rawStats.Where(x => x.LastName.Equals("hi")).ToList();

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
            return rawStats;
        }


        async public Task<List<Schedule>> GetTeamScheduleByIdAsync(int teamId)
        {
            string apiUrl = schedulesApiUrl;
            List<Schedule> schedule = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string urlWithId = $"{apiUrl}/{teamId}";
                    HttpResponseMessage response = await client.GetAsync(urlWithId);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        schedule = JsonConvert.DeserializeObject<List<Schedule>>(jsonString);

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
            return schedule;
        }


    }
}
