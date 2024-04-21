﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.Objects
{
    class Circle : BaseObject
    {
        public bool IsVisible { get; private set; }

        public Circle(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Green), -20, -20, 30, 30);
            g.DrawEllipse(new Pen(Color.Green, 2), -20, -20, 30, 30);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-20, -20, 40, 40);
            return path;
        }
    }
}