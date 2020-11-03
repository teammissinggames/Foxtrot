using Foxtrot.Models;
using Foxtrot.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace Foxtrot
{
    public class Engine : Game
    {
        private GraphicsDeviceManager _graphics;

        private OrthographicCamera _camera;

        private TiledMap map;

        private TiledMapRenderer mapRenderer;

        private SpriteBatch _characterSpriteBatch;

        private List<ControllableSprite> _sprites;

        private Sprite background;

        public Engine()
        {
            //main game constructor. Use it to initialize starting variables
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //ensures stable framerate of 60 fps.
            IsFixedTimeStep = true;
            TargetElapsedTime = TimeSpan.FromMilliseconds(16); // 16 milliseconds, or 60 FPS.
        }

        protected override void Initialize()
        {
            //1920 * 1080 OR 960 * 540
            _graphics.PreferredBackBufferWidth = 1920;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 1080;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            //This method is called after the constructor, but before the main game loop (Update/Draw)

            base.Initialize();

            var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _camera = new OrthographicCamera(viewportAdapter);

            //Load the compiled map
            //map.Content.Load<TiledMap>("insert path to map file")

            //Create the map renderer
            mapRenderer = new TiledMapRenderer(GraphicsDevice);
        }

        protected override void LoadContent()
        {
            //LoadContent is used to load game content. It is called only once per game, after Initialize method, but before the main game loop methods

            //create a new SpriteBatch, which can be used to draw textures.
            _characterSpriteBatch = new SpriteBatch(GraphicsDevice);

            var animations = new Dictionary<string, Animation>()
            {
                //{name, Animation object with reference to png, #frames in animation}
                {"Idle", new Animation(Content.Load<Texture2D>("CHEF01"), 5) },
            };

            _sprites = new List<ControllableSprite>()
            {
                //Load a controllable sprite
                new ControllableSprite(animations)
                {
                    Position = new Vector2(100, 100),
                    Input = new Input()
                    {
                        Up = Keys.W,
                        Down = Keys.S,
                        Left = Keys.A,
                        Right = Keys.D,
                    }
                },
            };
            //load and initialize background
            background = new Sprite(Content.Load<Texture2D>("FLOOR_TILE_1"))
            {
                Position = new Vector2(0, 0)
            };
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var transformMatrix = _camera.GetViewMatrix();

            _characterSpriteBatch.Begin(transformMatrix: transformMatrix);

            //draw background
            background.Draw(_characterSpriteBatch);

            //draw all listed sprites
            foreach (var sprite in _sprites)
                sprite.Draw(_characterSpriteBatch);
            _characterSpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
