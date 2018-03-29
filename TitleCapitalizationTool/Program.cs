using System;
using System.Linq;
using System.Text;

namespace TitleCapitalizationTool
{
	internal class Program
	{
		internal static void Main(string[] args)
		{
			char[] punctuationMarks = { ',', ':', ';', '!', '?', '.' };
			string[] exception = { "A", "An", "The", "And", "But", "For", "Nor", "So", "Yet", "At", "By", "In", "Of", "On", "Or", "Out", "To", "Up" };
			char[] separator = { ' ' };
			char[] punctuationEndMarks = { '!', '?', '.' };
			string userString;
			int index = 0;
			do
			{
				if (args.Length == 0)
				{
					Console.Write("Enter some string: ");
					Console.ForegroundColor = ConsoleColor.Red;
					userString = Console.ReadLine();
				}
				else
				{
					userString = args[index];
					Console.Write("Incorrect string: ");
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(userString);
				}

				if (userString.Length > 0)
				{
					userString = userString.ToLower();

					string[] words = userString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
					userString = string.Join(" ", words);

					StringBuilder builder = new StringBuilder(userString);
					for (int i = 0; i < builder.Length; i++) //Insert ' ' after punctuation marks
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
					for (int i = 0; i < builder.Length; i++) //Remove ' ' before punctuation marks
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

					for (int i = 0; i < builder.Length; i++) //Insert ' ' before and after '-'
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
					for (int i = 0; i < words.Length; i++)  //Upper first letter
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

					for (int i = 0; i < words.Length; i++)  //Include exeption (article e.t.)
					{
						for (int j = 0; j < exception.Length; j++)
						{
							if (words[i].Equals(exception[j]))
							{
								words[i] = words[i].ToLower();
								break;
							}
						}
					}

					if (words.Length > 1)
					{
						words[0] = words[0].Substring(0, 1).ToUpper() + words[0].Substring(1, words[0].Length - 1); //If article is the first word in the sentence
					}
					for (int i = 1; i < words.Length; i++)  //Upper first word in the sentence
					{
						for (int x = 0; x < punctuationEndMarks.Length; x++)
						{
							if (i + 1 < words.Length && words[i].Last() == punctuationEndMarks[x])
							{

								words[i + 1] = words[i + 1].Substring(0, 1).ToUpper() + words[i + 1].Substring(1, words[i + 1].Length - 1);
								break;
							}

						}
					}
					userString = string.Join(" ", words);
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write("Correct string: ");
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(userString);
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Gray;
					if (index < args.Length)
					{
						index++;
					}
				}
				else
				{
					Console.WriteLine("You didn't enter string!");
					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Gray;
				}
			} while (index < args.Length || args.Length == 0);
		}
	}
}