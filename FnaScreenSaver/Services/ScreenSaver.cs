using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FnaScreenSaver.Services
{
    /// <summary>
    /// Главный класс заставки — управляет отрисовкой и логикой снегопада.
    /// </summary>
    internal class ScreenSaver : Game
    {
        // Спрайты и текстуры
        private SpriteBatch spriteBatch = null!;
        private Texture2D background = null!;
        private Texture2D snowflakeTexture = null!;

        // Контейнер со снежинками
        private SnowflakeContainer flakes = null!;

        // Графический менеджер
        private readonly GraphicsDeviceManager graphics;

        /// <summary>
        /// Конструктор — настраиваю параметры игры.
        /// </summary>
        public ScreenSaver()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = true
            };

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Здесь я инициализирую все служебные объекты.
        /// </summary>
        protected override void Initialize()
        {
            var vp = GraphicsDevice.Viewport;
            flakes = new SnowflakeContainer(vp.Width, vp.Height);
            base.Initialize();
        }

        /// <summary>
        /// Загружаю необходимые ресурсы (текстуры и спрайты).
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("fon");
            snowflakeTexture = Content.Load<Texture2D>("Snowflake");

            base.LoadContent();
        }

        /// <summary>
        /// Освобождаю ресурсы при завершении работы.
        /// </summary>
        protected override void UnloadContent()
        {
            spriteBatch?.Dispose();
            background?.Dispose();
            snowflakeTexture?.Dispose();

            base.UnloadContent();
        }

        /// <summary>
        /// Здесь я проверяю ввод и обновляю состояние снежинок.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                Exit();
                return;
            }

            flakes.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Рисую фон и снегопад.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);

            foreach (var flake in flakes)
            {
                var rect = new Rectangle(
                    (int)flake.Left,
                    (int)flake.Top,
                    (int)flake.Size,
                    (int)flake.Size);

                spriteBatch.Draw(snowflakeTexture, rect, Color.Snow);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
