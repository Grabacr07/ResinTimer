﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using MetroRadiance.UI.Controls;

namespace MetroRadiance.Internal
{
    internal static class InternalExtensions
    {
        /// <summary>
        /// ウィンドウ操作を実行します。
        /// </summary>
        /// <param name="action">実行するウィンドウ操作。</param>
        /// <param name="source">操作を実行しようとしている UI 要素。この要素をホストするウィンドウに対し、<paramref name="action"/> 操作が実行されます。</param>
        public static void Invoke(this WindowAction action, FrameworkElement source)
        {
            var window = Window.GetWindow(source);
            if (window == null) return;

            switch (action)
            {
                case WindowAction.Active:
                    window.Activate();
                    break;
                case WindowAction.Close:
                    window.Close();
                    break;
                case WindowAction.Maximize:
                    window.WindowState = WindowState.Maximized;
                    break;
                case WindowAction.Minimize:
                    window.WindowState = WindowState.Minimized;
                    break;
                case WindowAction.Restore:
                    window.WindowState = WindowState.Normal;
                    break;
                case WindowAction.OpenSystemMenu:
                    var point = source.PointToScreen(new Point(0, source.ActualHeight));
                    SystemCommands.ShowSystemMenu(window, point);
                    break;
            }
        }
    }
}
