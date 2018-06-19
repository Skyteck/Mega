using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mega
{
    class Player : Sprite
    {

        float _Gravity = 5f;
        float _Friction = 5f;
        Vector2 _Momentum = Vector2.Zero;

        public virtual Rectangle _BottomRect
        {
            get
            {
                return new Rectangle((int)this._Position.X, this._BoundingBox.Bottom, 1, 1);
            }
        }
        public void UpdateActive(GameTime gameTime, List<Block> bList)
        {
            base.UpdateActive(gameTime);


            if(InputHelper.IsKeyPressed(Keys.Space))
            {
                _Momentum.Y -= 4.0f;
            }

            if(InputHelper.IsKeyDown(Keys.D))
            {
                _Momentum.X = 3f;
            }
            else if(InputHelper.IsKeyDown(Keys.A))
            {
                _Momentum.X = -3f;
            }

            this._Position += _Momentum;



            _Momentum.Y += _Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if(_Momentum.X > 0f)
            {
                _Momentum.X -= _Friction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_Momentum.X < 0f)
                {
                    _Momentum.X = 0f;
                }
            }
            else if(_Momentum.X < 0f)
            {
                _Momentum.X += _Friction * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(_Momentum.X > 0f)
                {
                    _Momentum.X = 0f;
                }
            }
            
            foreach(Block b in bList)
            {
                if(this._BottomRect.Intersects(b._BoundingBox))
                {
                    Console.WriteLine(b.Name);
                    this._Position.Y = b._BoundingBox.Top - this.frameHeight/2;
                    _Momentum.Y = 0f;
                }
            }

        }
    }
}
