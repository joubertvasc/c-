using System;
using System.Runtime.CompilerServices;

namespace JVUtils
{
    public class RIL_EVENTS
    {
        public event SignalQualityChangedSelectedHandler SignalQualityChanged;

        public void RiseSignalQualityChanged(SIGNALQUALITY signal)
        {
            if (this.SignalQualityChanged != null)
            {
                this.SignalQualityChanged(signal);
            }
        }

        public delegate void SignalQualityChangedSelectedHandler(SIGNALQUALITY signal);
    }
}

