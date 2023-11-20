using System.Collections;
using System.Collections.Generic;

namespace AMDD.Animation;

/// <summary>
/// Represents a state the animator can be in.
/// </summary>
public interface IAnimatorState : IEnumerable<KeyValuePair<string, int>>
{
	/// <summary>
	/// The animation currently being played.
	/// </summary>
	Animator.Animation playingAnim { get; set; }

	/// <summary>
	/// Get the value of an animator variable.
	/// </summary>
	/// <param name="name"> The name of the variable. </param>
	/// <param name="fallback"> The default value if the variable isn't found. </param>
	/// <returns> The value of the variable if found, otherwise the fallback. </returns>
	int Get(string name, int fallback = 0);
	/// <summary>
	/// Get the value of an animator variable as a bool.
	/// </summary>
	/// <param name="name"> The name of the variable. </param>
	/// <returns> The value of the variable as a bool if found, otherwise false. </returns>
	bool GetBool(string name);

	/// <summary>
	/// Sets the value of an animator variable.
	/// </summary>
	/// <param name="name"> The name of the variable to set. </param>
	/// <param name="value"> The value to set the variable to. </param>
	void Set(string name, int value);
	/// <summary>
	/// Sets the value of an animator variable as a bool.
	/// </summary>
	/// <param name="name"> The name of the variable to set. </param>
	/// <param name="value"> The value to set the variable to. </param>
	void SetBool(string name, bool value);
}