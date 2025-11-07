using FnaScreenSaver.Models;
using Microsoft.Xna.Framework;
using System.Collections;

namespace FnaScreenSaver.Services
{
    /// <summary>
    /// Это мой контейнер снежинок — здесь я храню, двигаю и создаю их заново при необходимости.
    /// </summary>
    internal class SnowflakeContainer : IEnumerable<Snowflake>
    {
        // Параметры генерации и физики
        private const double MinLayer = 1.0;        // минимальная "глубина"
        private const double MaxLayer = 8.0;        // максимальная "глубина"
        private const double WindEffect = 0.2;      // снос ветром
        private const double SpeedVariance = 0.3;   // разброс скоростей
        private const double DefaultSpeed = 1000.0; // базовая скорость падения
        private const double DefaultSize = 40.0;    // базовый размер снежинки
        private const int TotalFlakes = 1250;       // общее количество снежинок

        // Параметры экрана
        private double screenWidth;
        private double screenHeight;

        // Генератор случайных чисел
        private readonly Random randomizer = new();

        // Массив снежинок
        private readonly Snowflake[] allFlakes = new Snowflake[TotalFlakes];

        /// <summary>
        /// Конструктор: здесь я запоминаю размеры экрана и генерирую первую партию снежинок.
        /// </summary>
        public SnowflakeContainer(double width, double height)
        {
            screenWidth = width;
            screenHeight = height;

            for (int i = 0; i < allFlakes.Length; i++)
            {
                CreateFlake(i);
            }
        }

        /// <summary>
        /// Здесь я пересчитываю координаты всех снежинок за время одного кадра.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            double delta = gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < allFlakes.Length; i++)
            {
                var flake = allFlakes[i];

                // Перемещаю снежинку вниз и чуть-чуть в сторону (ветер)
                double move = flake.Speed * delta;
                flake.Top += move;
                flake.Left += move * WindEffect;

                // Если снежинка ушла за экран — появится снова сверху
                double sz = flake.Size;
                flake.Top = Reposition(flake.Top, -sz, screenHeight + sz);
                flake.Left = Reposition(flake.Left, -sz, screenWidth + sz);

                // Обновляю элемент массива
                allFlakes[i] = flake;
            }
        }

        /// <summary>
        /// Перебор для foreach — возвращаю весь массив снежинок.
        /// </summary>
        public IEnumerator<Snowflake> GetEnumerator() => ((IEnumerable<Snowflake>)allFlakes).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Здесь я создаю снежинку с новыми параметрами.
        /// </summary>
        private void CreateFlake(int index)
        {
            // Случайный разброс скорости (чуть быстрее или медленнее среднего)
            double speedOffset = 1 + SpeedVariance * (randomizer.NextDouble() * 2 - 1);

            // Определяю "слой" — влияет и на размер, и на скорость
            double layer = MinLayer + (MaxLayer - MinLayer) * randomizer.NextDouble();

            // Чем ближе слой — тем крупнее и быстрее
            double finalSpeed = (DefaultSpeed / layer) * speedOffset;
            double finalSize = DefaultSize / layer;

            // Рандомно размещаю снежинку в пределах экрана
            double posX = randomizer.NextDouble() * screenWidth;
            double posY = randomizer.NextDouble() * screenHeight;

            allFlakes[index].Speed = finalSpeed;
            allFlakes[index].Size = finalSize;
            allFlakes[index].Left = posX;
            allFlakes[index].Top = posY;
        }

        /// <summary>
        /// Здесь я возвращаю значение обратно в диапазон, если снежинка вышла за экран.
        /// </summary>
        private static double Reposition(double value, double min, double max)
        {
            double range = max - min;
            // Делаю обёртку, чтобы координаты "перетекали" через границы
            return value - range * Math.Floor((value - min) / range);
        }
    }
}
