using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using JVUtils;

namespace rt
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
               // This line hide the wait cursor, reducing the time it's show when the software is
               // called by a SMS.
               IntPtr hOldCursor = Kernel.SetCursor(IntPtr.Zero);

               Application.Run(new RTMain(args));
            }
        }
    }
}