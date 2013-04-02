using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;


namespace PandaPinball
{
    public class Lamp
    {
        string Name = "";
        FEZ_Pin.Digital IOPort;
        Boolean CurrentState = false;

        OutputPort LED;

        Timer FlashTimer;
        int FlashesRemaining = 0;
        int FlashIntervalInMS = 0;

        public Lamp(string name, FEZ_Pin.Digital IOport)
        {
            Name = name;
            IOPort = IOport;
            LED = new OutputPort((Cpu.Pin)IOPort, false);
        }

        public void TurnOn()
        {
            this.CurrentState = true;
            LED.Write(CurrentState);
        }

        public void TurnOff()
        {
            this.CurrentState = false;
            LED.Write(CurrentState);
        }

        private void FlashAction(object o)
        {
            if (FlashesRemaining > 0)
            {
                FlashesRemaining -= 1;
                if (CurrentState) { TurnOff(); } else { TurnOn();}
                //Debug.Print("Flashing " + Name + ". " + FlashesRemaining.ToString() + " toggles remaining.");
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
