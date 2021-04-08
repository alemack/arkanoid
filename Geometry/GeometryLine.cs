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
    public class GeometryLine : GeometryFigure
    {
        protected Vector2 _pos;

        public void ChangeSecondPoint(Vector2 v2)
        {
            Vector2 vector = v2 - _pos;
            float hypo = vector.Length();
            float angle = (float)Math.Atan2(vector.Y, vector.X);
            _scaleVector = new Vector2(hypo, LineWith);
            _angle = angle;
        }
        public GeometryLine(Texture2D tex, Vector2 v1, Vector2 v2, int size = 1) : base(tex)
        {
            Vector2 vector = v2 - v1;
            float hypo = vector.Length();
            float angle = (float)Math.Atan2(vector.Y, vector.X);
            _scaleVector = new Vector2(hypo, size);
            LineWith = size;
            _angle = angle;
            _pos = v1;
        }




        public override void Draw(SpriteBatch batch)
        {

            batch.Draw(_pixel, _pos, null, _color, _angle, _origin, _scaleVector, SpriteEffects.None, 1);
        }
    }
}
