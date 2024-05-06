using System.Drawing.Drawing2D;

namespace laba5.Objects
{
    // Класс, представляющий кружок
    class Circle : BaseObject
    {
        private bool wasDecreased = false; // Флаг, указывающий, был ли круг уменьшен
        public int leftTime; // Переменная для отслеживания времени
        public const int maxTime = 300; // Максимальное время
        private Random rnd = new Random();

        public Circle(float x, float y, float angle) : base(x, y, angle)
        {
        }

        // Метод для отрисовки круга
        public override void Render(Graphics g)
        {
            
            float size = 50 * ((float)leftTime / maxTime);// Вычисляем текущий размер круга

            // Отрисовываем круг
            g.FillEllipse(new SolidBrush(Color.Green), -size / 2, -size / 2, size, size);
            g.DrawEllipse(new Pen(Color.Green, 2), -size / 2, -size / 2, size, size);

            // Отображаем оставшееся время
            g.DrawString($"{leftTime}", new Font("Arial", 7), new SolidBrush(Color.Black), 25, 15);
        }


        // Метод для получения графического пути круга
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            float circleSize = 20 * ((float)leftTime / maxTime); // Вычисляем текущий размер круга
            path.AddEllipse(new RectangleF(-circleSize / 2, -circleSize / 2, circleSize, circleSize));
            return path;
        }

        // Метод, вызываемый при пересечении с другим объектом
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Player)
            {
                // Генерация случайных координат для нового местоположения круга
                Random rand = new Random();
                X = rand.Next(20, 300);
                Y = rand.Next(0, 300);
                ResetTimer();
            }
        }

        // Отсчет времени
        public void Tick()
        {
            leftTime--;
            if (leftTime <= 0)
            {
                // Перерождение кругов
                X = rnd.Next(0, 700);
                Y = rnd.Next(100, 350);
                ResetTimer(); // Перезапуск таймера
            }
        }

        //Рестарт времени
        private void ResetTimer()
        {
            leftTime = maxTime;
        }

        // Метод для проверки был ли круг уменьшен
        public bool WasDecreased()
        {
            return wasDecreased;
        }

        // Метод для сброса флага уменьшения круга
        public void ResetWasDecreased()
        {
            wasDecreased = false;
        }
    }
}