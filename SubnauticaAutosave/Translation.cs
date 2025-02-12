﻿using System;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace SubnauticaAutosave
{
	internal static class Translation
	{
		// private const string LanguagesFolder = "Languages";

		private static readonly Dictionary<string, string> languageStrings = new Dictionary<string, string>();

		// Json streaming code taken from Language.LoadLanguageFile(string)
		/* private static void LoadLanguageData()
		{
			string currentLanguage = Language.main.GetCurrentLanguage();

			if (string.IsNullOrEmpty(currentLanguage))
			{
				currentLanguage = "English";
			}

			string langFolder = Path.Combine(Entry.GetAssemblyDirectory, LanguagesFolder);
			string langFile = Path.Combine(langFolder, currentLanguage + ".json");

			if (!File.Exists(langFile))
			{
				langFile = Path.Combine(langFolder, "English.json");

				if (!File.Exists(langFile))
				{
					throw new Exception("[snAutosave] :: Could not find language file.");
				}
			}

			JsonData jsonData;

			using (StreamReader streamReader = new StreamReader(langFile))
			{
				try
				{
					jsonData = JsonMapper.ToObject(streamReader);
				}

				catch (Exception ex)
				{
					Entry.LogMessage(ex.ToString());
					Entry.LogMessage("Failed while loading language json.");

					return;
				}
			}

			foreach (string key in jsonData.Keys)
			{
				languageStrings[key] = (string)jsonData[key];
			}
		}*/

		private static bool TryTranslate(string candidate, out string translated)
		{
			if (languageStrings.TryGetValue(candidate, out translated))
			{
				return true;
			}

			else
			{
				//ReloadLanguage();

				if (languageStrings.TryGetValue(candidate, out translated))
				{
					return true;
				}
			}

			return false;
		}

		internal static string Translate(this string source)
		{
			if (TryTranslate(source, out string translated))
			{
				return translated;
			}

			Entry.LogMessage($"Could not find translated string for `{source}`");

			return source;
		}

		internal static string FormatTranslate(this string source, string arg0)
		{
			string basic = source.Translate();

			if (!string.IsNullOrEmpty(arg0))
			{
				try
				{
					return string.Format(basic, arg0);
				}

				catch (Exception ex)
				{
					Entry.LogMessage(ex.ToString());
					Entry.LogMessage($"Failed to format '{source}' with arg0 `{arg0}'");
				}
			}

			return basic;
		}

		//internal static void ReloadLanguage()
		//{
		//	languageStrings.Clear();

		//	LoadLanguageData();
		//}
	}
}
