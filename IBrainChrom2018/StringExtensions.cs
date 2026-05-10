using System;
using System.Text.RegularExpressions;

namespace IBrainChrom2018;

public static class StringExtensions
{
	public static string PadLeftWhileDouble(this string input, int length, char paddingChar = '\0')
	{
		int singleLength = GetSingleLength(input);
		return input.PadLeft(length - singleLength + input.Length, paddingChar);
	}

	private static int GetSingleLength(string input)
	{
		if (string.IsNullOrEmpty(input))
		{
			throw new ArgumentNullException();
		}
		return Regex.Replace(input, "[^\\x00-\\xff]", "aa").Length;
	}

	public static string PadRightWhileDouble(this string input, int length, char paddingChar = '\0')
	{
		int singleLength = GetSingleLength(input);
		return input.PadRight(length - singleLength + input.Length, paddingChar);
	}
}
