package agi.samples.sharedresources;

public class StringUtilities
{
	public static String trimStringStart(String str, char[] trimChars)
	{
		int strLen = str.length();
		if(strLen == 0)
			return str;

		int index = 0;
		while(index != strLen && arrayContains(trimChars, str.charAt(index)))
		{
			index++;
		}
		return str.substring(index);
	}

	public static String trimStringEnd(String str, char[] trimChars)
	{
		int strLen = str.length();
		if(strLen == 0)
			return str;

		int index = strLen;
		while(index != 0 && arrayContains(trimChars, str.charAt(index - 1)))
		{
			index--;
		}
		return str.substring(0, index);
	}

	public static boolean arrayContains(char[] array, char charToFind)
	{
		for(int i=0; i< array.length; i++)
		{
			char c = array[i];
			if(c == charToFind)
				return true;
		}
		return false;
	}
}