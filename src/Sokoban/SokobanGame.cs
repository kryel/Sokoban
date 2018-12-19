using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sokoban.Implementation;

namespace Sokoban
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SokobanGame : Game
    {
        private const int Size = 64;

        private readonly HashSet<Keys> _pressedKeys;

        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Level _level;
        private GameImplementation _game;

        private Texture2D _wall;
        private Texture2D _empty;
        private Texture2D _player;
        private Texture2D _target;
        private Texture2D _box;
        private Texture2D _screen;

        private SpriteFont _font;

        public SokobanGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _pressedKeys = new HashSet<Keys>();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _level = LevelParser.FromFile("Resources/Levels/Level0.txt");
            _game = new GameImplementation(_level);

            _graphics.PreferredBackBufferWidth = _level.Width * Size + 2;
            _graphics.PreferredBackBufferHeight = _level.Height * Size + 2;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _empty = Content.Load<Texture2D>("empty");
            _wall = Content.Load<Texture2D>("wall");
            _player = Content.Load<Texture2D>("player");
            _target = Content.Load<Texture2D>("target");
            _box = Content.Load<Texture2D>("box");
            _screen = Content.Load<Texture2D>("screen");

            _font = Content.Load<SpriteFont>("Font");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var pressedKeys = Keyboard.GetState().GetPressedKeys();
            foreach (var key in pressedKeys)
            {
                if (!_pressedKeys.Contains(key))
                {
                    _pressedKeys.Add(key);
                    OnPressed(key);
                }
            }

            _pressedKeys.RemoveWhere(k => !pressedKeys.Contains(k));

            base.Update(gameTime);
        }

        private void OnPressed(Keys key)
        {
            if (key == Keys.Escape)
            {
                Exit();
                return;
            }

            if (_game.HasWon())
            {
                if (key == Keys.R || key == Keys.Space || key == Keys.Enter)
                {
                    _game.Reset();
                }

                return;
            }

            switch (key)
            {
                case Keys.Escape:
                    Exit();
                    break;

                case Keys.W:
                case Keys.Up:
                    _game.MovePlayer(Direction.North);
                    break;
                case Keys.A:
                case Keys.Left:
                    _game.MovePlayer(Direction.West);
                    break;
                case Keys.S:
                case Keys.Down:
                    _game.MovePlayer(Direction.South);
                    break;
                case Keys.D:
                case Keys.Right:
                    _game.MovePlayer(Direction.East);
                    break;

                case Keys.R:
                    _game.Reset();
                    break;

                case Keys.PageDown:
                    _game.Undo();
                    break;
                case Keys.PageUp:
                    _game.Redo();
                    break;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            var gameState = _game.GetCurrentState();
            for (var y = 0; y < _level.Height; y++)
            {
                for (var x = 0; x < _level.Width; x++)
                {
                    var tile = new Tile(x, y);
                    var position = new Vector2(x * Size, y * Size);

                    _spriteBatch.Draw(GetTileTexture(tile), position, Color.White);
                }
            }

            foreach (var box in gameState.BoxPositions)
            {
                _spriteBatch.Draw(_box, new Vector2(box.X * Size, box.Y * Size), Color.White);
            }

            var player = gameState.PlayerPosition;
            _spriteBatch.Draw(_player, new Vector2(player.X * Size, player.Y * Size), Color.White);

            if (_game.HasWon())
            {
                _spriteBatch.Draw(_screen, new Vector2(0, 0), Color.Black * 0.5f);
                _spriteBatch.DrawString(_font, "You've won!", new Vector2(100, 100), Color.Azure);
            }

            _spriteBatch.End();

            base.Draw(gameTime);

            Texture2D GetTileTexture(Tile tile)
            {
                if (_level.Walls.Contains(tile))
                {
                    return _wall;
                }

                if (_level.TargetPositions.Contains(tile))
                {
                    return _target;
                }

                return _empty;
            }
        }
    }
}
