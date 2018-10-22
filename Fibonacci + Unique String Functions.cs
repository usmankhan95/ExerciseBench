static bool IsUnique(string phrase)
{
	for(int i = 0; i < phrase.Length; i++)
	{
		for(int k = i+1; k < phrase.Length; k++)
		{
			if (phrase[i] == phrase[k])
				return false;
		}
	}
	
	return true;
}

static string UniqueString(string phrase)
{
	HashSet<char> charSet = new HashSet<char>();
	StringBuilder sb = new StringBuilder();
	foreach(char c in phrase)
	{
		if (charSet.Add(c))
		{
			sb.Append(c);
		}
	}
	
	return sb.ToString();
}

static int[] Fibonacci(int n)
{
	int[] arr = new int[n];
	int a = 0, b = 1;
	for(int i = 0; i < n; i++)
	{
		int temp = a;
		a = b;
		b = temp + b;
		arr[i] = b;
	}
	return arr;
}