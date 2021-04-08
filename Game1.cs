using Arkanoid;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using ProjectMonoGame01.Controllers;
using ProjectMonoGame01.Geometry;


namespace ProjectMonoGame01
{
    public class Game1 : Game
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




        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            graphics.IsFullScreen = false;
            this.IsMouseVisible = false;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 1024;

            _numberBricksWidth = 10;
            _numberBricksHeight = 5;


        }



        protected override void Initialize()
        {
            base.Initialize();

            _paddle = new GameObject(_paddleSprite);
            _ball = new GameObject(_ballSprite);



            _boundariesPlayingField = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            _paddle.Position = new Vector2(((_boundariesPlayingField.Width - _paddle.Width) / 2), _boundariesPlayingField.Height - _paddle.Height - 20);
            _ball.Position = new Vector2((_boundariesPlayingField.Width - _ball.Width) / 2, _boundariesPlayingField.Height - _paddle.Height - _ball.Height - 20);

            _bricks = new GameObject[_numberBricksWidth, _numberBricksHeight];

            _ball.Velocity = new Vector2(3, -3);


            for (int i = 0; i < _numberBricksWidth; i++)
            {
                for (int j = 0; j < _numberBricksHeight; j++)
                {
                    _bricks[i, j] = new GameObject(_brickSprite);
                    _bricks[i, j].Position = new Vector2(i * 55 + 120, j * 25 + 100);


                }
            }

        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _background = Content.Load<Texture2D>("background");
            _paddleSprite = Content.Load<Texture2D>("racket");
            _brickSprite = Content.Load<Texture2D>("brick");
            _ballSprite = Content.Load<Texture2D>("ball");
        }

        protected override void UnloadContent()
        { }

        private void UpdateBall()
        {
            Rectangle nextRect = new Rectangle((int)(_ball.Position.X + _ball.Velocity.X), (int)(_ball.Position.Y + _ball.Velocity.Y), _ball.Width, _ball.Height);

            if (nextRect.Y <= 0)
            {
                _ball.MirrorHorizontal();
            }

            if (nextRect.Y >= _boundariesPlayingField.Height - nextRect.Height)
            {
                _ball.IsAlive = false;
            }

            if ((nextRect.X >= _boundariesPlayingField.Width - nextRect
                .Width) || nextRect.X <= 0)
            {
                _ball.MirrorVectical();
            }

            if (nextRect.Intersects(_paddle.Borders))
            {
                Collide(_ball, _paddle.Borders);
            }

            foreach (var brick in _bricks)
            {
                if (nextRect.Intersects(brick.Borders) && brick.IsAlive)
                {
                    brick.IsAlive = false;
                    Collide(_ball, brick.Borders);
                }
            }

            _ball.Position += _ball.Velocity;
        }

        public void Collide(GameObject gameObject, Rectangle rect2)
        {
            // отражение направления полёта по горизонтали при столкновении сверху илли снизу
            if (rect2.Left <= gameObject.Borders.Center.X && gameObject.Borders.Center.X <= rect2.Right)
            {
                gameObject.MirrorHorizontal();
            }

            // отражение направления полёта по вертикали при столкновении слева или справа
            else if (rect2.Top <= gameObject.Borders.Center.Y && gameObject.Borders.Center.Y <= rect2.Bottom)
            {
                gameObject.MirrorVectical();
            }
        }

        public void KeyboardProcessing()
        {
            KeyboardState state = Keyboard.GetState();

            // если Esc, то выход
            if (state.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateBall();

            KeyboardState keyboardState = Keyboard.GetState();

            // ракетка двигается вправо
            if (keyboardState.IsKeyDown(Keys.Right))
                _paddle.Position.X += 5f;
            // ракетка двигается влево
            if (keyboardState.IsKeyDown(Keys.Left))
                _paddle.Position.X -= 5f;

            _paddle.Position.X = MathHelper.Clamp(_paddle.Position.X, 0, _boundariesPlayingField.Width - _paddle.Width);


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);


            spriteBatch.Draw(_background, _boundariesPlayingField, Color.White);
            spriteBatch.Draw(_paddle.Sprite, _paddle.Position, Color.White);
            foreach (var brick in _bricks)
            {
                if (brick.IsAlive)
                {
                    spriteBatch.Draw(brick.Sprite, brick.Position, Color.White);
                }
            }
            spriteBatch.Draw(_ball.Sprite, _ball.Position, Color.White);


            spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}
