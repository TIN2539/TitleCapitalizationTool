using System;
using System.Linq;
using System.Text;

namespace TitleCapitalizationTool
{
	internal class Program
	{
		internal static void Main(string[] stringsFromConsole)
		{
			string[] exceptions = {"A", "An", "And", "At", "But", "By", "For", "In", "Nor", "Of", "On", "Or", "Out", "So", "The", "To", "Up", "Yet"};
			char[] punctuationEndMarks = { '!', '?', '.' };
			char[] punctuationMarks = { ',', ':', ';', '!', '?', '.' };
			char[] separator = { ' ' };
			string userString;
			int index = 0;
			do
			{
				if (stringsFromConsole.Length == 0)
				{
					Console.Write("Enter title to capitalize: ");
					Console.ForegroundColor = ConsoleColor.Red;
					userString = Console.ReadLine();
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				else
				{
					userString = stringsFromConsole[index];
					Console.Write("Original title: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(userString);
					Console.ForegroundColor = ConsoleColor.Gray;
				}

				if (userString.Length > 0)
				{
					userString = userString.ToLower();

					string[] words = userString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
					userString = string.Join(" ", words);

					StringBuilder builder = new StringBuilder(userString);
					// Insert ' ' after punctuation marks
					for (int i = 0; i < builder.Length; i++) 
					{
						for (int x = 0; x < punctuationMarks.Length; x++)
						{
							if (builder[i] == punctuationMarks[x] && i + 1 < builder.Length && builder[i + 1] != ' ')
							{
								builder.Insert(i + 1, ' ');
								break;
							}
						}
					}
					// Remove ' ' before punctuation marks
					for (int i = 0; i < builder.Length; i++)
					{
						for (int x = 0; x < punctuationMarks.Length; x++)
						{
							if (i - 1 > 0 && builder[i] == punctuationMarks[x] && builder[i - 1] == ' ')
							{
								builder.Remove(i - 1, 1);
								break;
							}
						}
					}
					// Insert ' ' before and after '-'
					for (int i = 0; i < builder.Length; i++)
					{
						if (builder[i] == '-')
						{
							if (builder[i + 1] != ' ')
							{
								builder.Insert(i + 1, ' ');
							}
							if (builder[i - 1] != ' ')
							{
								builder.Insert(i, ' ');
							}
						}
					}

					userString = builder.ToString();
					words = userString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
					// Upper first letter
					for (int i = 0; i < words.Length; i++) 
					{
						if (words[i].Length > 1)
						{
							words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1, words[i].Length - 1).ToLower();
						}
						else
						{
							words[i] = words[i].ToUpper();
						}
					}
					// Include exeption (article e.t.)
					for (int i = 0; i < words.Length; i++)  
					{
						for (int j = 0; j < exceptions.Length; j++)
						{
							if (words[i].Equals(exceptions[j]))
							{
								words[i] = words[i].ToLower();
								break;
							}
						}
					}
					// If article is the first word in the sentence
					if (words.Length > 1)
					{
						words[0] = words[0].Substring(0, 1).ToUpper() + words[0].Substring(1, words[0].Length - 1);
					}
					// Upper first and last words in the sentence
					for (int i = 1; i < words.Length; i++)  
					{
						for (int x = 0; x < punctuationEndMarks.Length; x++)
						{
							if (i + 1 < words.Length && words[i].Last() == punctuationEndMarks[x])
							{
								words[i] = words[i].Substring(0, 1).ToUpper() + words[i].Substring(1, words[i].Length - 1);
								words[i + 1] = words[i + 1].Substring(0, 1).ToUpper() + words[i + 1].Substring(1, words[i + 1].Length - 1);
								break;
							}

						}
					}
					userString = string.Join(" ", words);
					Console.Write("Capitalized title: ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(userString);
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Gray;
					if (index < stringsFromConsole.Length)
					{
						index++;
					}
				}
				else
				{
					Console.SetCursorPosition(0, Console.CursorTop - 1);
				}
			} while (index < stringsFromConsole.Length || stringsFromConsole.Length == 0);
		}
	}
}