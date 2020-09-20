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

		public static int Timeout
        {
			get => AppSettings.GetValueOrDefault(nameof(Timeout), 30);
			set => AppSettings.AddOrUpdateValue(nameof(Timeout), value);
        }

		public static int SumMax
		{
			get => AppSettings.GetValueOrDefault(nameof(SumMax), 50);
			set => AppSettings.AddOrUpdateValue(nameof(SumMax), value);
		}

		public static int DiffMax
		{
			get => AppSettings.GetValueOrDefault(nameof(DiffMax), 50);
			set => AppSettings.AddOrUpdateValue(nameof(DiffMax), value);
		}

		public static int MultMax
		{
			get => AppSettings.GetValueOrDefault(nameof(MultMax), 10);
			set => AppSettings.AddOrUpdateValue(nameof(MultMax), value);
		}
	}
}
