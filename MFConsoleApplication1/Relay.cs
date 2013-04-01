using System;
using Microsoft.SPOT;

namespace PinballProject
{
    public class Relay
    {
        private string Name;
        private int IOPort;

        public Relay(string name, int IOport)
        {
            Name = name;
            IOPort = IOport;
        }

        public void Trigger()
        {

        }

    }
}
