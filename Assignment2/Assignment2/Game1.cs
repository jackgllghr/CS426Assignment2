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
using Microsoft.Xna.Framework.Media;

namespace Assignment2
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		private Random rand = new Random();
		private int iter = 0;
		int state = 0;
		int score=0;

		Player player;
		List<Collectable> items;

		SpriteFont scoreFont;
		Vector2 FontPos;
		Song music;

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
			MediaPlayer.Volume = 1.0f;

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

//			music = Content.Load<Song> ("BennyHillTheme");
//			MediaPlayer.Play (music);

			items=new List<Collectable>();
			player = new Player (spriteBatch, Content);
			items.Add(new Collectable(spriteBatch, Content, rand.Next(GraphicsDevice.Viewport.Width), 0));
			// TODO: use this.Content to load your game content here

			scoreFont = Content.Load<SpriteFont> ("SpriteFont1");

			FontPos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2,
				graphics.GraphicsDevice.Viewport.Height / 10);

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

			player.advance ();
			// #4 Advance each of the sprites 
			foreach (Collectable i in items)
			{
				i.advance();
				if (i.isActive && CheckForCollision (i)) {
					score++;
					i.isActive = false;
				}
			}

			if (iter % 30==0) {
				items.Add (new Collectable(spriteBatch, Content, rand.Next(GraphicsDevice.Viewport.Width), 0));
				iter = 0;
			}
			iter++;
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.GhostWhite);

			// #5 Draw each sprite 
			spriteBatch.Begin();

			spriteBatch.DrawString (scoreFont, score.ToString(), FontPos, Color.Black);

			player.draw ();

			foreach (Collectable i in items)
			{
				if (i.isActive) {
					i.draw ();
				}
			}

			spriteBatch.End();

			base.Draw(gameTime); 

		}
		private Boolean CheckForCollision(Sprite i)
		{
			if(player.X < (i.X + (i.Width/2)) && (player.X + player.Width) > (i.X + (i.Height/2)) &&
				player.Y < (i.Y + (i.Height/2)) && (player.Y + player.Height) > (i.Y + (i.Height/2))){
				return true;
			}

			return false;
		}
	}
}