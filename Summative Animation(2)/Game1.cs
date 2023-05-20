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

        List<Texture2D> friskRightTextures = new List<Texture2D>();
        List<Texture2D> friskUpTextures = new List<Texture2D>();
        List<Texture2D> undyneUpTextures = new List<Texture2D>();
        List<Texture2D> undyneRightTextures = new List<Texture2D>();

        Screen screen;
        FriskAction friskAction;
        UndyneAction undyneAction;
        SpriteFont dialogue, title;
        MouseState mouseState;
        Texture2D friskRight1, friskRightStill, friskLeftStill, friskBack1, friskBack2, friskBackStill, undyneRight1, undyneRight2, undyneRight3, undyneUp1, undyneUp2, undyneUp3, introScreen, waterFall1, speechBubble;
        Vector2 friskSpeed, undyneSpeed;
        Rectangle friskRect, undyneRect,speechBubbleRect, backgroundRect;

        int friskAnimationCycleCounter = 0, undyneAnimationCycleCounter = 0, friskSpriteTimer = 0, undyneSpriteTimer = 0;
        float timeStamp, milliseconds, undyneStartCounter, undyneStartTimeStamp;
        bool callForHelp = false;
       

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
            undyneRect = new Rectangle(425, 832, 50, 105);
            speechBubbleRect = new Rectangle(700, 10, 250, 150);


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
            friskBack2 = Content.Load<Texture2D>("FriskBackWalk2");

            friskUpTextures.Add(friskBackStill);
            friskUpTextures.Add(friskBack1);
            friskUpTextures.Add(friskBack2);

            // UNDYNE WALKING ANIMATION SPRITES
            undyneRight1 = Content.Load<Texture2D>("UndyneRight1");
            undyneRight2= Content.Load<Texture2D>("UndyneRight2");
            undyneRight3= Content.Load<Texture2D>("UndyneRight3");

            undyneRightTextures.Add(undyneRight1);
            undyneRightTextures.Add(undyneRight2);
            undyneRightTextures.Add(undyneRight3);

            
            undyneUp1 = Content.Load<Texture2D>("UndyneUp1");
            undyneUp2 = Content.Load<Texture2D>("UndyneUp2");
            undyneUp3 = Content.Load<Texture2D>("UndyneUp3");

            undyneUpTextures.Add(undyneUp1);
            undyneUpTextures.Add(undyneUp2);
            undyneUpTextures.Add(undyneUp3);



            // BACKGROUNDS
            introScreen = Content.Load<Texture2D>("RuinsIntroScreen");
            waterFall1 = Content.Load<Texture2D>("WaterfallScreen1");

            title = Content.Load<SpriteFont>("title");
            dialogue = Content.Load<SpriteFont>("dialogue");

            speechBubble = Content.Load<Texture2D>("speechBubble");
        }

        protected override void Update(GameTime gameTime)
        {
            Window.Title = milliseconds.ToString();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            mouseState = Mouse.GetState();
            
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && backgroundRect.Contains(mouseState.X, mouseState.Y))
                {
                    screen = Screen.Waterfall1;
                    friskAction = FriskAction.leg1;
                    timeStamp = (float)gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
            else if (screen == Screen.Waterfall1)
            {
                // FRISK
                if (friskAction == FriskAction.leg1)
                {
                    friskSpriteTimer++;
                    friskSpeed = new Vector2(0, -3);
                    friskRect.Y += (int)friskSpeed.Y;

                    if (friskSpriteTimer == 15)
                    {
                        if (friskAnimationCycleCounter >= 0 && friskAnimationCycleCounter < 2) 
                        {
                            friskAnimationCycleCounter++;
                        }
                        else if (friskAnimationCycleCounter == 2) 
                        {
                            friskAnimationCycleCounter = 0;
                        }   
                        friskSpriteTimer = 0;
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
                        friskAnimationCycleCounter = 0;
                        friskSpeed = new Vector2(3, 0);
                        friskAction = FriskAction.leg2;
                        timeStamp = (float)gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }


                else if (friskAction == FriskAction.leg2)
                {
                    milliseconds = (float)gameTime.TotalGameTime.TotalMilliseconds - timeStamp;

                    friskSpriteTimer++;
                    friskRect.X += (int)friskSpeed.X;
                    if (friskSpriteTimer == 15)
                    {
                        if (friskAnimationCycleCounter == 0) 
                        {
                            friskAnimationCycleCounter = 1;
                        }
                        else if (friskAnimationCycleCounter == 1) 
                        {
                            friskAnimationCycleCounter = 0;
                        }
                        friskSpriteTimer = 0;
                    }
                    if (milliseconds >= 1000 && milliseconds < 1750)
                    {
                        callForHelp = true;
                    }
                    else
                    {
                        callForHelp = false;
                    }
                    if (milliseconds >= 4750)
                    {
                        screen = Screen.Outro;
                    }
                }
                



                // UNDYNE
                if (undyneAction == UndyneAction.leg1)
                {
                    undyneSpriteTimer++;
                    undyneStartCounter = (float)gameTime.TotalGameTime.TotalMilliseconds - timeStamp;
                    if ((float)gameTime.TotalGameTime.TotalMilliseconds - undyneStartCounter >= 3000)
                    {
                        undyneSpeed = new Vector2(0, -4);
                    }
                    undyneRect.Y += (int)undyneSpeed.Y;
                    if (undyneSpriteTimer == 15)
                    {
                        if (undyneAnimationCycleCounter >= 0 && undyneAnimationCycleCounter < 2)
                        {
                            undyneAnimationCycleCounter++;
                        }
                        else if (undyneAnimationCycleCounter == 2)
                        {
                            undyneAnimationCycleCounter = 0;
                        }
                        undyneSpriteTimer = 0;
                    }
                    if (undyneRect.Y <= 75)
                    {
                        undyneAction = UndyneAction.leg2;
                        undyneSpeed = new Vector2(4, 0);
                    }
                }
                else if (undyneAction == UndyneAction.leg2)
                {
                    undyneSpriteTimer++;
                    undyneRect.X += (int)undyneSpeed.X;
                    if (undyneSpriteTimer == 15)
                    {
                        if (undyneAnimationCycleCounter >= 0 && undyneAnimationCycleCounter < 2)
                        {
                            undyneAnimationCycleCounter++;
                        }
                        else if (undyneAnimationCycleCounter == 2)
                        {
                            undyneAnimationCycleCounter = 0;
                        }
                        undyneSpriteTimer = 0;
                    }
                }
            }
            if (screen == Screen.Outro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && backgroundRect.Contains(mouseState.X, mouseState.Y))
                {
                    Exit();
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


                // FRISK
                if (friskAction == FriskAction.leg1)
                {
                    _spriteBatch.Draw(friskUpTextures[friskAnimationCycleCounter], friskRect, Color.White);
                }
                else if (friskAction == FriskAction.lookAround)
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
                    _spriteBatch.Draw(friskRightTextures[friskAnimationCycleCounter], friskRect, Color.White);
                }
                if (callForHelp)
                {
                    _spriteBatch.Draw(speechBubble, speechBubbleRect, Color.White);
                    _spriteBatch.DrawString(dialogue, "Help!", new Vector2(800, 50), Color.Black);
                }


                // UNDYNE
                if (undyneAction == UndyneAction.leg1)
                {
                    _spriteBatch.Draw(undyneUpTextures[undyneAnimationCycleCounter], undyneRect, Color.White);
                }
                else if (undyneAction == UndyneAction.leg2)
                {
                    _spriteBatch.Draw(undyneRightTextures[undyneAnimationCycleCounter], undyneRect, Color.White);

                }
            }

            if (screen == Screen.Outro)
            {
                _spriteBatch.Draw(introScreen, backgroundRect, Color.White);
                _spriteBatch.DrawString(title, "THE END", new Vector2(270, 50), Color.White);
                _spriteBatch.DrawString(dialogue, "Click anywhere to exit", new Vector2(270, 110), Color.White);
               
            }



            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}