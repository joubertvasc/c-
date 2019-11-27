﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using JVUtils;

namespace OpenCellClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main(string[] args)
        {
            Application.Run(new OpenCellClientMain(args));
        }
    }
}