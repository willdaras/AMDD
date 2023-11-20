using AMDD.Animation.Animator;
using AMDD.Animation.Nodes;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AMDD.Animation;

public static class GetPlayerTree
{
	public static AnimatorTree ConstructPlayerTree(ContentManager contentManager)
	{
		PlayerTextures playerTextures = new PlayerTextures(contentManager);

		AnimatorTree tree = new AnimatorTree();

		Animator.Animation idleAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleTextureR) });
		Animator.Animation idleAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.idleTextureL) });
		Animator.Animation fallingAnimationR = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingTextureR) });
		Animator.Animation fallingAnimationL = new BasicAnimation(new List<IAnimationFrame>() { new BasicAnimationFrame(playerTextures.fallingTextureL) });
		Animator.Animation runAnimationR = new BasicAnimation(new List<IAnimationFrame>() 
										{ new BasicAnimationFrame(playerTextures.runTexturesR[0]), new BasicAnimationFrame(playerTextures.runTexturesR[1]),
										  new BasicAnimationFrame(playerTextures.runTexturesR[2]), new BasicAnimationFrame(playerTextures.runTexturesR[3]),
										  new BasicAnimationFrame(playerTextures.runTexturesR[4]), new BasicAnimationFrame(playerTextures.runTexturesR[5]), });
		Animator.Animation runAnimationL = new BasicAnimation(new List<IAnimationFrame>()
										{ new BasicAnimationFrame(playerTextures.runTexturesL[0]), new BasicAnimationFrame(playerTextures.runTexturesL[1]),
										  new BasicAnimationFrame(playerTextures.runTexturesL[2]), new BasicAnimationFrame(playerTextures.runTexturesL[3]),
										  new BasicAnimationFrame(playerTextures.runTexturesL[4]), new BasicAnimationFrame(playerTextures.runTexturesL[5]), });

		Node runNode = new DirectionalAnimationNode(runAnimationL, runAnimationR);
		Node idleNode = new DirectionalAnimationNode(idleAnimationL, idleAnimationR);
		Node fallingNode = new DirectionalAnimationNode(fallingAnimationL, fallingAnimationR);

		Node runSwitch = new SwitchNode(runNode, idleNode, "xVel", new Comparison.NotEqual(), 0);
		Node groundedSwitch = new SwitchNode(runSwitch, fallingNode, "grounded", new Comparison.EqualTo(), 1);

		tree.currentState = new AnimatorState(new Dictionary<string, int>() { { "xVel", 0 }, { "grounded", 0 }, { "facingRight", 1 } } ) { playingAnim = idleAnimationR };
		tree.treeRoot = groundedSwitch;
		return tree;
	}

	private class PlayerTextures
	{
		public List<Texture2D> runTexturesR;
		public List<Texture2D> runTexturesL;
		public Texture2D idleTextureR;
		public Texture2D idleTextureL;
		public Texture2D fallingTextureR;
		public Texture2D fallingTextureL;

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
			idleTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_raised_left");
			fallingTextureR = manager.Load<Texture2D>("Sprites/Player/right/player_fall/player_fall_right");
			fallingTextureL = manager.Load<Texture2D>("Sprites/Player/left/player_fall/player_fall_left");
		}
	}
}