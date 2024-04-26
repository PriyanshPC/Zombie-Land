using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ZombieLand.Controls;
using ZombieLand.Managers;
using ZombieLand.Models;

namespace ZombieLand.States
{
    public class HighscoresState : State
    {
        private List<Component> _components;

        private SpriteFont _font;

        private ScoreManager _scoreManager;

        public HighscoresState(Game1 game, ContentManager content)
          : base(game, content)
        {
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Font");

            _scoreManager = ScoreManager.Load();

            var buttonTexture = _content.Load<Texture2D>("Other/Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            _components = new List<Component>()
            {
                 new Button(buttonTexture, buttonFont)
                    {
                         Text = "Main Menu",
                         Position = new Vector2(Game1.ScreenWidth * 2 / 3, Game1.ScreenHeight - 300),
                         Click = new EventHandler(Button_MainMenu_Clicked),
                         Layer = 0.1f
                     },
                 new Button(buttonTexture, buttonFont)
                     {
                          Text = "Clear Score",
                          Position = new Vector2(Game1.ScreenWidth / 3, Game1.ScreenHeight - 300),
                          Click = new EventHandler(Button_ClearScore_Clicked),
                          Layer = 0.1f
                     }
             };

    }

        private void Button_MainMenu_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _content));
        }
        private void Button_ClearScore_Clicked(object sender, EventArgs args)
        {
            _scoreManager.Scores.Clear();
            ScoreManager.Save(_scoreManager);

            _game.ChangeState(new HighscoresState(_game, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Clicked(this, new EventArgs());

            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            spriteBatch.DrawString(_font, "Highscores:\n\n" + string.Join("\n", _scoreManager.HighScores.Select(c => c.PlayerName + ": " + c.Value)), new Vector2(Game1.ScreenWidth / 3, 400), Color.White);

            spriteBatch.End();
        }
    }
}
