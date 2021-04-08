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
    public class ScreenManager : BaseScreen
    {
        protected List<BaseScreen> _screens;
        protected BaseScreen _activeScreen;
        protected Texture2D _pauseTex;
        protected bool IsPause;
        protected KeyboardState prevKs;


        public ScreenManager(Game game)
        {
            IsPause = false;
            _game = game;
            _screens = new List<BaseScreen>();
            GameScreen gs = new GameScreen(game);
            MenuScreen ms = new MenuScreen(game);
            _screens.Add(ms);
            _screens.Add(gs);
            _activeScreen = ms;
            //-----------------
            ms.EventExitGame += ms_EventExitGame;
            ms.EventNewGame += ms_EventNewGame;
        }

        void ms_EventNewGame()
        {
            _activeScreen = _screens[1];
            _activeScreen.Activate();
        }

        void ms_EventExitGame()
        {
            _game.Exit();
        }

        public override void LoadContent()
        {
            _batch = new SpriteBatch(_game.GraphicsDevice);
            foreach (var sc in _screens)
            {
                sc.LoadContent();
            }
            _pauseTex = _game
                .Content
                .Load<Texture2D>("pause");
        }

        public override void Draw()
        {
            _activeScreen.Draw();
            if (IsPause)
            {
                _batch.Begin();
                _batch.Draw(_pauseTex,
                    new Vector2(250, 100), Color.White);
                _batch.End();
            }
        }

        public override void Update(GameTime time)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Escape))
            {
                _activeScreen = _screens[0];
                _activeScreen.Activate();
            }
            if (ks.IsKeyDown(Keys.Space)
                &&
                prevKs.IsKeyUp(Keys.Space))
            {
                // вкл - на выкл, выкл - вкл
                if (_activeScreen ==
                    _screens[1])
                    IsPause = !IsPause;
            }
            if (!IsPause)
                _activeScreen.Update(time);
            prevKs = ks;
        }
    }
}
