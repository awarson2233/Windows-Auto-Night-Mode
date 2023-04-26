#region copyright
//  Copyright (C) 2022 Auto Dark Mode
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <https://www.gnu.org/licenses/>.
#endregion
using AutoDarkModeLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AutoDarkModeSvc.Events
{
    public class SwitchEventArgs : EventArgs
    {
        public SwitchEventArgs(SwitchSource source)
        {
            Source = source;
        }

        public SwitchEventArgs(SwitchSource source, Theme requestedTheme)
        {
            Source = source;
            Theme = requestedTheme;
        }

        public SwitchEventArgs(SwitchSource source, Theme requestedTheme, DateTime time, bool refreshDwm = false)
        {
            Source = source;
            Theme = requestedTheme;
            Time = time;
            RefreshDwm = refreshDwm;
        }

        public void OverrideTheme(Theme newTheme, ThemeOverrideSource overrideSource)
        {
            if (!Theme.HasValue)
            {
                Theme = newTheme;
            }
            else
            {
                Theme = newTheme;
                _themeOverrideSources.Add(overrideSource);
            }
        }

        /// <summary>
        /// Tries to set a switch time
        /// </summary>
        /// <param name="time">the switch time to set</param>
        /// <returns>true if a switch time was set; false if a switch time was already set</returns>
        public bool TrySetTime(DateTime time)
        {
            if (!Time.HasValue)
            {
                Time = time;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RefreshDwm { get; }
        public SwitchSource Source { get; }
        private List<ThemeOverrideSource> _themeOverrideSources { get; } = new();
        public ReadOnlyCollection<ThemeOverrideSource> ThemeOverrideSources { get { return new(_themeOverrideSources); } }
        public Theme? Theme { get; private set; } = null;
        public DateTime? Time { get; private set; } = null;
    }
}