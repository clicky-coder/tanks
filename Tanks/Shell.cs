using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tanks
{
    public abstract class Shell
    {
        public Rectangle rectangle;
        public Texture2D texture;
        public SpriteBatch spriteBatch;
        public string description;
        public string specialDescription;
        public bool draw;
        public bool exploding;
        public int initialX;
        public int initialY;
        public int initialMouseX;
        public int initialMouseY;
        public int damage;
        public int weight;
        public int MaxDistanceX; //distance to go before exploding
        public int MaxDistanceY;
        public int initialVelocity;
        public int x;
        public int counter;
        public int explodeCounter = 0;
        public List<Texture2D> explodeTextures;
        public abstract void Update();
        public  void Draw()
        {
            if (draw)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(texture, rectangle, Color.White);
                spriteBatch.End();
            }
        }
    }

    public class Shell_Normal:Shell
    {
        public Shell_Normal(Rectangle r, Texture2D t, SpriteBatch s, int iv, int _initialMouseX, int _initialMouseY, int range)
        {
            rectangle = r;
            texture = t;
            spriteBatch = s;
            damage = 5; //
            weight = 2;
            initialVelocity = iv;
            initialX = rectangle.X;
            initialY = rectangle.Y;
            x = 0;
            counter = 0;
            initialMouseX = _initialMouseX;
            initialMouseY = _initialMouseY;
            MaxDistanceX = initialX + range;
            MaxDistanceY = initialY - range;
            description = "Normal, everyday artillary shell";
            draw = true;
            exploding = false;
        }
        public override void Update()
        {
            if (rectangle.X > MaxDistanceX || rectangle.Y < MaxDistanceY) Explode();
        }
        public void Explode()
        {
            exploding = true;
            rectangle.Width = rectangle.Height = 100;
            if (explodeCounter < explodeTextures.Count)
            {
                texture = explodeTextures[explodeCounter];
                explodeCounter++;
            }
            else
            {
                draw = false;
            }
        }
        
    }
}
