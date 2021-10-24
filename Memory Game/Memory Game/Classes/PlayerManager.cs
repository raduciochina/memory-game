using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Game_App.Classes
{
    #region API DTO's
    public class PlayerProfile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class PlayerScore
    {
        public int ChallengerID { get; set; }
        public byte Best { get; set; }
        public DateTime DateAchieved { get; set; }
    }

    public class PlayerData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Best { get; set; }
        public DateTime DateAchieved { get; set; }
    }

    #endregion

    public static class PlayerManager
    {
        public static void Save(PlayerProfile player)
        {
            Settings.PlayerFirstName = player.FirstName;
            Settings.PlayerLastName = player.LastName;
            Settings.PlayerEmail = player.Email;
        }

        public static PlayerProfile GetPlayerProfileFromLocal()
        {
            return new PlayerProfile
            {
                FirstName = Settings.PlayerFirstName,
                LastName = Settings.PlayerLastName,
                Email = Settings.PlayerEmail
            };
        }

        public static PlayerScore GetPlayerScoreFromLocal()
        {
            return new PlayerScore
            {
                ChallengerID = Settings.PlayerID,
                Best = Convert.ToByte(Settings.TopScore),
                DateAchieved = Settings.DateAchieved
            };
        }

        public static void UpdateBest(int score)
        {
            if (Settings.TopScore < score)
            {
                Settings.TopScore = score;
                Settings.DateAchieved = DateTime.UtcNow;
            }
        }

        public static int GetBestScore(int currentLevel)
        {
            if (Settings.TopScore > currentLevel)
                return Settings.TopScore;
            else
                return currentLevel;
        }

        public async static Task<bool> Sync()
        {
            REST.GameAPI api = new REST.GameAPI();
            bool result = false;

            try
            {
                if (!Settings.IsProfileSync)
                    result = await api.SavePlayerProfile(PlayerManager.GetPlayerProfileFromLocal(), true);

                if (Settings.PlayerID == 0)
                    Settings.PlayerID = await api.GetPlayerID(Settings.PlayerEmail);

                result = await api.SavePlayerScore(PlayerManager.GetPlayerScoreFromLocal());

            }
            catch
            {
                return result;
            }

            return result;
        }

        public async static Task<bool> CheckScoreAndSync(int score)
        {
            if (Settings.TopScore < score)
            {
                UpdateBest(score);
                if (Utils.IsConnectedToInternet())
                {
                    var response = await Sync();
                    return response == true ? true : false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        public async static Task<PlayerData> CheckExistingPlayer(string email)
        {
            REST.GameAPI api = new REST.GameAPI();
            PlayerData player = new PlayerData();

            if (Utils.IsConnectedToInternet())
            {
                player = await api.GetPlayerData(email);
            }

            return player;
        }
    }
}
//The Save() method saves the player information within the application settings.
//The GetPlayerProfileFromLocal() method fetches the player information from the application settings.
//The GetPlayerScoreFromLocal() method fetches the player score details from the application settings.
//The UpdateBest() method updates the player score details within the application settings.
//The GetBestScore() method fetches the player top score from the application settings.
//The asynchronous Sync() method syncs the player profile and score details with data from the database into the local application settings.
//The asynchronous CheckScoreAndSync() method updates the top score to the database.
//The asynchronous CheckExistingPlayer() method verifies the existence of a player from the database.