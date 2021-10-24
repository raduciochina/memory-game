using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memory_Game_App.Classes
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string PlayerFirstName
        {
            get => AppSettings.GetValueOrDefault(nameof(PlayerFirstName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(PlayerFirstName), value);
        }

        public static string PlayerLastName
        {
            get => AppSettings.GetValueOrDefault(nameof(PlayerLastName), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(PlayerLastName), value);
        }

        public static string PlayerEmail
        {
            get => AppSettings.GetValueOrDefault(nameof(PlayerEmail), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(PlayerEmail), value);
        }

        public static int TopScore
        {
            get => AppSettings.GetValueOrDefault(nameof(TopScore), 1);
            set => AppSettings.AddOrUpdateValue(nameof(TopScore), value);
        }

        public static DateTime DateAchieved
        {
            get => AppSettings.GetValueOrDefault(nameof(DateAchieved), DateTime.UtcNow);
            set => AppSettings.AddOrUpdateValue(nameof(DateAchieved), value);
        }

        public static bool IsProfileSync
        {
            get => AppSettings.GetValueOrDefault(nameof(IsProfileSync), false);
            set => AppSettings.AddOrUpdateValue(nameof(IsProfileSync), value);
        }

        public static int PlayerID
        {
            get => AppSettings.GetValueOrDefault(nameof(PlayerID), 0);
            set => AppSettings.AddOrUpdateValue(nameof(PlayerID), value);
        }

    }
}
//We are defining them in Settings.cs file so that we can access properties from shared code across all our applications, thus having a central location for shared properties.