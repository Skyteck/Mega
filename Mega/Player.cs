using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Mega
{
    class Player : Sprite
    {

        float _Gravity = 300f;
        float _Friction = 5f;
        float _Speed = 300f;
        float _JumpSpeed = 600f;
        float _AirTime = 0f;
        const float _MaxAirTime = .5f;
        Vector2 _Momentum = Vector2.Zero;
        Vector2 curPos;
        Texture2D rectTex;

        public virtual Rectangle _TopRect
        {
            get
            {
                return new Rectangle((int)(this._BoundingBox.Left + 3), this._BoundingBox.Top, frameWidth - 6, 1);
            }
        }
        public virtual Rectangle _LeftRect
        {
            get
            {
                return new Rectangle((int)(this._BoundingBox.Left), this._BoundingBox.Top, 3, frameHeight - 3 );
            }
        }

        public virtual Rectangle _RightRect
        {
            get
            {
                return new Rectangle((int)(this._BoundingBox.Right-3), this._BoundingBox.Top, 3, frameHeight - 3);
            }
        }


        public virtual Rectangle _BottomRect
        {
            get
            {
                return new Rectangle((int)(this._BoundingBox.Left), this._BoundingBox.Bottom, frameWidth, 1);
            }
        }

        public override void LoadContent(string path, ContentManager content)
        {
            base.LoadContent(path, content);
            rectTex = content.Load<Texture2D>(@"Art/edgeTex");
        }

        public void UpdateActive(GameTime gameTime, List<Rectangle> bList)
        {
            base.UpdateActive(gameTime);
            curPos = this._Position;

            if(InputHelper.IsKeyDown(Keys.Space) && _AirTime < _MaxAirTime)
            {
                this._Position.Y -= (int)(_JumpSpeed * gameTime.ElapsedGameTime.TotalSeconds);
                _AirTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(InputHelper.IsKeyDown(Keys.D))
            {
                this._Position.X += (int)(_Speed * gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if(InputHelper.IsKeyDown(Keys.A))
            {
                this._Position.X -= (int)(_Speed * gameTime.ElapsedGameTime.TotalSeconds);
            }

            //this._Position += _Momentum;



            this._Position.Y += (_Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            
            //if(_Momentum.X > 0f)
            //{
            //    _Momentum.X -= _Friction * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //    if (_Momentum.X < 0f)
            //    {
            //        _Momentum.X = 0f;
            //    }
            //}
            //else if(_Momentum.X < 0f)
            //{
            //    _Momentum.X += _Friction * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //    if(_Momentum.X > 0f)
            //    {
            //        _Momentum.X = 0f;
            //    }
            //}
            
            foreach(Rectangle b in bList)
            {
                if(this._RightRect.Intersects(b) || this._LeftRect.Intersects(b))
                {
                    //_Momentum.X = 0f;
                    this._Position.X = curPos.X;
                }

                if(this._BottomRect.Intersects(b))
                {
                    this._Position.Y = b.Top - this.frameHeight/2;
                    this._Position.Y = curPos.Y;
                    _AirTime = 0f;
                    //_Momentum.Y = 0f;
                }
                else if(this._TopRect.Intersects(b))
                {
                    //_Momentum.Y = 0f;
                    this._Position.Y = curPos.Y;

                }
            }

            if(this._Position.Y > 500)
            {
                //_Momentum.Y = 0f;
                this._Position.Y = 500;
                this._Position.Y = curPos.Y;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.Draw(rectTex, _TopRect, Color.White);
            spriteBatch.Draw(rectTex, _BottomRect, Color.White);
            spriteBatch.Draw(rectTex, _RightRect, Color.White);
            spriteBatch.Draw(rectTex, _LeftRect, Color.White);
        }
    }

    
}
