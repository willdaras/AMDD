using AMDD.Animation.Comparison;

namespace AMDD.Animation.Nodes;

public class SwitchNode : Node
{
	public Node success;
	public Node failure;

	public string value;
	public int successValue;
	public IComparer comparer;

	public SwitchNode(Node success, Node failure, string value, IComparer comparer, int successValue)
	{
		this.success = success;
		this.failure = failure;
		this.value = value;
		this.successValue = successValue;
		this.comparer = comparer;
	}

	public override TerminationNode Resolve(IAnimatorState state)
	{
		if (comparer.Compare(state.Get(value), successValue))
		{
			return success.Resolve(state);
		}
		return failure.Resolve(state);
	}
}
