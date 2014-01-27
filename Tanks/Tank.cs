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
using System.Timers;

namespace Tanks
{
    class Tank
    {
        #region shell count
        int normalShells = 100;
        #endregion
        Rectangle rectangle;
        Texture2D texture;
        SpriteBatch spriteBatch; //spritebatch to handle drawing
        public List<Shell> shells; //list to hold fired shells
        public Dictionary<string, Texture2D> shellTextures;
        MouseState oldMouse;
        int power;
        int range;
        const int GRAVITY = -10;
        public Tank(Texture2D t, Rectangle r, SpriteBatch sb)
        {
            rectangle = r;
            texture = t;
            spriteBatch = sb;
            power = 500;
            shells = new List<Shell>();
        }
        public void Update()
        {
            Move();
            Shoot();
        }
        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();
        }
        void Move()
        {
            int moveX = 5;
            int moveY = 5;
            KeyboardState keys = Keyboard.GetState();
            if (keys.IsKeyDown(Keys.A)) rectangle.X -= moveX;
            if (keys.IsKeyDown(Keys.D)) rectangle.X += moveX;
            if (keys.IsKeyDown(Keys.W)) rectangle.Y -= moveY;
            if (keys.IsKeyDown(Keys.S)) rectangle.Y += moveY;
            if (rectangle.X <= 0) rectangle.X += 5;
            if (rectangle.X >= (1280 - rectangle.Width)) rectangle.X -= 5;
            if (rectangle.Y <= 450) rectangle.Y += 5;
            if (rectangle.Y >= (720 - rectangle.Width)) rectangle.Y -= 5;
        }
        void Shoot()
        {
            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released && mouse.X > rectangle.X + rectangle.Width)//limit to just shooting forward
            {
                //Insert something here to check what bullet is in the inventory
                Shell_Normal shell = new Shell_Normal(new Rectangle(rectangle.X + rectangle.Width, rectangle.Y + 40, 10, 10), shellTextures["Shell_Normal"], spriteBatch, power - 2, mouse.X, mouse.Y, power - 2);
                shell.explodeTextures = g.explodeTextures;
                shells.Add(shell);
            }
            oldMouse = mouse;
            foreach (Shell s in shells)
            {
                MoveProjectile(s);
            }
        }
        void MoveProjectile(Shell shell)
        {
            if (shell.exploding == false && shell.draw)
            {
                int slopeY = shell.initialMouseY - shell.initialY;
                int slopeX = shell.initialMouseX - shell.initialX;
                int reduceFactor = 20;
                double distance = Math.Sqrt((slopeY * slopeY) + (slopeX * slopeX));
                if (distance <= 10)
                {
                    reduceFactor = 1;
                }
                slopeY /= reduceFactor;
                slopeX /= reduceFactor;

                shell.rectangle.X += slopeX;
                shell.rectangle.Y += slopeY;
            }
        }
        public Rectangle GetPlayerRec()
        {
            return rectangle;

        }
        public Texture2D GetPlayerTex()
        {
            return texture;
        }
        public List<Shell> GetPlayerShells()
        {
            return shells;
        }
    }
}
