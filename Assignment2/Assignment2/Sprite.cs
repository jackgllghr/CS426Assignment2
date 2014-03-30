using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace Assignment2
{
	public abstract class Sprite
	{
		public SpriteBatch spriteBatch;
		public Texture2D texture;

		public int X, Y, dirX, dirY, Width, Height;
		public int movementSpeed=5;
		public Vector2 dir;

		public Sprite(SpriteBatch s, ContentManager c) {
			spriteBatch = s;


		}
		public virtual void draw(){
			spriteBatch.Draw(texture, new Rectangle(X, Y, Width, Height), Color.White);
		}
		public abstract void advance();
	}
	public class Player : Sprite
	{
		public int frame=0;
		public int frameCount;
		public int iterator;

		public Player(SpriteBatch inBatch, ContentManager inContent)
			: base(inBatch, inContent)
		{
			texture = inContent.Load<Texture2D>("Alucard");
			X = 100;
			Y = 100;
			Width = 56; 
			Height=128;
			frameCount = 3;
			iterator = 0;
		}
		public override void draw() {
			if (iterator > 5)
			{
				frame++;
				iterator = 0;
			}
			if (frame > frameCount) frame = 0;

			iterator++;
			spriteBatch.Draw(texture, new Rectangle(X, Y, Width, Height), new Rectangle(frame * 28, 0, 28, 64), Color.White);


		}
		public override void advance()
		{
			KeyboardState keys = Keyboard.GetState(); 
			GamePadState pad=GamePad.GetState(PlayerIndex.One);
			if ((Y+Height) < spriteBatch.GraphicsDevice.Viewport.Height && keys.IsKeyDown(Keys.Down) || pad.IsButtonDown(Buttons.DPadDown)) {
				Y = Y + movementSpeed; 
			}
			if (Y > 0 && keys.IsKeyDown(Keys.Up) || pad.IsButtonDown(Buttons.DPadUp))  {
				Y = Y - movementSpeed; 
			}
			if (X > 0 && keys.IsKeyDown(Keys.Left) || pad.IsButtonDown(Buttons.DPadLeft)) { 
				X = X - movementSpeed; 
			}
			if ((X+Width)<spriteBatch.GraphicsDevice.Viewport.Width && keys.IsKeyDown(Keys.Right) || pad.IsButtonDown(Buttons.DPadRight)){
				X = X + movementSpeed;
			}
		}
	}
}
