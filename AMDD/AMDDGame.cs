using AMDD.Animation;
using AMDD.ECS;
using AMDD.ECS.Components;
using AMDD.ECS.Systems;
using AMDD.Rendering;
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

		private Scene _scene;

		#region Sprites
		private Texture2D _defaultPlayerTexture;
		#endregion

		public AMDDGame()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			_renderTarget = new RenderTarget2D(GraphicsDevice, 240, 160);

			_graphics.PreferredBackBufferWidth = 720;
			_graphics.PreferredBackBufferHeight = 480;
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

			_defaultPlayerTexture = Content.Load<Texture2D>("Sprites/Player/right/player_run/player_run_right1");

			_scene = new Scene();

			SetGlobalData();

			// TODO: use this.Content to load your game content here

			_scene.systems.Add(new InstantiationSystem());
			_scene.systems.Add(new InputSystem());
			_scene.systems.Add(new GravitySystem());
			_scene.systems.Add(new PlayerControllerSystem());
			_scene.systems.Add(new PlayerAnimationSystem());
			_scene.systems.Add(new BulletSystem());
			_scene.systems.Add(new AnimationSystem());
			_scene.systems.Add(new HealthSystem());
			_scene.systems.Add(new DestroyOnHealthZeroSystem());
			_scene.preDrawSystems.Add(new CamFollowPlayerSystem());

			Rendering.Camera camera = new BasicCamera(240, 160);
			camera.SetActive();
			_scene.entityMap.AddNewEntity(camera);

			// TODO this is disgusting, FIX
			Entity entity = new SceneEntity();
			entity.GetComponent<Name>().name = "player";
			entity.GetComponent<Position>().position = new Vector2(70, 0);
			entity.AddComponent<Player>();
			entity.AddComponent<FacingDirection>();
			entity.AddComponent(new Sprite() { image = _defaultPlayerTexture}); entity.AddComponent<Input>(); entity.AddComponent(new Physics() { dragScale=2f, xDragScale=4f });
			entity.AddComponent(new Shooting() { bulletPool = new ObjectPooling.ObjectPool(new ObjectPooling.BulletConstructor(), 20) });
			entity.AddComponent(new Gravity() { gravityScale = 5f }); entity.AddComponent(new Collider() { collider = new Rectangle(0, 0, 14, 31), offset = new Vector2(3, 0), continuous = true });
			entity.AddComponent(new Animated() { tree = GetPlayerTree.ConstructPlayerTree(Content) });
			entity.AddComponent<Grounded>();
			_scene.entityMap.AddNewEntity(entity);

			Entity ground = new SceneEntity();
			ground.GetComponent<Position>().position = new Vector2(0, 120);
			ground.AddComponent(new Sprite() { image = Content.Load<Texture2D>("Sprites/Tiles/backgrounds/artaria_first_background") });
			ground.AddComponent<Static>(); ground.AddComponent(new Collider() { collider = new Rectangle(0, 120, 400, 100) });
			_scene.entityMap.AddNewEntity(ground);

			Entity wall = new SceneEntity();
			wall.GetComponent<Position>().position = new Vector2(70, 80);
			wall.AddComponent(new Sprite() { image = Content.Load<Texture2D>("Sprites/Tiles/backgrounds/artaria_first_background") });
			wall.AddComponent<Static>(); wall.AddComponent(new Collider() { collider = new Rectangle(0, 120, 400, 100) });
			wall.AddComponent<Health>(); wall.AddComponent<DamageBuffer>();
			_scene.entityMap.AddNewEntity(wall);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// TODO: Add your update logic here
			_scene.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.SetRenderTarget(_renderTarget);
			GraphicsDevice.Clear(Color.CornflowerBlue);
			_scene.Draw(gameTime);
			GraphicsDevice.SetRenderTarget(null);
			_spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			_spriteBatch.Draw(_renderTarget, new Rectangle(0, 0, 720, 480), Color.White);
			_spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}