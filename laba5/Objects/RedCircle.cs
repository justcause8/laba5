using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace laba5.Objects
{
    // Класс представляет красный круг, который увеличивается по размеру при каждом обновлении
    class RedCircle : BaseObject
    {
        private float size = 20; // Начальный размер круга
        private const float maxSize = 500; // Максимальный размер круга

        // Начальный, максимальный и текущий размеры круга
        public float InitialR { get; private set; }
        public float MaxR { get; private set; }
        public float CurrentR { get; private set; }
        private const float GrowthRate = 0.5f; // скорость увеличения размера

        // Конструктор класса, инициализирующий начальные параметры круга
        public RedCircle(float x, float y, float angle) : base(x, y, angle)
        {
            InitialR = size;
            MaxR = maxSize;
            CurrentR = InitialR;
        }

        // Метод сбрасывает текущий размер и перемещает круг в случайное место на экране
        public void Reset()
        {
            CurrentR = InitialR;
            Random rand = new Random();
            X = rand.Next(15, 285);  // 300 - диаметр круга
            Y = rand.Next(15, 285);  // 300 - диаметр круга
        }

        // Метод отрисовывает круг
        public override void Render(Graphics g)
        {
            // Устанавливаем прозрачность цвета
            Color redColor = Color.FromArgb(128, Color.Red); // Установка альфа-канала на половину (128)
            g.FillEllipse(new SolidBrush(redColor), -CurrentR, -CurrentR, CurrentR * 2, CurrentR * 2);
        }

        // Метод возвращает графический путь для объекта круга
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-CurrentR, -CurrentR, CurrentR * 2, CurrentR * 2);
            return path;
        }

        // Метод вызывается при пересечении круга с другим объектом
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            // Если пересечение произошло с игроком и текущий размер больше начального, сбросить размер и переместить круг
            if (obj is Player)
            {
                if (CurrentR > InitialR)
                {
                    CurrentR = InitialR; // сброс размера
                    Random rand = new Random();
                    X = rand.Next(15, 285);  // 300 - диаметр круга
                    Y = rand.Next(15, 285);  // 300 - диаметр круга
                }
            }
        }

        // Метод обновляет размер круга, увеличивая его на GrowthRate
        public void UpdateSize()
        {
            if (CurrentR < MaxR)
            {
                CurrentR += GrowthRate;
            }
        }
    }
}
