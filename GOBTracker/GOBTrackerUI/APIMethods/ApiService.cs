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
        string gamesApiUrl = "https://localhost:7063/api/Games";
        string teamRosterApiUrl = "https://localhost:7063/api/TeamRoster";
        string playersApiUrl = "https://localhost:7063/api/Players";
        string playerTeamsApiUrl = "https://localhost:7063/api/PlayerTeams";
        string playerGameStatsApiUrl = "https://localhost:7063/api/PlayerGameStats";
        //string teamsApiUrl = "http://localhost:5123/api/Teams";
        //string teamRosterApiUrl = "http://localhost:5123/api/TeamRoster";
        //string playersApiUrl = "http://localhost:5123/api/Players";
        //string playerTeamsApiUrl = "http://localhost:5123/api/PlayerTeams";
        //string playerGameStatsApiUrl = "http://localhost:5123/api/PlayerGameStats";

        string HomeTeamGameStatsApiUrl = "https://localhost:7063/api/HomeTeamGameStats";
        string AwayTeamGameStatsApiUrl = "https://localhost:7063/api/AwayTeamGameStats";


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


        /*async public Task<List<PlayerGameStat>> GetGameStatsAsync(String selectedGame)  ///////finish this
        {
            string apiUrl = playerGameStatsApiUrl;

            List<PlayerGameStat> rawStatsFirstTeam = null;
            List<PlayerGameStat> rawStatsSecondTeam = null;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        rawStatsFirstTeam = JsonConvert.DeserializeObject<List<PlayerGameStat>>(jsonString);
                        rawStatsSecondTeam = JsonConvert.DeserializeObject<List<PlayerGameStat>>(jsonString);


                        rawStatsFirstTeam = rawStatsFirstTeam.Where(x => x.GameDateTime.Equals(selectedGame)).ToList(); //add 2nd condition
                        rawStatsSecondTeam = rawStatsSecondTeam.Where(x => x.GameDateTime.Equals(selectedGame)).ToList(); //add 2nd condition

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
        */


        async public Task<List<Game>> GetGamesAsync()
        {
            string apiUrl = playerGameStatsApiUrl;

            List<Game> games = null;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        games = JsonConvert.DeserializeObject<List<Game>>(jsonString);

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
            return games;
        }


        /*async public Task<List<List<PlayerGameStat>>> GetSeperateTeamStatsFromGameAsync(String thisGame)
        {
            string apiUrl = teamsApiUrl;

            string tempApiUrl = gamesApiUrl;


            List<PlayerGameStat> rawStatsFirstTeam = null;
            List<PlayerGameStat> rawStatsSecondTeam = null;

            List<Game> totalGames = null
            List<Team> bothTeams = null;
            
            //List<PlayerGameStat> rawStatsSecondTeam = null;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    HttpResponseMessage tempResponse = await client.GetAsync(tempApiUrl);

                    //totalGames


                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        rawStatsFirstTeam = JsonConvert.DeserializeObject<List<PlayerGameStat>>(jsonString);
                        rawStatsSecondTeam = JsonConvert.DeserializeObject<List<PlayerGameStat>>(jsonString);


                        rawStatsFirstTeam = rawStatsFirstTeam.Where(x => x.GameDateTime.Equals(thisGame)).ToList(); 
                        //rawStatsFirstTeam = rawStatsFirstTeam.Where(x => x..Equals(thisGame)).ToList(); //add conditition 4 team1 only


                        rawStatsSecondTeam = rawStatsSecondTeam.Where(x => x.GameDateTime.Equals(thisGame)).ToList();
                        //rawStatsSecondTeam = rawStatsSecondTeam.Where(x => x.GameDateTime.Equals(thisGame)).ToList(); //add condition 4 team2 only

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
        
        */


        async public Task<List<OurTeamGameStat>> GetHomeRawTeamStatsFromGameAsync(String gameSearch)
        {
            string apiUrl = HomeTeamGameStatsApiUrl;
            //string apiUrl2 = AwayTeamGameStatsApiUrl; 


            //List<HomeTeamGameStats> rawStatsHome = null;
            List<OurTeamGameStats> rawStatsAway = null;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        rawStatsHome = JsonConvert.DeserializeObject<List<HomeTeamGameStats>>(jsonString);
                        rawStatsAway = JsonConvert.DeserializeObject<List<AwayTeamGameStats>>(jsonString);


                        rawStatsHome = rawStatsHome.Where(x => x.GameDateTime.Equals(gameSearch)).ToList();
                        rawStatsAway = rawStatsAway.Where(x => x.GameDateTime.Equals(gameSearch)).ToList();

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

        async public Task<List<PlayerGameStat>> GetOpponentRawTeamStatsFromGameAsync(String gameSearch)
        {
            string apiUrl = HomeTeamGameStatsApiUrl;
            string apiUrl2 = AwayTeamGameStatsApiUrl;


            List<HomeTeamGameStats> rawStatsHome = null;
            List<AwayTeamGameStats> rawStatsAway = null;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        rawStatsHome = JsonConvert.DeserializeObject<List<HomeTeamGameStats>>(jsonString);
                        rawStatsAway = JsonConvert.DeserializeObject<List<AwayTeamGameStats>>(jsonString);


                        rawStatsHome = rawStatsHome.Where(x => x.GameDateTime.Equals(gameSearch)).ToList();
                        rawStatsAway = rawStatsAway.Where(x => x.GameDateTime.Equals(gameSearch)).ToList();

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




    }
}
