namespace AMDD.Common;

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