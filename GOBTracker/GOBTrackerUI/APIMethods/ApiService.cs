﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GOBTrackerUI.Models;
using System.Diagnostics;
using static System.Net.WebRequestMethods;


namespace GOBTrackerUI.APIMethods
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;


        private static readonly string baseURL = "https://localhost:7063/api";
        //private static readonly string baseURL = "http://localhost:5123/api";

        private static readonly string teamsApiUrl = $"{baseURL}/Teams";
        private static readonly string gamesApiUrl = $"{baseURL}/Games";
        private static readonly string teamRosterApiUrl = $"{baseURL}/TeamRoster";
        private static readonly string playersApiUrl = $"{baseURL}/Players";
        private static readonly string playerTeamsApiUrl = $"{baseURL}/PlayerTeams";
        private static readonly string playerGameStatsApiUrl = $"{baseURL}/PlayerGameStats";
        private static readonly string schedulesApiUrl = $"{baseURL}/Schedules";
        private static readonly string HomeTeamGameStatsApiUrl = $"{baseURL}/OurTeamGameStats";
        private static readonly string AwayTeamGameStatsApiUrl = $"{baseURL}/OpponentTeamGameStats";
        private static readonly string statsUrl = $"{baseURL}/Stats";
        private static readonly string teamGameScoreApiUrl = $"{baseURL}/TeamGameScore";

     
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
                        //filter out the deleted teams
                        teams = teams.Where(x => x.IsDeleted != 1).ToList();
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

                        //filter out the deleted players
                        players = players.Where(x => x.IsDeleted != 1).ToList();
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
        async public Task<bool> AddStats(Stat stat)
        {
            string apiUrl = statsUrl;
            
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    string jsonStat = JsonConvert.SerializeObject(stat);
                    HttpContent content = new StringContent(jsonStat, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Stat added successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to add stat. Status code: " + response.StatusCode);
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


        async public Task<List<PlayerGameStat>> GetGameStatsAsync(String lastNameSearch)
        {
            string apiUrl = playerGameStatsApiUrl;
            List<PlayerGameStat> rawStats = null;

            using (HttpClient client = new HttpClient())
            {

                try {
                    
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

        
        async public Task<List<Game>> GetGamesAsync()
        {
            string apiUrl = gamesApiUrl;

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

        

        async public Task<List<OurTeamGameStat>> GetHomeRawTeamStatsFromGameAsync(int gameId)
        {
            string apiUrl = HomeTeamGameStatsApiUrl;


        
            List<OurTeamGameStat> rawStatsHome = null;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    
                    string urlWithId = $"{apiUrl}/{gameId}";
                    HttpResponseMessage response = await client.GetAsync(urlWithId);
                    

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        

                        rawStatsHome = JsonConvert.DeserializeObject<List<OurTeamGameStat>>(jsonString);


                        rawStatsHome = rawStatsHome.Where(x => x.GameId == gameId).ToList();

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
            return rawStatsHome;
        }

        async public Task<List<OpponentTeamGameStat>> GetAwayRawTeamStatsFromGameAsync(int gameId)
        {
            string apiUrl = AwayTeamGameStatsApiUrl;
            

            List<OpponentTeamGameStat> rawStatsAway = null;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string urlWithId = $"{apiUrl}/{gameId}";
                    HttpResponseMessage response = await client.GetAsync(urlWithId);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        rawStatsAway = JsonConvert.DeserializeObject<List<OpponentTeamGameStat>>(jsonString);
                        


                        rawStatsAway = rawStatsAway.Where(x => x.GameId == gameId).ToList();
                        

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
            return rawStatsAway;
        }

        async public Task<List<PlayerTeam>> GetPlayerTeamsByPlayerID(int playerID)
        {
            string apiUrl = playerTeamsApiUrl;


            List<PlayerTeam> playerTeams = null;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string urlWithId = $"{apiUrl}/player/{playerID}";
                    HttpResponseMessage response = await client.GetAsync(urlWithId);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        playerTeams = JsonConvert.DeserializeObject<List<PlayerTeam>>(jsonString);



                        playerTeams = playerTeams.Where(x => x.PlayerId == playerID).ToList();


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
            return playerTeams;
        }

        async public Task<bool> AddGameAsync(Game newGame)
        {
            string apiUrl = gamesApiUrl;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string jsonGame = JsonConvert.SerializeObject(newGame);
                    HttpContent content = new StringContent(jsonGame, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Game added successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to add game. Status code: " + response.StatusCode);
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
        public async Task<bool> DeleteGameByIdAsync(int gameId)
        {
            string apiUrl = gamesApiUrl;

            try
            {
                // Construct the URL with the game ID as part of the endpoint
                string urlWithId = $"{apiUrl}/{gameId}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(urlWithId);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Game deleted successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to delete game. Status code: " + response.StatusCode);
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


        
        async public Task<List<TeamGameScore>> GetTeamGameScore(int gameID)
        {
            string apiUrl = teamGameScoreApiUrl;


            List<TeamGameScore> gameScores = null;


            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string urlWithId = $"{apiUrl}/game/{gameID}";
                    HttpResponseMessage response = await client.GetAsync(urlWithId);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();

                        gameScores = JsonConvert.DeserializeObject<List<TeamGameScore>>(jsonString);



                        gameScores = gameScores.Where(x => x.GameId == gameID).ToList();


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
            return gameScores;
        }

        async public Task<bool> DeleteStatByIDasync(int statID)
        {
            string apiUrl = statsUrl;

            try
            {
                // Construct the URL with the game ID as part of the endpoint
                string urlWithId = $"{apiUrl}/{statID}";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.DeleteAsync(urlWithId);

                    if (response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Stat deleted successfully");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Failed to stat game. Status code: " + response.StatusCode);
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

            public async Task<List<Stat>> GetStats()
            {
                string apiUrl = statsUrl;

                List<Stat> stats = null;

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonString = await response.Content.ReadAsStringAsync();
                            stats = JsonConvert.DeserializeObject<List<Stat>>(jsonString);

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
                return stats;
            }

        
    }
}
