using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace laba5.Objects
{
    // Базовый класс для всех объектов в игре
    class BaseObject
    {
        // Поля объекта: координаты, угол поворота и радиус
        public float X;
        public float Y;
        public float Angle;
        public float R;

        // Таймер объекта
        public float timer = 60;

        // Событие, срабатывающее при пересечении с другим объектом
        public Action<BaseObject, BaseObject> OnOverlap;

        // Конструктор по умолчанию
        public BaseObject() { }

        // Конструктор с параметрами
        public BaseObject(float x, float y, float angle)
        {
            X = x;
            Y = y;
            Angle = angle;
        }

        // Метод для получения матрицы преобразования
        public Matrix GetTransform()
        {
            var matrix = new Matrix();
            matrix.Translate(X, Y);
            matrix.Rotate(Angle);

            return matrix;
        }

        // Виртуальный метод для отрисовки объекта
        public virtual void Render(Graphics g)
        {
        }

        // Виртуальный метод для получения графического пути объекта
        public virtual GraphicsPath GetGraphicsPath()
        {
            return new GraphicsPath();
        }

        // Виртуальный метод для проверки пересечения с другим объектом
        public virtual bool Overlaps(BaseObject obj, Graphics g)
        {
            // Получаем графические пути для текущего объекта и переданного объекта
            var path1 = this.GetGraphicsPath();
            var path2 = obj.GetGraphicsPath();

            // Трансформируем пути с использованием матрицы преобразования каждого объекта
            path1.Transform(this.GetTransform());
            path2.Transform(obj.GetTransform());

            // Создаем регион, представляющий область пересечения путей
            var region = new Region(path1);
            region.Intersect(path2);

            // Проверяем, пуст ли регион пересечения. Если не пустой, значит, объекты пересекаются
            return !region.IsEmpty(g);
        }

        // Виртуальный метод для обработки пересечения с другим объектом
        public virtual void Overlap(BaseObject obj)
        {
            // Проверяем, установлено ли событие обработки пересечения
            if (this.OnOverlap != null)
            {
                // Вызываем событие, передавая в качестве аргументов текущий объект и объект, с которым он пересекся
                this.OnOverlap(this, obj);
            }
        }


    }
}
