using System;
using Microsoft.SPOT;



namespace PandaPinball
{
    public class Player
    {
        /* State Definition: */



        private long Score;
        private int State;
        private int balls;


        public Player() {
            Score = 0;
            State = Pinball.PlayerNotActive;
            balls = 0;
        }

        public void SetupPlayer() {
            //This sets the player up, and joins them to the game
            balls = Pinball.Game.GetMaxBalls();
            State = Pinball.PlayerActiveNotPlayer;
            Score = 0;
        }

        public void AddScore(int ScoreToAdd)
        {
            if (State == Pinball.PlayerActivePlaying) { Score += ScoreToAdd; } else { Debug.Print("Can't add score to non active player!"); }
        }

        public int Ball()
        {
            return balls;
        }

        public void ChangeState(int NewState)
        {
            State = NewState;
        }

        public int LoseBall()
        {
            balls -= 1;
            if (balls < 0) { balls = 0; }
            return balls;   
        }

        public int CurrentState()
        {
            return State;
        }

        public void Reactivate() {
            //reset balls, keep them in the game, preserve score
            balls = Pinball.Game.GetMaxBalls();
        }
    
    }
}
