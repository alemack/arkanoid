using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arkanoid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProjectMonoGame01.Controllers;

namespace DemoGameScreen.UiScreens
{
    public class GameScreen : BaseScreen
    {
        #region поля 
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        /// <summary>
        /// Границы игрового поля.
        /// </summary>
        private Rectangle _boundariesPlayingField;
        /// <summary>
        /// Фон игрового поля.
        /// </summary>
        private Texture2D _background;

        /// <summary>
        /// Игровая ракетка.
        /// </summary>
        protected GameObject _paddle;
        /// <summary>
        /// Спрайт игровогой ракетки.
        /// </summary>
        private Texture2D _paddleSprite;

        /// <summary>
        /// Кол-во кирпичей в ширину.
        /// </summary>
        private int _numberBricksWidth;
        /// <summary>
        /// Кол-ва кирпичей в высоту.
        /// </summary>
        private int _numberBricksHeight;
        /// <summary>
        /// Массив кирпичей.
        /// </summary>
        private GameObject[,] _bricks;
        /// <summary>
        /// Спрайт кирпича.
        /// </summary>
        private Texture2D _brickSprite;

        /// <summary>
        /// Мячик.
        /// </summary>
        private GameObject _ball;
        /// <summary>
        /// Спрайт мячика.
        /// </summary>
        private Texture2D _ballSprite;

        private MouseCursor cursor;
        private UIController UI;
        bool gameStarted;



       
        Random rnd = new Random();
        #endregion


        public GameScreen(Game game)
        {
            _game = game;
        }

        public override void Activate()
        {
            base.Activate();
            _bm = new BubblesManager(_game, rnd.Next(10, 20), _bubbleTex);
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("background");
            _paddleSprite = Content.Load<Texture2D>("racket");
            _brickSprite = Content.Load<Texture2D>("brick");
            _ballSprite = Content.Load<Texture2D>("ball");
        }

        public override void Update(GameTime time)
        {
            _bm.Update();
        }

        public override void Draw()
        {
            _batch.Begin();
            _bm.Draw(_batch);
            _batch.End();
        }
    }
}
