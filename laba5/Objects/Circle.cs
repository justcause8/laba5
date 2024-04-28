using laba5.Objects;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace laba5.Objects
{
    // Класс, представляющий круговой объект
    class Circle : BaseObject
    {
        private bool wasDecreased = false; // Флаг, указывающий, был ли круг уменьшен
        public float Size { get; private set; } // Размер круга


        public Circle(float x, float y, float angle) : base(x, y, angle)
        {
            Size = 40;
        }

        // Метод для отрисовки круга
        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -Size / 2, -Size / 2, Size, Size);
            g.DrawEllipse(new Pen(Color.Green, 2), -Size / 2, -Size / 2, Size, Size);
        }

        // Метод для получения графического пути круга
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-Size / 2, -Size / 2, Size, Size);
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
                X = rand.Next(20, 300 - 30);
                Y = rand.Next(0, 300 - 30);

                // Установка размера круга больше на 10
                Size = 50;
            }
        }

        // Метод для уменьшения размера круга на заданную величину
        public void DecreaseSize(float amount)
        {
            Size -= amount;
            if (Size <= 0)
            {
                // Генерация случайных координат для нового местоположения круга
                Random rand = new Random();
                X = rand.Next(20, 300 - 30);
                Y = rand.Next(0, 300 - 30);

                // Возвращение начального размера
                Size = 30;
                wasDecreased = false;
            }
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
