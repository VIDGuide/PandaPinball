using System;
using System.Collections;

using System.Text;
using Microsoft.SPOT;

namespace PandaPinball
{
    public class RuleProcessor
    {
        const string vbCrLf = "\r\n";

        

        public RuleProcessor()
        {
            //init script engine here
        }

        public void AddPlayer()
        {
            if (Pinball.Game.GetCredits() > 0)
            {
                Pinball.Game.ConsumeCredit(1);

                if (Pinball.Game.GetPlayersInGame() < Pinball.Game.GetMaxPlayers())
                {
                    if (Pinball.Game.GetGameState() != Pinball.Game_InProgress)
                    {
                        // play sound here
                        Pinball.Game.SetPlayersInGame(Pinball.Game.GetPlayersInGame()+1);
                        Pinball.Game.Players[Pinball.Game.GetPlayersInGame() - 1].SetupPlayer();
                        Pinball.Game.SetGameState(Pinball.Game_Standby);
                    }
                }
            }
            else
            {
                Debug.Print("Can't Add Player, No Credits!");
            }
        }

        public void AddCredit()
        {
            Pinball.Game.AddCredits(1);
        }

        public void BallReturn()
        {
            Pinball.Game.Relays[2].Trigger();
        }

        public void StartButton()
        {
            if (Pinball.Game.GetGameState()  != Pinball.Game_InProgress)
            {
                if (Pinball.Game.GetCredits() > 0 && Pinball.Game.GetPlayersInGame() < Pinball.Game.GetMaxPlayers())
                {
                    AddPlayer();
                }
                else
                {
                    //play sound here
                    Pinball.Game.SetGameState(Pinball.Game_InProgress);
                    Pinball.Game.SetCurrentPlayer(0);
                    Pinball.Game.Players[Pinball.Game.GetCurrentPlayer()].ChangeState(Pinball.PlayerActivePlaying);
                    BallReturn();
                }
            }
            else
            {
                if (Pinball.Game.GetGameState() == Pinball.Game_Ending)
                {
                    if (Pinball.Game.GetCredits() > 0)
                    {
                        Pinball.Game.ConsumeCredit(1);
                        Pinball.Game.Players[Pinball.Game.GetCurrentPlayer()].Reactivate();
                        //play sound here

                    }
                    else
                    {
                        Debug.Print("No Credit");
                    }
                }
                else
                {
                    Debug.Print("Start Button Pushed, nothing to do while in game.");
                }
            }
        }

        public void nextPlayer()
        {
            if (Pinball.Game.GetCurrentPlayer() > -1)
            {
                if (Pinball.Game.Players[Pinball.Game.GetCurrentPlayer()].LoseBall() == 0)
                {
                    //game over for currernt player - PlayerGameOver
                    // progress to 'game ending' state, 10 seconds for start button to consume credit to continue
                    // game waits for this time before ending player

                }
                else
                {
                    Pinball.Game.Players[Pinball.Game.GetCurrentPlayer()].ChangeState(Pinball.PlayerActiveNotPlayer);
                    int i = 0;
                    Boolean OK = false;
                    while (OK == false)
                    {
                        Pinball.Game.SetCurrentPlayer(Pinball.Game.GetCurrentPlayer()+1);
                        if (Pinball.Game.GetCurrentPlayer() > (Pinball.Game.GetMaxPlayers() - 1)) { Pinball.Game.SetCurrentPlayer(0); }
                        if (Pinball.Game.Players[Pinball.Game.GetCurrentPlayer()].Ball() > 0 && Pinball.Game.Players[Pinball.Game.GetCurrentPlayer()].CurrentState() == Pinball.PlayerActiveNotPlayer)
                        {
                            Pinball.Game.Players[Pinball.Game.GetCurrentPlayer()].ChangeState(Pinball.PlayerActivePlaying);
                            OK = true;
                            break;
                        }
                        i += 1;
                        if (i > Pinball.Game.GetMaxPlayers())
                        {
                            GameOver();
                            OK = true;
                            break;
                        }
                    }
                }

            }
            else
            {
                Debug.Print("End of Game, no more players");
            }
        }

        void GameOver()
        {
            //todo: show final scores, end of game stuff
            for (int i = 0; i < Pinball.Game.GetMaxPlayers(); i++)
            {
                Pinball.Game.Players[i].ChangeState(Pinball.PlayerNotActive);
            }
            Pinball.Game.SetCurrentPlayer(-1);
            Pinball.Game.SetPlayersInGame(0);
            Pinball.Game.SetGameState(Pinball.Game_GameOver);
        }

        public void RunScript(string Code)
        {
           //execute script code here
           
        }   
    }
}
