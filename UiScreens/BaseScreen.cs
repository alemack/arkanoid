using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DemoGameScreen.UiScreens
{
    abstract public class BaseScreen
    {
        protected Game _game;
        protected SpriteBatch _batch;

        virtual public void Activate() { }
        abstract public void LoadContent();
        abstract public void Draw();
        abstract public void Update(GameTime time);
    }
}
