using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMonoGame01.Geometry
{
    public class GeometryPoint : GeometryFigure
    {
        protected Vector2 _pos;

        public GeometryPoint(Texture2D tex, int x, int y, int size = 1) : base(tex)
        {
            _pos = new Vector2(x, y);
            _angle = 0;
            _origin = new Vector2(0, 0);
            _scaleVector = new Vector2(size, size);
        }

        public GeometryPoint(Texture2D tex, int x, int y, int width = 1, int hight = 1) : base(tex)
        {
            _pos = new Vector2(x, y);
            _angle = 0;
            _origin = new Vector2(0, 0);
            _scaleVector = new Vector2(width, hight);
        }

        public override void Draw(SpriteBatch batch)
        {

            batch.Draw(_pixel, _pos, null, _color, _angle, _origin, _scaleVector, SpriteEffects.None, 1);
        }
    }
}
