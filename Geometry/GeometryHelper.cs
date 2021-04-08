using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMonoGame01.Controllers;
using Microsoft.Xna.Framework;

namespace ProjectMonoGame01.Geometry
{
    public class GeometryHelper : DrawableGameComponent
    {
        protected List<GeometryFigure> _figures;
        protected Texture2D _pixel;
        protected SpriteBatch _batch;
        public GeometryHelper(Game game) : base(game)
        {
            game.Components.Add(this);
            _figures = new List<GeometryFigure>();
        }


        protected override void LoadContent()
        {
            base.LoadContent();

            _batch = new SpriteBatch(Game.GraphicsDevice);

            _pixel = new Texture2D(Game.GraphicsDevice, 1, 1);
            Color[] colors = { Color.White };
            _pixel.SetData<Color>(colors);
        }

        public GeometryFigure AddPoint(int x, int y, int size = 1)
        {
            var pt = new GeometryPoint(_pixel, x, y, size);
            _figures.Add(pt);

            return pt;
        }

        public GeometryFigure AddPoint(int x, int y, int width = 1, int hight = 1)
        {
            var pt = new GeometryPoint(_pixel, x, y, width, hight);

            _figures.Add(pt);

            return pt;
        }

        public GeometryFigure AddPoint(Vector2 v, int size = 1)
        {
            var pt = new GeometryPoint(_pixel, (int)v.X, (int)v.Y, size);
            _figures.Add(pt);

            return pt;
        }

        public GeometryFigure AddLine(Vector2 v1, Vector2 v2, int size = 1)
        {
            var line = new GeometryLine(_pixel, v1, v2, size);
            _figures.Add(line);

            return line;
        }


        public GeometryFigure AddPolygon(Vector2 center, int radius, int number)
        {
            var polygon = new GeometryPolygon(_pixel, center, radius, number);

            _figures.Add(polygon);

            return polygon;
        }

        public override void Draw(GameTime gameTime)
        {
            _batch.Begin();
            foreach (var fig in _figures)
            {
                fig.Draw(_batch);
            }
            _batch.End();
        }
    }
}
