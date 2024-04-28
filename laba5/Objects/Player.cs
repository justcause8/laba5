using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba5.Objects
{
    // Класс, представляющий игрока
    class Player : BaseObject
    {
        // Событие, которое срабатывает при пересечении с маркером
        public Action<Marker> OnMarkerOverlap;

        // Компоненты скорости игрока по осям X и Y
        public float vX, vY;

        // Конструктор класса Player
        public Player(float x, float y, float angle) : base(x, y, angle)
        {

        }

        // Метод для отрисовки игрока
        public override void Render(Graphics g)
        {
            // Отрисовка тела игрока (круга)
            g.FillEllipse(new SolidBrush(Color.DeepSkyBlue), -15, -15, 30, 30);
            g.DrawEllipse(new Pen(Color.Black, 2), -15, -15, 30, 30);
            // Отрисовка направляющей линии игрока
            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 30, 0);
        }

        // Метод для получения графического пути игрока
        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-15, -15, 30, 30);
            return path;
        }

        // Метод для обработки пересечений игрока с другими объектами
        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);

            // Если пересечение произошло с маркером
            if (obj is Marker)
            {
                // Вызываем событие OnMarkerOverlap и передаем в него маркер
                OnMarkerOverlap(obj as Marker);
            }
        }
    }
}
