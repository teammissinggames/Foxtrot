using System;
using System.Linq;
using System.Collections.Generic;
using Foxtrot.Managers;
using Foxtrot.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Foxtrot.Sprites
{
    public class Sprite
    {
        #region Fields

        protected Vector2 _position;

        protected Texture2D _texture;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;

            }
        }

        #endregion

        #region Method

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_texture != null)
                spriteBatch.Draw(_texture, Position, Color.White);
            else throw new Exception("Error: No texture found.");
        }

        public Sprite(Texture2D texture)
        {
            _texture = texture;
        }

        #endregion
    }
}
