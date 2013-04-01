using System;
using System.Collections;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Hardware;
using GHI.Premium.Hardware;

namespace PandaPinball
{


    public class Pinball
    {
        public const int PlayerNotActive = 0;
        public const int PlayerActiveNotPlayer = 1;
        public const int PlayerActivePlaying = 2;
        public const int PlayerGameOver = 3;

        public const int Game_GameOver = 0;
        public const int Game_Standby = 1;
        public const int Game_InProgress = 2;
        public const int Game_Ending = 3;

        public static RuleProcessor ScriptEngine = new RuleProcessor();

        public static NightFever Game = new NightFever(); // Change this line to new 'game' class for different playfields/games
                
        public static void Main()
        {
            Debug.Print("Welcome to " + Pinball.Game.GetGameName() + ". Starting up...");
            Debug.Print("Game Started. Ready to Run");
            Thread.Sleep(Timeout.Infinite);
        }

        public void PlaySoundByFileName(string filename)
        {

        }
    }
}
