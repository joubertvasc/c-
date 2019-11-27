using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace CommonDLL
{
    public static class CFModule
    {
        [DllImport("\\windows\\CFModule.dll")]
        public static extern int Initialize();

        [DllImport("\\windows\\CFModule.dll")]
        public static extern int Shutdown();

        [DllImport("\\windows\\CFModule.dll")]
        public static extern int CancelForward();

        [DllImport("\\windows\\CFModule.dll")]
        public static extern int ForwardCall(string lpszNumber, LINEFORWARDMODE dwMode, int nSeconds);

        [DllImport("\\windows\\RedirCalls.dll")]
        public static extern double xx(double a, double b);
    }
}
