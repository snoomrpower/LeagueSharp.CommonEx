﻿#region

using System;
using System.Windows.Forms;
using LeagueSharp.CommonEx.Core.Enumerations;
using LeagueSharp.CommonEx.Core.Utils;

#endregion

namespace LeagueSharp.CommonEx.Core.UI
{
    /// <summary>
    ///     Root of the User Interface.
    /// </summary>
    public class Root
    {
        /// <summary>
        ///     Returns if the menu shoudl be drawn.
        /// </summary>
        public static bool MenuVisible = true;

        /// <summary>
        ///     Windows Process Messages callback.
        /// </summary>
        /// <param name="args">
        ///     <see cref="WndEventArgs" />
        /// </param>
        private static void Game_OnWndProc(WndEventArgs args)
        {
            var keys = new WindowsKeys(args);
            if (keys.SingleKey == Keys.ShiftKey)
            {
                var value = keys.Msg == WindowsMessages.KEYDOWN;

                MenuVisible = value;

                if (value)
                {
                    MenuInterface.OnMenuOpen(null);
                }
                else
                {
                    MenuInterface.OnMenuClose(null);
                }
            }

            MenuInterface.OnWndProc(keys);
        }

        /// <summary>
        ///     Drawing callback.
        /// </summary>
        /// <param name="args">
        ///     <see cref="EventArgs" />
        /// </param>
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (MenuVisible)
            {
                MenuInterface.OnDraw();
            }
        }

        /// <summary>
        ///     Update callback.
        /// </summary>
        /// <param name="args">
        ///     <see cref="EventArgs" />
        /// </param>
        private static void Game_OnUpdate(EventArgs args) {}

        /// <summary>
        ///     Calls an init for the root user interface.
        /// </summary>
        public static void Init()
        {
            Game.OnUpdate += Game_OnUpdate;
            Drawing.OnEndScene += Drawing_OnDraw;
            Game.OnWndProc += Game_OnWndProc;
        }
    }
}