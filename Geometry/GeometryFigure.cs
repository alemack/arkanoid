using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectMonoGame01.Controllers;


namespace ProjectMonoGame01.Geometry
{
    abstract public class GeometryFigure
    {
        protected Vector2 _origin;
        protected float _angle;


        protected Color _color;

        protected Vector2 _scaleVector;

        public Vector2 Scale
        {
            get { return _scaleVector; }
            set { _scaleVector = value; }
        }


        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        private int _lineWidth;

        public int LineWith
        {
            get { return _lineWidth; }
            set { _lineWidth = value; }
        }

        // pixel = picture's element
        protected Texture2D _pixel;

        public GeometryFigure(Texture2D tex)
        {
            _pixel = tex;
            _color = Color.White;
            _lineWidth = 1;
        }



        abstract public void Draw(SpriteBatch batch);
    }
}
