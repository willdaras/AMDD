namespace AMDD.Animation.Comparison;

/// <summary>
/// An abstract representation of a comparator.
/// </summary>
public interface IComparer
{
	public bool Compare(int x, int y);
}