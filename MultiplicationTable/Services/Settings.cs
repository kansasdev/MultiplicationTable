using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplicationTable.Services
{
	public static class Settings
	{
		private static ISettings AppSettings => CrossSettings.Current;

		public static string WorkMode
		{
			get => AppSettings.GetValueOrDefault(nameof(WorkMode), string.Empty);
			set => AppSettings.AddOrUpdateValue(nameof(WorkMode), value);
		}
	}
}
