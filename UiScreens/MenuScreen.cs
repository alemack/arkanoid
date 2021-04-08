using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DemoGameScreen.UiControls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DemoGameScreen.UiScreens
{
    public delegate void MenuEvent();


    public class MenuScreen : BaseScreen
    {
        public event MenuEvent EventNewGame;
        public event MenuEvent EventSaveGame;
        public event MenuEvent EventLoadGame;
        public event MenuEvent EventExitGame;

        protected TextEngine _textEngine;
        protected UIButton _btnNewGame;
        protected UIButton _btnExit;
        protected UIButton _btnSave;
        protected UIButton _btnLoad;
        protected Texture2D _backTex;
        protected UIButton[] buttons;

        public MenuScreen(Game game)
        {
            _game = game;
            _textEngine = new TextEngine(game);
            game.Components.Add(_textEngine);
        }

        public override void LoadContent()
        {
            _batch = new SpriteBatch(_game.GraphicsDevice);
            _backTex = _game.Content.Load<Texture2D>("back");
            // создаем кнопки
            _btnNewGame = new UIButton(_textEngine);
            _btnSave = new UIButton(_textEngine);
            _btnLoad = new UIButton(_textEngine);
            _btnExit = new UIButton(_textEngine);
            //----------------------------------
            buttons = new UIButton[]
                {_btnNewGame, _btnSave,
                    _btnLoad, _btnExit};
            int cx = _game.GraphicsDevice.Viewport.Width / 2;
            int i = 0;
            foreach (UIButton btn in buttons)
            {
                btn.Pos = new Vector2(cx - 100, 100 + (50 + 20) * i);
                btn.Size = new Vector2(200, 50);
                btn.Background = _backTex;
                btn.BackStyle = BackgroundStyle.Stretch;
                i++;
            }
            _btnNewGame.Text = "Новая Игра";
            _btnSave.Text = "Сохранить";
            _btnLoad.Text = "Загрузить";
            _btnExit.Text = "Выход";
            _btnExit.Click += _btnExit_Click;
            _btnNewGame.Click += _btnNewGame_Click;
            _btnSave.Click += _btnSave_Click;
            _btnLoad.Click += _btnLoad_Click;
        }

        public override void Activate()
        {
            foreach (UIButton btn in buttons)
            {
                btn.Visible = true;
            }
        }

        void HideButtons()
        {
            foreach (UIButton btn in buttons)
            {
                btn.Visible = false;
            }
        }

        void _btnLoad_Click(UIControl sender)
        {
            if (EventLoadGame != null)
            {
                HideButtons();
                EventLoadGame();
            }
        }

        void _btnSave_Click(UIControl sender)
        {
            if (EventSaveGame != null)
            {
                HideButtons();
                EventSaveGame();
            }
        }

        void _btnNewGame_Click(UIControl sender)
        {
            if (EventNewGame != null)
            {
                HideButtons();
                EventNewGame();
            }
        }

        void _btnExit_Click(UIControl sender)
        {
            if (EventExitGame != null)
            {
                HideButtons();
                EventExitGame();
            }
        }

        public override void Draw()
        {
            foreach (UIButton btn in buttons)
            {
                btn.Draw(_batch);
            }
        }

        public override void Update(GameTime time)
        {
            foreach (UIButton btn in buttons)
            {
                btn.Update();
            }
        }
    }
}
