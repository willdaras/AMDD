using AMDD.Animation.Animator;
using AMDD.Animation.Nodes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AMDD.Animation;

/// <summary>
/// A constructor for the Player's AnimationTree.
/// </summary>
public static class GetPlayerTree
{
	/// <summary>
	/// Constructs the player tree.
	/// </summary>
	/// <param name="contentManager"> The content manager to access files with. </param>
	/// <returns> The constructed Player AnimationTree. </returns>
	public static AnimatorTree ConstructPlayerTree(ContentManager contentManager)
	{
		PlayerTextures playerTextures = new PlayerTextures(contentManager);

		AnimatorTree tree = new AnimatorTree();

		//Animator.Animation idleAnimationR = new DirectionalAnimation(new List<List<IAnimationFrame>>() { new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleTextureR) }, new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagUpTextureR) },
		//															 new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagDownTextureR) }, new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleUpTextureR) } }, "shootAngle");
		//Animator.Animation idleAnimationL = new DirectionalAnimation(new List<List<IAnimationFrame>>() { new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleTextureL) }, new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagUpTextureL) },
		//															 new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagDownTextureL) }, new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleUpTextureL) } }, "shootAngle");

		Animator.Animation idleDiagDownAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagDownTextureR) });
		Animator.Animation idleDiagUpAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagUpTextureR, new Microsoft.Xna.Framework.Vector2(0, -1)) });
		Animator.Animation idleUpAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleUpTextureR, new Microsoft.Xna.Framework.Vector2(0, -5)) });
		Animator.Animation idleAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleTextureR) });

		Animator.Animation idleDiagDownAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagDownTextureL) });
		Animator.Animation idleDiagUpAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleDiagUpTextureL) });
		Animator.Animation idleUpAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleUpTextureL, new Microsoft.Xna.Framework.Vector2(0, -5)) });
		Animator.Animation idleAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleTextureL) });

		Animator.Animation fallingAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingTextureR) });
		Animator.Animation fallingDiagUpAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingDiagUpTextureR) });
		Animator.Animation fallingDiagDownAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingDiagDownTextureR) });
		Animator.Animation fallingUpAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingUpTextureR, new Microsoft.Xna.Framework.Vector2(0, -5)) });

		Animator.Animation fallingAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingTextureL) });
		Animator.Animation fallingDiagUpAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingDiagUpTextureL) });
		Animator.Animation fallingDiagDownAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingDiagDownTextureL) });
		Animator.Animation fallingUpAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingUpTextureL, new Microsoft.Xna.Framework.Vector2(0, -5)) });

		Animator.Animation runAnimationR = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/right/player_run/player_run_right", contentManager));
		Animator.Animation runAnimationL = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/left/player_run/player_run_left", contentManager));
		Animator.Animation runRaisedAnimationR = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/right/player_run_raised_right/player_run_raised_right", contentManager));
		Animator.Animation runRaisedAnimationL = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/left/player_run_raised_left/player_run_raised_left", contentManager));
		Animator.Animation runDiagUpRaisedAnimationR = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/right/player_run_raised_up_diagonal/player_run_raised_up_diagonal_right", contentManager));
		Animator.Animation runDiagUpRaisedAnimationL = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/left/player_run_raised_up_diagonal/player_run_raised_up_diagonal_left", contentManager));
		Animator.Animation runDiagDownRaisedAnimationR = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/right/player_run_raised_down_diagonal/player_run_raised_down_diagonal_right", contentManager));
		Animator.Animation runDiagDownRaisedAnimationL = new BasicAnimation(PlayerTextures.GetAnim("Sprites/Player/left/player_run_raised_down_diagonal/player_run_raised_down_diagonal_left", contentManager));

		//Animator.Animation runAnimR = new BasicAnimation(runRaisedAnimationR); // new DirectionalAnimation(new List<List<IAnimationFrame>> { runRaisedAnimationR, runDiagUpRaisedAnimationR, runDiagDownRaisedAnimationR, runRaisedAnimationR, runRaisedAnimationR }, "shootAngle");
		//Animator.Animation runAnimL = new BasicAnimation(runRaisedAnimationL); // new DirectionalAnimation(new List<List<IAnimationFrame>> { runRaisedAnimationL, runDiagUpRaisedAnimationL, runDiagDownRaisedAnimationL, runRaisedAnimationL, runRaisedAnimationL }, "shootAngle");

		Node aimDownRunNode = new DirectionalAnimationNode(runDiagDownRaisedAnimationL, runDiagDownRaisedAnimationR);
		Node aimUpRunNode = new DirectionalAnimationNode(runDiagUpRaisedAnimationL, runDiagUpRaisedAnimationR);
		Node aimStraightRunNode = new DirectionalAnimationNode(runRaisedAnimationL, runRaisedAnimationR);

		Node aimDownIdleNode = new DirectionalAnimationNode(idleDiagDownAnimationL, idleDiagDownAnimationR);
		Node aimUpIdleNode = new DirectionalAnimationNode(idleUpAnimationL, idleUpAnimationR);
		Node aimDiagUpIdleNode = new DirectionalAnimationNode(idleDiagUpAnimationL, idleDiagUpAnimationR);
		Node aimStraightIdleNode = new DirectionalAnimationNode(idleAnimationL, idleAnimationR);

		Node aimStraightFallNode = new DirectionalAnimationNode(fallingAnimationL, fallingAnimationR);
		Node aimDiagUpFallNode = new DirectionalAnimationNode(fallingDiagUpAnimationL, fallingDiagUpAnimationR);
		Node aimDiagDownFallNode = new DirectionalAnimationNode(fallingDiagDownAnimationL, fallingDiagDownAnimationR);
		Node aimUpFallNode = new DirectionalAnimationNode(fallingUpAnimationL, fallingUpAnimationR);

		Node aimStraightSwitchRunNode = new SwitchNode(aimUpRunNode, aimStraightRunNode, "shootAngle", new Comparison.EqualTo(), 1);
		Node aimUpSwitchRunNode = new SwitchNode(aimUpRunNode, aimStraightSwitchRunNode, "shootAngle", new Comparison.EqualTo(), 3);
		Node aimDownSwitchRunNode = new SwitchNode(aimDownRunNode, aimUpSwitchRunNode, "shootAngle", new Comparison.EqualTo(), 2);

		Node aimUpSwitchIdleNode = new SwitchNode(aimUpIdleNode, aimStraightIdleNode, "shootAngle", new Comparison.EqualTo(), 3);
		Node aimDiagUpSwitchIdleNode = new SwitchNode(aimDiagUpIdleNode, aimUpSwitchIdleNode, "shootAngle", new Comparison.EqualTo(), 1);
		Node aimDownSwitchIdleNode = new SwitchNode(aimDownIdleNode, aimDiagUpSwitchIdleNode, "shootAngle", new Comparison.EqualTo(), 2);

		Node aimUpSwitchFallNode = new SwitchNode(aimUpFallNode, aimStraightFallNode, "shootAngle", new Comparison.EqualTo(), 3);
		Node aimDiagUpSwitchFallNode = new SwitchNode(aimDiagUpFallNode, aimUpSwitchFallNode, "shootAngle", new Comparison.EqualTo(), 1);
		Node aimDownSwitchFallNode = new SwitchNode(aimDiagDownFallNode, aimDiagUpSwitchFallNode, "shootAngle", new Comparison.EqualTo(), 2);

		Node runNode = new DirectionalAnimationNode(runAnimationL, runAnimationR);

		Node shootingSwitch = new SwitchNode(aimDownSwitchRunNode, runNode, "shooting", new Comparison.EqualTo(), 1);
		Node runSwitch = new SwitchNode(shootingSwitch, aimDownSwitchIdleNode, "xVel", new Comparison.NotEqual(), 0);
		Node groundedSwitch = new SwitchNode(runSwitch, aimDownSwitchFallNode, "grounded", new Comparison.EqualTo(), 1);

		tree.currentState = new AnimatorState(new Dictionary<string, int>() { { "xVel", 0 }, { "grounded", 0 }, { "facingRight", 1 }, { "shooting", 0 }, { "shootAngle", 0 } } ) { playingAnim = idleAnimationL };
		tree.treeRoot = groundedSwitch;
		return tree;
	}

	private class PlayerTextures
	{
		public List<Texture2D> runTexturesR;
		public List<Texture2D> runTexturesL;

		public Texture2D idleTextureR;
		public Texture2D idleDiagUpTextureR;
		public Texture2D idleDiagDownTextureR;
		public Texture2D idleUpTextureR;

		public Texture2D idleTextureL;
		public Texture2D idleDiagUpTextureL;
		public Texture2D idleDiagDownTextureL;
		public Texture2D idleUpTextureL;

		public Texture2D fallingTextureR;
		public Texture2D fallingDiagUpTextureR;
		public Texture2D fallingDiagDownTextureR;
		public Texture2D fallingUpTextureR;

		public Texture2D fallingTextureL;
		public Texture2D fallingDiagUpTextureL;
		public Texture2D fallingDiagDownTextureL;
		public Texture2D fallingUpTextureL;

		public PlayerTextures(ContentManager manager)
		{
			runTexturesR = new List<Texture2D>
			{
				manager.Load<Texture2D>("Sprites/Player/right/player_run/player_run_right1"),
				manager.Load<Texture2D>("Sprites/Player/right/player_run/player_run_right2"),
				manager.Load<Texture2D>("Sprites/Player/right/player_run/player_run_right3"),
				manager.Load<Texture2D>("Sprites/Player/right/player_run/player_run_right4"),
				manager.Load<Texture2D>("Sprites/Player/right/player_run/player_run_right5"),
				manager.Load<Texture2D>("Sprites/Player/right/player_run/player_run_right6")
			};
			runTexturesL = new List<Texture2D>
			{
				manager.Load<Texture2D>("Sprites/Player/left/player_run/player_run_left1"),
				manager.Load<Texture2D>("Sprites/Player/left/player_run/player_run_left2"),
				manager.Load<Texture2D>("Sprites/Player/left/player_run/player_run_left3"),
				manager.Load<Texture2D>("Sprites/Player/left/player_run/player_run_left4"),
				manager.Load<Texture2D>("Sprites/Player/left/player_run/player_run_left5"),
				manager.Load<Texture2D>("Sprites/Player/left/player_run/player_run_left6")
			};

			idleTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_raised_right");
			idleDiagUpTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_raised_up_diagonal_right");
			idleDiagDownTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_raised_down_diagonal_right");
			idleUpTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_raised_up_right");

			idleTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_raised_left");
			idleDiagUpTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_raised_up_diagonal_left");
			idleDiagDownTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_raised_down_diagonal_left");
			idleUpTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_raised_up_left");

			fallingTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_fall/player_fall_right");
			fallingDiagUpTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_fall/player_fall_up_diagonal_right");
			fallingDiagDownTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_fall/player_fall_down_diagonal_right");
			fallingUpTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_fall/player_fall_up_right");

			fallingTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_fall/player_fall_left");
			fallingDiagUpTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_fall/player_fall_up_diagonal_left");
			fallingDiagDownTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_fall/player_fall_down_diagonal_left");
			fallingUpTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_fall/player_fall_up_left");
		}

		public static List<IAnimationFrame> GetAnim(string animPath, ContentManager manager)
		{
			List<IAnimationFrame> frames = new List<IAnimationFrame>();
			for (int i = 1; i < 7; i++)
			{
				frames.Add(new BasicAnimationFrame(manager.Load<Texture2D>(animPath + i.ToString())));
			}
			return frames;
		}
	}
}