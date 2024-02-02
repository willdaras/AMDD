namespace AMDD.Animation.Comparison;

/// <summary>
/// Compares whether an int is greater than another.
/// </summary>
public class GreaterThan : IComparer
{
	public bool Compare(int x, int y)
	{
		return x > y;
	}
}

/// <summary>
/// Compares whether an int is less than another.
/// </summary>
public class LessThan : IComparer
{
	public bool Compare(int x, int y)
	{
		return x < y;
	}
}

/// <summary>
/// Compares whether an int is equal to another.
/// </summary>
public class EqualTo : IComparer
{
	public bool Compare(int x, int y)
	{
		return x == y;
	}
}

/// <summary>
/// Compares whether an int is greater than or equal to another.
/// </summary>
public class GreaterThanOrEqual : IComparer
{
	public bool Compare(int x, int y)
	{
		return x >= y;
	}
}

/// <summary>
/// Compares whether an int is less than or equal to another.
/// </summary>
public class LessThanOrEqual : IComparer
{
	public bool Compare(int x, int y)
	{
		return x <= y;
	}
}

/// <summary>
/// Compares whether an int is not equal to another.
/// </summary>
public class NotEqual : IComparer
{
	public bool Compare(int x, int y)
	{
		return x != y;
	}
}