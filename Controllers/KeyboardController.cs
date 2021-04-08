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
    // делегат для события "Выстрел"
    public delegate void EventShoot();


    // класс для замены Dictionary<Keys, ActionType>
    public class ActionKeys : Dictionary<Keys, ActionType>
    {

    }

    // Поддержка смены раскладки клавиатуры

    /// <summary>
    /// Виды воздействия/управления
    /// игрового объекта
    /// </summary>
    public enum ActionType
    {
        MoveLeft,
        MoveRight,
        MoveForward,
        MoveBackward,
        Shoot // Jump, Fly, ...
    }

    /// <summary>
    /// Виды движения игрового объекта
    /// (стрэйф, с поворотами)
    /// </summary>
    public enum MovementType
    {
        Simple, Advanced
    }

    /// <summary>
    /// Виды стандартной клавиатурной
    /// раскладки
    /// </summary>
    public enum LayoutKeyboard
    {
        LayoutWASD,
        LayoutArrows
    }

    public class KeyboardController
        : BaseController
    {
        // prev - previous
        protected KeyboardState _prevKs;

        /// <summary>
        /// Событие - выстрел
        /// </summary>
        public event EventShoot ShootClick;
        public event EventShoot ShootPress;

        private LayoutKeyboard _layout;

        public LayoutKeyboard Layout
        {
            get { return _layout; }
            set
            {
                _layout = value;
                switch (_layout)
                {
                    case LayoutKeyboard.LayoutWASD:
                        _actionKeyCurrent = _actionKeyWASD;
                        break;
                    case LayoutKeyboard.LayoutArrows:
                        _actionKeyCurrent = _actionKeyArrows;
                        break;
                }
            }
        }

        /// <summary>
        /// Раскадка клавиатуры -
        /// словарь: Клавиша - Действие
        /// </summary>
        private ActionKeys _actionKeyCurrent;

        private ActionKeys _actionKeyWASD;

        private ActionKeys _actionKeyArrows;

        private MovementType _type;

        public MovementType MovementType
        {
            get { return _type; }
            set { _type = value; }
        }

        private float _speed;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public void SetCustomLayout(ActionKeys layout)
        {
            _actionKeyCurrent = layout;
        }


        public KeyboardController(Game game)
            : base(game)
        {
            _speed = 1;

            // создаём две стандартные раскладки
            _actionKeyWASD = new ActionKeys()
            {
                { Keys.W, ActionType.MoveForward },
                { Keys.A, ActionType.MoveLeft },
                { Keys.D, ActionType.MoveRight },
                { Keys.S, ActionType.MoveBackward },
                { Keys.Space, ActionType.Shoot }
            };
            _actionKeyArrows = new ActionKeys()
            {
                { Keys.Up, ActionType.MoveForward },
                { Keys.Left, ActionType.MoveLeft },
                { Keys.Right, ActionType.MoveRight },
                { Keys.Down, ActionType.MoveBackward },
                { Keys.Enter, ActionType.Shoot }
            };
            //--------------------------------
            /*
            _actionKeyCurrent = _actionKeyArrows;
            _layout = LayoutKeyboard.LayoutArrows;
            */
            Layout = LayoutKeyboard.LayoutArrows;
        }

        private void SimpleMove(KeyboardState ks, ref Vector2 delta)
        {
            // смотрим текущую раскладку
            foreach (var pair in _actionKeyCurrent)
            {
                Keys k = pair.Key;
                ActionType move = pair.Value;
                if (ks.IsKeyDown(k))
                {
                    switch (move)
                    {
                        case ActionType.MoveLeft:
                            delta.X = -1;
                            break;
                        case ActionType.MoveRight:
                            delta.X = +1;
                            break;
                        case ActionType.MoveForward:
                            delta.Y = -1;
                            break;
                        case ActionType.MoveBackward:
                            delta.Y = +1;
                            break;

                        case ActionType.Shoot:
                            // зажигаем событие
                            if (ShootPress != null)
                                ShootPress();
                            break;
                    }
                }

                if (ks.IsKeyUp(k) && move == ActionType.Shoot)
                {
                    // если клавиша стрельбы - сейчас не нажата
                    // а ранее была нажата - выстрел!
                    if (_prevKs.IsKeyDown(k))
                    {
                        if (ShootClick != null)
                            ShootClick();
                    }
                }
            }
        }

        private void AdvancedMove(KeyboardState ks, ref Vector2 delta)
        {
            // угол поворота 5°  // alt 248
            float rotation = (float)(Math.PI / 180) * 2;
            foreach (var pair in _actionKeyCurrent)
            {
                Keys k = pair.Key;
                ActionType move = pair.Value;
                if (ks.IsKeyDown(k))
                {
                    switch (move)
                    {
                        case ActionType.MoveLeft:
                            _driven.Direction -= rotation;
                            break;
                        case ActionType.MoveRight:
                            _driven.Direction += rotation;
                            break;
                        case ActionType.MoveForward:
                            delta.X = (float)Math.Cos(_driven.Direction);
                            delta.Y = (float)Math.Sin(_driven.Direction);
                            break;
                        case ActionType.MoveBackward:
                            delta.X = -(float)Math.Cos(_driven.Direction);
                            delta.Y = -(float)Math.Sin(_driven.Direction);
                            break;

                        case ActionType.Shoot:
                            // зажигаем событие
                            if (ShootPress != null)
                                ShootPress();
                            break;

                    }
                }

                if (ks.IsKeyUp(k) && move == ActionType.Shoot)
                {
                    // если клавиша стрельбы - сейчас не нажата
                    // а ранее была нажата - выстрел!
                    if (_prevKs.IsKeyDown(k))
                    {
                        if (ShootClick != null)
                            ShootClick();
                    }
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            var delta = new Vector2();

            switch (_type)
            {
                case MovementType.Simple:
                    SimpleMove(ks, ref delta);
                    break;
                case MovementType.Advanced:
                    AdvancedMove(ks, ref delta);
                    break;
            }

            _driven.Move(delta * _speed);

            // запоминаем состояние клавиатуры - как
            // предыдущее
            _prevKs = ks;

            base.Update(gameTime);
        }
    }
}
