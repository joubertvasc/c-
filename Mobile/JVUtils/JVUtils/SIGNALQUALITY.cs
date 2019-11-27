using System;

namespace JVUtils
{
    public class SIGNALQUALITY
    {
        public string BitErrorRate;
        public string HighSignalStrength;
        public string LowSignalStrength;
        public string MaxSignalStrength;
        public string MinSignalStrength;
        public string SignalStrength;

        public SIGNALQUALITY(string SignalStrength, string MinSignalStrength, string MaxSignalStrength, string BitErrorRate, string LowSignalStrength, string HighSignalStrength)
        {
            this.SignalStrength = SignalStrength;
            this.MinSignalStrength = MinSignalStrength;
            this.MaxSignalStrength = MaxSignalStrength;
            this.BitErrorRate = BitErrorRate;
            this.LowSignalStrength = LowSignalStrength;
            this.HighSignalStrength = HighSignalStrength;
        }
    }
}

