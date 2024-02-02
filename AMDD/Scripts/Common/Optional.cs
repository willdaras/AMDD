namespace AMDD.Common;

/// <summary>
/// A way to bundle a variable with a bool.
/// </summary>
/// <typeparam name="T"> The type to store. </typeparam>
public struct Optional<T>
{
	public T value;
	public bool enabled;

	public Optional(T initialValue)
	{
		value = initialValue;
		enabled = true;
	}
}