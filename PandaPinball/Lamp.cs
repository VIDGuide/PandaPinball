using System;
using System.Threading;
using Microsoft.SPOT;



namespace PandaPinball
{
    public class Lamp
    {
        string Name = "";
        int IOPort = 0;
        Boolean CurrentState = false;

        Timer FlashTimer;
        int FlashesRemaining = 0;
        int FlashIntervalInMS = 0;

        public Lamp(string name, int IOport)
        {
            Name = name;
            IOPort = IOport;
        }

        public void TurnOn()
        {
            this.CurrentState = true;
            //call IO port to turn lamp on
        }

        public void TurnOff()
        {
            this.CurrentState = false;
            //cal IO Port to turn lamp off
        }

        private void FlashAction(object o)
        {
            if (FlashesRemaining > 0)
            {
                FlashesRemaining -= 1;
                if (CurrentState) { TurnOff(); } else { TurnOn();}
                FlashTimer = new Timer(new TimerCallback(FlashAction), 0, FlashIntervalInMS, 0); //relaunch timer event if we have more flashes to do
            }
        }

        public void Flash(int DurationInMSBetweenStates, int numFlashes)
        {
            /* Note: Flashes count each state toggle. 1 Flash == 1 state flip */
            FlashesRemaining = numFlashes;
            FlashIntervalInMS = DurationInMSBetweenStates;
            FlashTimer = new Timer(new TimerCallback(FlashAction), 0, FlashIntervalInMS, 0);
            
        }

        public Boolean Status()
        {
            return CurrentState;
        }
    }
}
