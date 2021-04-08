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
    public class GeometryPolygon : GeometryFigure
    {
        protected List<GeometryLine> _lines;
        public GeometryPolygon(Texture2D tex, Vector2 center, int radius, int number) : base(tex)
        {
            _lines = new List<GeometryLine>();

            float delta = (float)((2 * Math.PI) / number);
            float alpha = +(float)(Math.PI / 2);

            int x = (int)(center.X + radius * Math.Cos(alpha));
            int y = (int)(center.Y - radius * Math.Sin(alpha));

            Vector2 first = new Vector2(x, y);
            for (int i = 0; i < number; i++)
            {
                alpha += delta;

                Vector2 second = new Vector2(x, y);
                var line = new GeometryLine(tex, first, second);
                _lines.Add(line);
                first = second;

            }
        }

        public override void Draw(SpriteBatch batch)
        {
            foreach (var line in _lines)
            {
                line.Draw(batch);
            }
        }
    }
}
