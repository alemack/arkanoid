using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using ProjectMonoGame01.Geometry;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectMonoGame01.Controllers
{
    class UIController : BaseController
    {
        /// <summary>
        /// Кнопка.
        /// </summary>
        private SimpleButton button;

        public UIController(Game game) : base(game)
        {
            button = new SimpleButton(game, 300, 100);
        }

        public void Buttom()
        {
            button.ColorFont = Color.Black;

            button.ColorBack = Color.Coral;
            button.AddButton(new Vector2(10, 10), "Пуск начался");
            button.verticalAlignment = VerticalAlignment.Center;
            button.horizontalAlignment = HorizontalAlignment.Center;

            button.ColorBack = Color.DarkGray;
            button.AddButton(new Vector2(350, 10), "Пуск начался");
            button.verticalAlignment = VerticalAlignment.Center;
            button.horizontalAlignment = HorizontalAlignment.Center;
        }

        public void Click(Vector2 pos)
        {
            Random rnd = new Random();

            int red = rnd.Next(0, 255);
            int green = rnd.Next(0, 255);
            int blue = rnd.Next(0, 255);

            Color colorRandom = new Color(red, green, blue);

            if (pos.X > 10 && pos.X < 310 && pos.Y > 10 && pos.Y < 110)
            {
                button.GeoList[0].Color = colorRandom;
            }

            if (pos.X > 350 && pos.X < 660 && pos.Y > 10 && pos.Y < 110)
            {
                button.GeoList[1].Color = colorRandom;
            }
        }

        public void MouseEnter(Vector2 pos)
        {

            if (pos.X > 10 && pos.X < 310 &&
                pos.Y > 10 && pos.Y < 110)
            {
                button.GeoList[0].Scale = new Vector2(340, 150);
            }
            else
            if (pos.X > 350 && pos.X < 660 && pos.Y > 10 && pos.Y < 110)
            {
                button.GeoList[1].Scale = new Vector2(340, 150);
            }
            else
            {
                button.GeoList[0].Scale = new Vector2(300, 100);
                button.GeoList[1].Scale = new Vector2(300, 100);
            }
        }

        public void Draw(GameTime gameTime)
        {
            button.Draw(gameTime);
        }
    }
}
