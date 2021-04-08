using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMonoGame01.Controllers
{
    public class MouseController
        : BaseController
    {
        public MouseController(Game game)
            : base(game)
        {

        }

        private bool _moving;
        private Vector2 _destination;

        public void MoveToPoint(Vector2 pt)
        {
            _destination = pt;
            _moving = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (_driven == null) return;

            MouseState ms = Mouse.GetState();
            var mousePosition = new Vector2(ms.X, ms.Y);

            // вектор прицеливания
            Vector2 scope = mousePosition - _driven.Position;
            // угол этого вектора
            double alpha = Math.Atan2(scope.Y, scope.X);

            _driven.Direction = (float)alpha;

            if (_moving)
            {
                Vector2 delta = scope / 20;
                _driven.Move(delta);
                if (delta.Length() < 2)
                {
                    _moving = false;
                }
            }

            base.Update(gameTime);
        }
    }
}
