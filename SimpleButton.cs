using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ProjectMonoGame01.Geometry;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace ProjectMonoGame01
{
    /// <summary>
    /// Вертикальное выравнивание текста.
    /// </summary>
    public enum VerticalAlignment
    {
        Top,
        Center,
        Bottom
    }

    /// <summary>
    /// Горизонтальное выравнивание текста.
    /// </summary>
    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public class SimpleButton : DrawableGameComponent
    {
        #region поля
        private SpriteBatch batch;

        /// <summary>
        /// Подключение геометрии для контура кнопки.
        /// </summary>
        private GeometryHelper geometry;

        /// <summary>
        /// Поле для загрузки дока разрешения ".spritefont".
        /// </summary>
        private SpriteFont font;
        /// <summary>
        /// Текст на кнопке.
        /// </summary>
        private string _text;

        /// <summary>
        /// Позиция на экране.
        /// </summary>
        private Vector2 _pos;

        /// <summary>
        /// Цвет фона.
        /// </summary>
        private Color colorBack;
        /// <summary>
        /// Цвет надписи.
        /// </summary>
        private Color colorFont;

        /// <summary>
        /// Вертикальное выравнивание текста.
        /// </summary>
        private VerticalAlignment _vertAl;
        /// <summary>
        /// Горизонтальное выравнивание текста.
        /// </summary>
        private HorizontalAlignment _horAl;

        /// <summary>
        /// Высота кнопки.
        /// </summary>
        private int _hight;
        /// <summary>
        /// Ширина кнопки.
        /// </summary>
        private int _wight;

        /// <summary>
        /// Размеры текста (вектор - ширина и высота для шрифта в пикселях).
        /// </summary>
        private Vector2 _fontSize;

        /// <summary>
        /// Лист кнопок.
        /// </summary>
        private List<SpriteFont> _butList;
        /// <summary>
        /// Лист позиций (координат) кнопок.
        /// </summary>
        private List<Vector2> _posList;

        private List<GeometryFigure> _geoList;


        #endregion

        public List<GeometryFigure> GeoList
        {
            get { return _geoList; }
            set { _geoList = value; }
        }




        public SimpleButton(Game game, int width, int hight) : base(game)
        {
            game.Components.Add(this);

            geometry = new GeometryHelper(game);

            _fontSize = new Vector2();

            _butList = new List<SpriteFont>();
            _posList = new List<Vector2>();
            _geoList = new List<GeometryFigure>();

            _wight = width;
            _hight = hight;
        }

        public HorizontalAlignment horizontalAlignment
        {
            get { return _horAl; }

            set
            {
                _horAl = value;
                int i = 0;

                if (_posList.Contains(_pos))
                {
                    i = _posList.IndexOf(_pos);
                }

                switch (_horAl)
                {
                    case HorizontalAlignment.Left:
                        _pos.X += 5;
                        break;
                    case HorizontalAlignment.Center:
                        _pos.X += (_wight / 2) - (_fontSize.X / 2);
                        break;
                    case HorizontalAlignment.Right:
                        _pos.X += _wight - (_fontSize.X + 5);
                        break;
                }
                _posList[i] = _pos;
            }
        }

        public VerticalAlignment verticalAlignment
        {
            get { return _vertAl; }

            set
            {
                _vertAl = value;
                int i = 0;

                if (_posList.Contains(_pos))
                {
                    i = _posList.IndexOf(_pos);
                }

                switch (_vertAl)
                {
                    case VerticalAlignment.Top:
                        _pos.Y += 5;
                        break;
                    case VerticalAlignment.Center:
                        _pos.Y += (_hight / 2) - (_fontSize.Y / 2);
                        break;
                    case VerticalAlignment.Bottom:
                        _pos.Y += _hight - (_fontSize.Y + 5);
                        break;
                }
                _posList[i] = _pos;
            }
        }

        public Color ColorBack
        {
            get { return colorBack; }
            set { colorBack = value; }
        }

        public Color ColorFont
        {
            get { return colorFont; }
            set { colorFont = value; }
        }

        public Vector2 Position
        {
            get { return _pos; }
            set { _pos = value; }
        }

        public void AddButton(Vector2 pos, string text)
        {
            _text = text;
            _pos = pos;

            var pt = geometry.AddPoint((int)pos.X, (int)pos.Y, _wight, _hight);
            pt.Color = colorBack;

            _fontSize = font.MeasureString(_text);
            _butList.Add(font);
            _posList.Add(_pos);
            _geoList.Add(pt);
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            batch = new SpriteBatch(GraphicsDevice);
            font = Game.Content.Load<SpriteFont>("Score");
        }



        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            batch.Begin();

            for (int i = 0; i < _posList.Count; i++)
            {
                batch.DrawString(_butList[i], _text, _posList[i], colorFont);
            }

            batch.End();
        }
    }
}

