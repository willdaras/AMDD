using AMDD.Animation.Comparison;

namespace AMDD.Animation.Nodes;

/// <summary>
/// A control node that switches between two branches based on a condition.
/// </summary>
public class SwitchNode : Node
{
	/// <summary>
	/// The node to trigger if the condition is true.
	/// </summary>
	public Node success;
	/// <summary>
	/// The node to trigger if the condition is false.
	/// </summary>
	public Node failure;

	/// <summary>
	/// The name of the animator variable to check.
	/// </summary>
	public string value;
	/// <summary>
	/// The value to compare the animator variable to.
	/// </summary>
	public int compareValue;
	/// <summary>
	/// The Comparer to compare the values with.
	/// </summary>
	public IComparer comparer;

	public SwitchNode(Node success, Node failure, string value, IComparer comparer, int compareValue)
	{
		this.success = success;
		this.failure = failure;
		this.value = value;
		this.compareValue = compareValue;
		this.comparer = comparer;
	}

	/// <summary>
	/// Resolves the node, evaluating the success Node if the comparison evaluates to true, else evaluating the failure Node.
	/// </summary>
	/// <param name="state"> The current state of the animator. </param>
	/// <returns> The TerminationNode at the end of the branch. </returns>
	public override TerminationNode Resolve(IAnimatorState state)
	{
		if (comparer.Compare(state.Get(value), compareValue))
		{
			return success.Resolve(state);
		}
		return failure.Resolve(state);
	}
}
