using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Memory_Game_App.Classes;


namespace Memory_Game_App.REST
{
    public class GameAPI
    {
        private const string APIUri = "localhost"; //replace this value with the published URI to where your API is hosted. E.g http://yourdomain.com/yourappname/api/game Jump
        HttpClient client;
        public GameAPI()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Host = "localhost";  //replace this value with the actual domain
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<bool> SavePlayerProfile(PlayerProfile data, bool isNew = false)
        {
            var uri = new Uri($"{APIUri}/AddPlayer");

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNew)
                response = await ProcessPostAsync(uri, content);


            if (response.IsSuccessStatusCode)
            {
               Settings.IsProfileSync = true;
                return true;
            }

            return false;
        }

        public async Task<bool> SavePlayerScore(PlayerScore data)
        {
            var uri = new Uri($"{APIUri}/AddScore");

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await ProcessPostAsync(uri, content);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<int> GetPlayerID(string email)
        {
            var uri = new Uri($"{APIUri}/GetPlayerID?email={email}");
            int id = 0;

            var response = await ProcessGetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                id = JsonConvert.DeserializeObject<int>(content);
            }

            return id;
        }

        public async Task<PlayerData> GetPlayerData(string email)
        {
            var uri = new Uri($"{APIUri}/GetPlayerProfile?email={email}");
            PlayerData player = null;

            var response = await ProcessGetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                player = new PlayerData();
                var content = await response.Content.ReadAsStringAsync();
                player = JsonConvert.DeserializeObject<PlayerData>(content);
            }

            return player;
        }

        private async Task<HttpResponseMessage> ProcessPostAsync(Uri uri, StringContent content)
        {
            return await client.PostAsync(uri, content); ;
        }

        private async Task<HttpResponseMessage> ProcessGetAsync(Uri uri)
        {
            return await client.GetAsync(uri);
        }
    }
}
//The class just contains some method that calls the API endpoints that we have created in the MemoyGame.API