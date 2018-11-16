namespace SatlockApp.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants
        const string token = "Token";
        const string user = "User";
        static readonly string stringDefault = string.Empty;
        #endregion

        public static string Token
        {
            get
            {
                return AppSettings.GetValueOrDefault(token, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(token, value);
            }
        }

        public static string Username
        {
            get
            {
                return AppSettings.GetValueOrDefault(user, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(user, value);
            }

        }
    }
}
