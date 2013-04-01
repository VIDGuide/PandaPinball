using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHI.Premium.Hardware;

namespace PandaPinball
{
    public class Switch
    {
        private string SwitchName;
        private Cpu.Pin IOPort;
        private string DownScript;
        private string UpScript;

        public Switch(string switchName, Cpu.Pin IOport, string downScript, string upScript, Port.InterruptMode ActivateState)
        {
            SwitchName = switchName;
            IOPort = IOport;
            DownScript = downScript;
            UpScript = upScript;

            InterruptPort IntButton = new InterruptPort(IOPort, false, Port.ResistorMode.PullUp, ActivateState);
            IntButton.OnInterrupt += new NativeEventHandler(this.Press);
        }

        public void Press(uint port, uint state, DateTime time)
        {
            switch (state)
            {
                case 0:
                    Debug.Print("Switch Pressed: " + SwitchName);
                    Pinball.ScriptEngine.RunScript(DownScript);
                    break;
                case 1:
                    Debug.Print("Switch Released: " + SwitchName);
                    Pinball.ScriptEngine.RunScript(UpScript);
                    break;
                default:
                    Debug.Print("Unknown State Detected: " + state.ToString());
                    break;

            }
        }
    }
}
