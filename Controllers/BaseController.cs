using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arkanoid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ProjectMonoGame01.Controllers
{
    abstract public class BaseController
        : GameComponent
    {
        protected GameObject _driven;

        public BaseController(Game game)
            : base(game)
        {
            game.Components.Add(this);
        }

        virtual public void Attach(GameObject driven)
        {
            _driven = driven;
        }
    }
}
