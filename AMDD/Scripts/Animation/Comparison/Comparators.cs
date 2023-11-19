namespace AMDD.Animation.Comparison;

public class GreaterThan : IComparer
{
	public bool Compare(int x, int y)
	{
		return x > y;
	}
}

public class LessThan : IComparer
{
	public bool Compare(int x, int y)
	{
		return x < y;
	}
}

public class EqualTo : IComparer
{
	public bool Compare(int x, int y)
	{
		return x == y;
	}
}

public class GreaterThanOrEqual : IComparer
{
	public bool Compare(int x, int y)
	{
		return x >= y;
	}
}

public class LessThanOrEqual : IComparer
{
	public bool Compare(int x, int y)
	{
		return x <= y;
	}
}

public class NotEqual : IComparer
{
	public bool Compare(int x, int y)
	{
		return x != y;
	}
}