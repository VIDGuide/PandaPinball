using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHI.Premium.Hardware;

namespace PandaPinball
{
    public class NightFever
    {
        const int BallsPerGame = 5;
        const int MaxPlayers = 2;
        const string GameName = "Night Fever";

        const int NumSwitches = 100;
        const int NumLamps = 100;
        const int NumRelays = 5;

        Switch[] Switches = new Switch[NumSwitches];
        public Lamp[] Lamps = new Lamp[NumLamps];
        public Relay[] Relays = new Relay[NumRelays];

        public Player[] Players = new Player[MaxPlayers];

        int CurrentPlayer = -1;
        int Credits = 0;
        int PlayersInGame = 0;
        int GameState = Pinball.Game_GameOver;


        public NightFever()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                Players[i] = new Player();
            }
           
            /* Lamp Definitions */
            Lamps[0] = new Lamp("Player 1 Lamp", 1);
            Lamps[1] = new Lamp("Player 2 Lamp", 2);

            /* Relay Definitions */
            Relays[0] = new Relay("Knocker",0);
            Relays[1] = new Relay("Locked Ball Eject",1);
            Relays[2] = new Relay("Ball Return",2);
            Relays[3] = new Relay("Left Bank Reset",3);
            Relays[4] = new Relay("Right Bank Reset", 4);

            /* Switch Definitions */
            Switches[0] = new Switch("Ball Detect", Cpu.Pin.GPIO_Pin3, "","", Port.InterruptMode.InterruptEdgeBoth);
            Switches[1] = new Switch("Outer Left Drain", Cpu.Pin.GPIO_Pin5, "", "", Port.InterruptMode.InterruptEdgeLow);
            Switches[2] = new Switch("Left Coin Insert", Cpu.Pin.GPIO_Pin4, "ADDCREDIT 1:PLAYSOUND CreditSound.wav", "", Port.InterruptMode.InterruptEdgeLow);
            Switches[3] = new Switch("Right Coin Insert", Cpu.Pin.GPIO_Pin6, "ADDCREDIT 2:PLAYSOUND CreditSound.wav", "", Port.InterruptMode.InterruptEdgeLow);
            Switches[4] = new Switch("Credit Button", Cpu.Pin.GPIO_Pin7, "ADDCREDIT 1:PLAYSOUND CreditSound.wav", "", Port.InterruptMode.InterruptEdgeLow);
            Switches[5] = new Switch("Start Button", Cpu.Pin.GPIO_Pin10, "", "", Port.InterruptMode.InterruptEdgeLow);
        }

        public int GetMaxBalls()
        {
            return BallsPerGame;
        }

        public int GetMaxPlayers()
        {
            return MaxPlayers;
        }

        public string GetGameName()
        {
            return GameName;
        }

        public int GetCurrentPlayer()
        {
            return CurrentPlayer;
        }

        public void SetCurrentPlayer(int NewCurrentPlayer)
        {
            CurrentPlayer = NewCurrentPlayer;
        }

        public int GetCredits()
        {
            return Credits;
        }

        public void AddCredits(int CreditsToAdd)
        {
            Credits += CreditsToAdd;
            if (Credits > 9) { Credits = 9; }
        }

        public void ConsumeCredit(int NumCredits)
        {
            Credits -= NumCredits;
            if (Credits < 0) { Credits = 0;}
        }

        public int CurrentPlayerBallsRemaining() {
            if (CurrentPlayer > -1) { return Players[CurrentPlayer].Ball(); } else { return 0; }
        }

        public int GetPlayersInGame() {
            return PlayersInGame;
        }

        public void SetPlayersInGame(int SetNumPlayers)
        {
            PlayersInGame = SetNumPlayers;
            if (PlayersInGame < 0) { PlayersInGame = 0; }
            if (PlayersInGame > MaxPlayers) { PlayersInGame = MaxPlayers; }
        }

        public int GetGameState()
        {
            return GameState;
        }

        public void SetGameState(int NewGameState)
        {
            GameState = NewGameState;
        }
    }
}
