using AMDD.ECS;
using AMDD.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AMDD
{
	public class AMDDGame : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private RenderTarget2D _renderTarget;

		public AMDDGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_renderTarget = new RenderTarget2D(GraphicsDevice, GameData.screenX, GameData.screenY);

			_graphics.PreferredBackBufferWidth = (int)(GameData.screenX * GameData.upscaleScale);
			_graphics.PreferredBackBufferHeight = (int)(GameData.screenY * GameData.upscaleScale);

			_graphics.ApplyChanges();

			base.Initialize();
		}

		public void SetGlobalData()
		{
			GameData.spriteBatch = _spriteBatch;
			GameData.contentManager = Content;
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			SetGlobalData();

			//_scene = new ObjectCreation.Scenes.BaseSceneConstructor().ConstructScene();
			//_scene.preDrawSystems.Add(new CamFollowPlayerSystem());
			Scene scene = new LevelEditor.LevelEditorSceneConstructor().ConstructScene();

			Entity ground = new SceneEntity();
			ground.GetComponent<Position>().position = new Vector2(0, 64);

			ground.AddComponent<Static>(); ground.AddComponent(new Collider() { collider = new Rectangle(0, 120, 400, 100) });
			scene.entityMap.AddNewEntity(ground);

			Scene.SetActiveScene(scene);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();
			if (Keyboard.GetState().IsKeyDown(Keys.O))
				Scene.SetActiveScene(new ObjectCreation.Scenes.GameSceneConstructor().ConstructScene());

			// TODO: Add your update logic here
			Scene.activeScene.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.SetRenderTarget(_renderTarget);
			_graphics.ApplyChanges();
			GraphicsDevice.Clear(new Color(17, 20, 23));
			Scene.activeScene.Draw(gameTime);
			GraphicsDevice.SetRenderTarget(null);
			_graphics.ApplyChanges();
			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			_spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, 720, 480), Color.White);
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}