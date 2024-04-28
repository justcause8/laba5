using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.Objects
{
    // Класс, представляющий маркер на экране
    class Marker : BaseObject
    {
        // Конструктор класса Marker
        public Marker(float x, float y, float angle) : base(x, y, angle)
        {

        }

        // Метод отрисовки маркера
        public override void Render(Graphics g)
        {
            // Отрисовка маркера: красный круг с двумя кольцами
            g.FillEllipse(new SolidBrush(Color.Red), -3, -3, 6, 6);
            g.DrawEllipse(new Pen(Color.Red, 2), -6, -6, 12, 12);
            g.DrawEllipse(new Pen(Color.Red, 2), -10, -10, 20, 20);
        }

        // Метод для получения графического пути объекта
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            // Добавляем эллипс в графический путь
            path.AddEllipse(-3, -3, 6, 6);
            return path;
        }
    }
}
