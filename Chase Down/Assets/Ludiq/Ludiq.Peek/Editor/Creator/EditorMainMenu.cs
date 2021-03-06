using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace Ludiq.Peek
{
	// ReSharper disable once RedundantUsingDirective
	using PeekCore;

	public static class EditorMainMenu
	{
		private const char Separator = '/';

		public static IEnumerable<string> Parse()
		{
			var menuString = EditorGUIUtility.SerializeMainMenuToString();

			using (var sr = new StringReader(menuString))
			{
				string line;

				var previousIndent = -1;

				string previousItem = null;

				while ((line = sr.ReadLine()) != null)
				{
					var spaceCount = 0;

					foreach (var c in line)
					{
						if (c == ' ')
						{
							spaceCount++;
						}
						else
						{
							break;
						}
					}

					var level = line.Trim();

					var indent = spaceCount / 4;

					string item;

					if (indent > previousIndent)
					{
						item = Combine(previousItem, level);
					}
					else if (indent < previousIndent)
					{
						var parent = GetParent(previousItem);

						for (var i = 0; i < previousIndent - indent; i++)
						{
							parent = GetParent(parent);
						}

						item = Combine(parent, level);
					}
					else // if (indent == previousIndent)
					{
						item = Combine(GetParent(previousItem), level);
					}

					yield return item;

					previousIndent = indent;
					previousItem = item;
				}
			}
		}

		private static string Combine(string parent, string level)
		{
			if (parent == null)
			{
				return level;
			}
			else
			{
				return $"{parent}{Separator}{level}";
			}
		}

		private static string GetParent(string item)
		{
			if (!item.Contains(Separator))
			{
				return null;
			}

			return item.PartBeforeLast(Separator);
		}
	}
}