using System.Collections;
using System.Collections.Generic;

namespace AMDD.Animation;

public interface IAnimatorState : IEnumerable<KeyValuePair<string, int>>
{
	Animator.Animation playingAnim { get; set; }

	int Get(string name, int fallback = 0);
	bool GetBool(string name);

	void Set(string name, int value);
	void SetBool(string name, bool value);
}