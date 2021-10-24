using MemoryGame.API.Models.DataManager;
using MemoryGame.API.Models.DB;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MemoryGame.API.API
{
    [EnableCors(origins: "http://localhost Jump :44362", headers: "*", methods: "*")]
    public class GameController : ApiController
    {
        GameManager _gm;
        public GameController()
        {
            _gm = new GameManager();
        }

        public IEnumerable<ChallengerViewModel> Get()
        {
            return _gm.GetAll;
        }

        [HttpPost]
        public HTTPApiResponse AddPlayer(Challenger user)
        {
            return _gm.AddChallenger(user);
        }

        [HttpPost]
        public void AddScore(Rank user)
        {
            _gm.UpdateCurrentBest(user);
        }

        [HttpPost]
        public HTTPApiResponse DeletePlayer(int id)
        {
            return _gm.DeleteChallenger(id);
        }

        public int GetPlayerID(string email)
        {
            return _gm.GetChallengerID(email);
        }

        public ChallengerViewModel GetPlayerProfile(string email)
        {
            return _gm.GetChallengerByEmail(email);
        }
    }
}
//Get()

//api / game / get

//Get all the challenger and rank data

//AddPlayer(Challenger user)

//api / game / addplayer

//Adds a new challenger

//AddScore(Rank user)

//api / game / addscore

//Adds or updates a challenger score

//DeletePlayer(int id)

//api / game / deleteplayer

//Removes a player

//GetPlayerID(string email)

//api / game / getplayerid

//Get the challenger id based on email

//GetPlayerProfile(string email)

//api / game / getplayerprofile

//Get challenger information based on email


