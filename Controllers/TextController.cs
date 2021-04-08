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
    public class TextController : DrawableGameComponent
    {
        #region поля 
        private SpriteBatch batch;
        /// <summary>
        /// поле для загрузки score.spritefont.
        /// </summary>
        private SpriteFont _font;
        /// <summary>
        /// Позиция на экране.
        /// </summary>
        private Vector2 _pos;
        /// <summary>
        /// Хранит координаты мыши.
        /// </summary>
        private List<Vector2> FontPos;
        /// <summary>
        /// Хранит надписи.
        /// </summary>
        private List<SpriteFont> spriteFonts;
        #endregion

        public TextController(Game game) : base(game)
        {
            game.Components.Add(this);
            FontPos = new List<Vector2>();
            spriteFonts = new List<SpriteFont>();
        }

        /// <summary>
        /// Добавляет текст на экране.
        /// </summary>
        /// <param name="position"></param>
        public void AddFont(Vector2 position)
        {
            _pos = position;
            FontPos.Add(_pos);
            spriteFonts.Add(_font);
        }

        /// <summary>
        /// Удаляет текстовую метку на экране.
        /// </summary>
        /// <param name="pos"></param>
        public void Delete(Vector2 pos)
        {
            // получили координаты мыши
            int x = (int)pos.X;
            int y = (int)pos.Y;

            // сравниваем полученные координаты с имеющимися в листе
            for (int i = 0; i < FontPos.Count; i++)
            {
                x -= (int)FontPos[i].X;
                y -= (int)FontPos[i].Y;

                if (x < 101 && x > -101 && y < 101 && y > -101)
                {
                    // удалилили координаты и потом удалили надпись на экране 
                    FontPos.RemoveAt(i);
                    spriteFonts.RemoveAt(i);
                }
                x = (int)pos.X;
                y = (int)pos.Y;
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            batch = new SpriteBatch(GraphicsDevice);
            _font = Game.Content.Load<SpriteFont>("Score");
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            batch.Begin();
            for (int i = 0; i < spriteFonts.Count; i++)
            {
                batch.DrawString(spriteFonts[i], "The Rock Army - \"Back in Black\"", FontPos[i], Color.Black);
            }
            batch.End();
        }
    }
}
