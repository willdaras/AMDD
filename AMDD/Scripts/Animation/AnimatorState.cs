using System.Collections;
using System.Collections.Generic;

namespace AMDD.Animation;

/// <summary>
/// A basic implementation of an animator state.
/// </summary>
public class AnimatorState : IAnimatorState
{
	public Animator.Animation playingAnim { get; set; }

	private Dictionary<string, int> values = new Dictionary<string, int>();

	public AnimatorState(Dictionary<string, int> values)
	{
		this.values = values;
	}

	public int Get(string name, int fallback = 0)
	{
		if (values.TryGetValue(name, out int value)) { return value; }
		return fallback;
	}

	public bool GetBool(string name)
	{
		if (values.TryGetValue(name, out int value)) { return value == 1; }
		return false;
	}

	public void Set(string name, int value)
	{
		if (values.ContainsKey(name)) 
		{
			values[name] = value;
		}
	}

	public void SetBool(string name, bool value)
	{
		if (values.ContainsKey(name))
		{
			values[name] = value ? 1 : 0;
		}
	}

	public IEnumerator<KeyValuePair<string, int>> GetEnumerator()
	{
		return values.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
