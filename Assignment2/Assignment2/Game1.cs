using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Assignment2
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		private Texture2D playerTexture, balls;
		private float _angle;
		private Vector2 playerPosition;
		int playerHeight;
		int playerWidth;
		private Vector2 playerDirection;
		private Vector2 ballPosition;

		private Random _rand = new Random();
		private int _frame = 0;
		private int _iter = 0;
		private int ballNo = 0;
		int ballSize = 60;
		int state = 0;
		SpriteEffects facing;
		int movementSpeed = 40;

		List<Sprite> sprites;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			_angle = 0.0f;
			playerPosition = new Vector2(68, 50);
			playerDirection = new Vector2(3, 3);
			playerHeight = 200;
			playerWidth = 200;

			ballPosition.X = _rand.Next(GraphicsDevice.Viewport.Width);
			ballPosition.Y = _rand.Next(GraphicsDevice.Viewport.Height);

			sprites=new List<Sprite>();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			sprites.Add(new Player(spriteBatch, Content));
			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			KeyboardState keys = Keyboard.GetState(); // Q to Quit 
			if (keys.IsKeyDown(Keys.Q)) this.Exit();

			// #4 Advance each of the sprites 
			foreach (Sprite s in sprites)
			{
				s.advance();
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);

			// #5 Draw each sprite 
			spriteBatch.Begin();
			foreach (Sprite s in sprites)
			{
				s.draw();
			}
			spriteBatch.End();

			base.Draw(gameTime); 

		}
		private Boolean CheckForCollision()
		{
			if (playerPosition.X > ballPosition.X && playerPosition.X<ballPosition.X+ballSize && playerPosition.Y >ballPosition.Y && playerPosition.Y<ballPosition.Y+ballSize)
				return true;

			else return false;
		}
	}
}