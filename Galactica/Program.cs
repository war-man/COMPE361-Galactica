﻿// Kevin Belew
// 818366010
// 12/8/17
using System;
using System.Windows.Forms;

/// <summary>
/// Used Tutorials found here: https://blogs.msdn.microsoft.com/tarawalker/2012/12/10/windows-8-game-development-using-c-xna-and-monogame-3-0-building-a-shooter-game-walkthrough-part-2-creating-the-shooterplayer-asset-of-the-game/
/// </summary>


namespace Galactica
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.Run(new MainMenu());

                
       
        }
    }
#endif
}
