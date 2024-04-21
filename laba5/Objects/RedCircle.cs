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
        public float Radius { get; private set; }
        private const float GrowthRate = 1.1f;
        private const float ResetRadius = 20;

        public RedCircle(float x, float y, float angle, float radius) : base(x, y, angle)
        {
            Radius = radius;
        }

        public void Reset()
        {
            Radius = ResetRadius;
            Random rand = new Random();
            X = rand.Next(15, 285);  // 300 - диаметр круга
            Y = rand.Next(15, 285);  // 300 - диаметр круга
        }

        public void Grow()
        {
            Radius *= GrowthRate;
        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), -Radius, -Radius, 2 * Radius, 2 * Radius);
            g.DrawEllipse(new Pen(Color.Black, 2), -Radius, -Radius, 2 * Radius, 2 * Radius);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-Radius, -Radius, 2 * Radius, 2 * Radius);
            return path;
        }
    }


    /*class RedCircle : BaseObject
    {
        public float InitialR { get; private set; }
        public float CurrentR { get; private set; }

        public RedCircle(float x, float y, float angle, float r) : base(x, y, angle)
        {
            InitialR = r;
            CurrentR = r;
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
    }*/
}
