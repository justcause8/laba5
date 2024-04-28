using laba5.Objects;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace laba5.Objects
{
    class Circle : BaseObject
    {
        public bool IsVisible { get; private set; }
        public float Size { get; private set; } // Размер круга
        private bool wasDecreased = false; // Флаг, указывающий, был ли круг уменьшен


        public Circle(float x, float y, float angle) : base(x, y, angle)
        {
            Size = 40; // Изначальный размер круга
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -Size / 2, -Size / 2, Size, Size); // Используем Size для отрисовки круга
            g.DrawEllipse(new Pen(Color.Green, 2), -Size / 2, -Size / 2, Size, Size);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-Size / 2, -Size / 2, Size, Size); // Используем Size для построения пути
            return path;
        }
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            if (obj is Player)
            {
                Random rand = new Random();
                X = rand.Next(20, 300 - 30);  // Ширина экрана минус размер объекта
                Y = rand.Next(0, 300 - 30);  // Высота экрана минус размер объекта
                Size = 50; // Установим размер круга больше на 10
            }
        }


        public void DecreaseSize(float amount)
        {
            Size -= amount; // Уменьшаем размер на заданную величину
            if (Size <= 0) // Если размер становится нулевым или отрицательным
            {
                Random rand = new Random();
                X = rand.Next(20, 300 - 30);  // Ширина экрана минус размер объекта
                Y = rand.Next(0, 300 - 30);  // Высота экрана минус размер объекта
                Size = 30; // Возвращаем начальный размер
                wasDecreased = false; // Сбрасываем флаг
            }
        }

        public bool WasDecreased()
        {
            return wasDecreased;
        }

        public void ResetWasDecreased()
        {
            wasDecreased = false; // Сбрасываем флаг
        }
    }
}