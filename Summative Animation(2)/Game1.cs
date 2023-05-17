using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
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
            leg1,
            lookAround,
            leg2,
        }
        enum UndyneAction
        {
            leg1,
            leg2,
        }

        List<Texture2D> friskRightTextures = new List<Texture2D>(); // aldworth
        List<Texture2D> friskUpTextures = new List<Texture2D>();
        List<Texture2D> undyneUpTextures = new List<Texture2D>();

        Screen screen;
        FriskAction friskAction;
        UndyneAction undyneAction;
        SpriteFont dialogue, title;
        MouseState mouseState;
        Texture2D friskRight1, friskRightStill,  friskLeftStill, friskBack1, friskBackStill, undyneRight1, undyneRight2, undyneRight3, undyneUp1, undyneUp2, undyneUp3, introScreen, waterFall1, waterFall2, outroScreen;
        Vector2 friskSpeed, undyneSpeed;
        Rectangle friskRect, undyneRect, backgroundRect, titleRect;

        int friskTurns = 0, animationCycleCounter = 0, i = 0, j = 0;
        float timeStamp, milliseconds, undyneStartCounter, undyneStartTimeStamp;
       

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

            friskRightTextures.Add(friskRightStill); 
            friskRightTextures.Add(friskRight1);

            friskBackStill = Content.Load<Texture2D>("FriskBackStill");
            friskBack1 = Content.Load<Texture2D>("FriskBackWalk1");

            friskUpTextures.Add(friskBackStill);
            friskUpTextures.Add(friskBack1);

            // UNDYNE WALKING ANIMATION SPRITES
            undyneRight1 = Content.Load<Texture2D>("UndyneRight1");
            undyneRight1 = Content.Load<Texture2D>("UndyneRight2");
            undyneRight2 = Content.Load<Texture2D>("UndyneRight3");

            undyneUp1 = Content.Load<Texture2D>("UndyneUp1");
            undyneUp2 = Content.Load<Texture2D>("UndyneUp2");
            undyneUp3 = Content.Load<Texture2D>("UndyneUp3");

            undyneUpTextures.Add(undyneUp1);
            undyneUpTextures.Add(undyneUp2);



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
                if (j == 0)
                {
                    timeStamp = (float)gameTime.TotalGameTime.TotalMilliseconds;
                }
                if (friskAction == FriskAction.leg1)
                {
                    i++;
                    friskSpeed = new Vector2(0, -3);
                    friskRect.Y += (int)friskSpeed.Y;

                    if (i == 15)
                    {
                        if (animationCycleCounter == 0) // change these to 2 if another sprite is needed
                        {
                            animationCycleCounter = 1;
                        }
                        else if (animationCycleCounter == 1) // ^^
                        {
                            animationCycleCounter = 0;
                        }
                        i = 0;
                    }
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
                    if (milliseconds >= 1000)
                    {
                        friskSpeed = new Vector2(3, 0);
                        friskAction = FriskAction.leg2;
                    }
                }
                else if (friskAction == FriskAction.leg2)
                {
                    i++;
                    friskRect.X += (int)friskSpeed.X;
                    if (i == 15)
                    {
                        if (animationCycleCounter == 0) // change these to 2 if another sprite is needed
                        {
                            animationCycleCounter = 1;
                        }
                        else if (animationCycleCounter == 1) // ^^
                        {
                            animationCycleCounter = 0;
                        }
                        i = 0;
                    }
                }
                if (undyneAction == UndyneAction.leg1)
                {
                    undyneStartCounter = (float)gameTime.TotalGameTime.TotalMilliseconds - timeStamp;
                    if ((float)gameTime.TotalGameTime.TotalMilliseconds - undyneStartCounter >= 1000)
                    {
                        undyneSpeed = new Vector2(0, -4);
                        undyneRect.Y += (int)undyneRect.Y;
                    }
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

                if (friskAction == FriskAction.leg1)
                {
                    _spriteBatch.Draw(friskUpTextures[animationCycleCounter], friskRect, Color.White);
                }


                if (friskAction == FriskAction.lookAround)
                {                  
                    if (milliseconds <= 500)
                    { 
                        _spriteBatch.Draw(friskLeftStill, friskRect, Color.White); 
                    }
                    else if (milliseconds <= 1000)
                    {                      
                        _spriteBatch.Draw(friskRightStill, friskRect, Color.White); 
                    }
                    
                }
                else if (friskAction == FriskAction.leg2)
                {
                    _spriteBatch.Draw(friskRightTextures[animationCycleCounter], friskRect, Color.White);
                }



                if (undyneAction == UndyneAction.leg1)
                {
                    _spriteBatch.Draw(undyneUpTextures[animationCycleCounter], undyneRect, Color.White);
                }
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