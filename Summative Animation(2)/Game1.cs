using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;

namespace Summative_Animation_2_
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        enum Screen
        {
            Intro,
            Waterfall1,
            Waterfall2,
            Outro
        }

        enum FriskAction
        {
            start,
            leg1,
            lookAround,
            leg2,

        }

        Screen screen;
        FriskAction friskAction;
        SpriteFont dialogue, title;
        MouseState mouseState;
        Texture2D friskRight1, friskRight2, friskRightStill, friskLeft1, friskLeft2, friskLeftStill, friskForward1, friskForward2, friskForwardStill, friskBack1, friskBackStill, undyneRight1, undyneRight2, undyneForward1, undyneForward2, undyneRightStill, introScreen, waterFall1, waterFall2, outroScreen;
        Vector2 friskSpeed, undyneSpeed;
        Rectangle friskRect, undyneRect, backgroundRect, titleRect;

        int friskTurns = 0;
        float timeStamp, milliseconds, animationCycleMilliSec;
       

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;

            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 700;
            _graphics.ApplyChanges();

            backgroundRect = new Rectangle(0, 0, 1000, 700);
            friskRect = new Rectangle(425, 632, 42, 68);
            undyneRect = new Rectangle(300, 500, 50, 105);


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // FRISK WALKING ANIMATION SPRITES
            friskRightStill = Content.Load<Texture2D>("FriskRightStill");
            friskRight1 = Content.Load<Texture2D>("FriskRightWalk1");

            friskLeftStill = Content.Load<Texture2D>("FriskLeftStill");
            friskLeft1 = Content.Load<Texture2D>("FriskLeftWalk1");

            friskForwardStill = Content.Load<Texture2D>("FriskForwardStill");
            friskForward1 = Content.Load<Texture2D>("FriskForwardWalk1");
            friskForward2 = Content.Load<Texture2D>("FriskForwardWalk2");

            friskBackStill = Content.Load<Texture2D>("FriskBackStill");
            friskBack1 = Content.Load<Texture2D>("FriskBackWalk1");


            // UNDYNE WALKING ANIMATION SPRITES
            undyneRightStill = Content.Load<Texture2D>("UndyneStill");
            undyneRight1 = Content.Load<Texture2D>("UndyneWalkRight1");
            undyneRight2 = Content.Load<Texture2D>("UndyneWalkRight2");


            // BACKGROUNDS
            introScreen = Content.Load<Texture2D>("RuinsIntroScreen");
            waterFall1 = Content.Load<Texture2D>("WaterfallScreen1");
            waterFall2 = Content.Load<Texture2D>("WaterfallScreen2");

            title = Content.Load<SpriteFont>("title");
            dialogue = Content.Load<SpriteFont>("dialogue");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && backgroundRect.Contains(mouseState.X, mouseState.Y))
                {
                    screen = Screen.Waterfall1;
                    friskAction = FriskAction.leg1;
                }
            }
            if (screen == Screen.Waterfall1)
            {
                if (friskAction == FriskAction.leg1)
                {
                    friskSpeed = new Vector2(0, -3);
                    friskRect.Y += (int)friskSpeed.Y;

                    if (friskRect.Top < 100)
                    {
                        timeStamp = (float)gameTime.TotalGameTime.TotalMilliseconds;
                        friskSpeed = new Vector2(0, 0);
                        friskAction = FriskAction.lookAround;
                    } 
                }
                else if (friskAction == FriskAction.lookAround)
                {
                    milliseconds = (float)gameTime.TotalGameTime.TotalMilliseconds - timeStamp;
                    if (milliseconds >= 2000)
                    {
                        friskSpeed = new Vector2(3, 0);
                        friskAction= FriskAction.leg2;
                    } 
                }
                else if (friskAction == FriskAction.leg2)
                {
                    friskRect.X += (int)friskSpeed.X;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introScreen, backgroundRect, Color.White);
                _spriteBatch.DrawString(title, "Undertale: The Animation", new Vector2(270, 50), Color.White);
                _spriteBatch.DrawString(dialogue, "Click anywhere to begin", new Vector2(270, 110), Color.White);
            }
            if (screen == Screen.Waterfall1)
            {
                _spriteBatch.Draw(waterFall1, backgroundRect, Color.White);

                int i = 1;
                if (i == 60)
                {
                    timeStamp = (float)gameTime.TotalGameTime.TotalMilliseconds;
                }
                if (friskAction == FriskAction.leg1)
                {
                    Window.Title = milliseconds.ToString() + "  :  " + timeStamp.ToString();

                    milliseconds = (float)gameTime.TotalGameTime.TotalMilliseconds - timeStamp;
                    if (milliseconds > timeStamp && milliseconds <= timeStamp + 250)
                    {
                        _spriteBatch.Draw(friskBack1, friskRect, Color.White);
                    }
                    else if (milliseconds > timeStamp + 250 && milliseconds <= timeStamp + 500)
                    {
                        _spriteBatch.Draw(friskBackStill, friskRect, Color.White);
                    }
                    else if (milliseconds > timeStamp + 500 && milliseconds <= timeStamp + 750)
                    {
                        _spriteBatch.Draw(friskBack1, friskRect, Color.White);
                    }
                    else if (milliseconds > timeStamp + 750 && milliseconds <= timeStamp + 999)
                    {
                        _spriteBatch.Draw(friskBackStill, friskRect, Color.White);
                    }
                }


                if (friskAction == FriskAction.lookAround)
                {                  
                    if (milliseconds <= 1000)
                    { 
                        _spriteBatch.Draw(friskLeftStill, friskRect, Color.White); 
                    }
                    else if (milliseconds <= 2000)
                    {                      
                        _spriteBatch.Draw(friskRightStill, friskRect, Color.White); 
                    }
                    
                }
                else if (friskAction == FriskAction.leg2)
                {
                    _spriteBatch.Draw(friskRight1, friskRect, Color.White);
                }

            }


            if (screen == Screen.Waterfall2)
            {
                _spriteBatch.Draw(waterFall2, backgroundRect, Color.White);
            }


            if (screen == Screen.Outro)
            {
                _spriteBatch.Draw(outroScreen, backgroundRect, Color.White);
            }
            


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}