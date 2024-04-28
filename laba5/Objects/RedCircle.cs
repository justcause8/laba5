using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.Objects
{
    class RedCircle : BaseObject
     {
        private float size = 20; // Начальный размер круга
        private const float maxSize = 500; // Максимальный размер круга

        public float InitialR { get; private set; }
        public float MaxR { get; private set; }
        public float CurrentR { get; private set; }
        private const float GrowthRate = 0.5f; // скорость увеличения размера

        public RedCircle(float x, float y, float angle) : base(x, y, angle)
        {
            InitialR = size;
            MaxR = maxSize;
            CurrentR = InitialR;
        }

        public void Reset()
        {
            CurrentR = InitialR;
            Random rand = new Random();
            X = rand.Next(15, 285);  // 300 - диаметр круга
            Y = rand.Next(15, 285);  // 300 - диаметр круга
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), -CurrentR, -CurrentR, CurrentR * 2, CurrentR * 2);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-CurrentR, -CurrentR, CurrentR * 2, CurrentR * 2);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

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

        public void UpdateSize()
        {
            if (CurrentR < MaxR)
            {
                CurrentR += GrowthRate;
            }
        }
    }
}
