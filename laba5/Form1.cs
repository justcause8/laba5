using laba5.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace laba5
{
    public partial class Form1 : Form
    {
        // Список объектов на экране
        List<BaseObject> objects = new List<BaseObject>();
        Player player;
        Marker marker;
        int counter = 0;

        // Конструктор формы
        public Form1()
        {
            InitializeComponent();
            UpdateScoreText(); // Обновление отображения счета

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);// Инициализация игрока

            // Добавление реакции на пересечение игрока с объектом
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            // Добавление реакции на пересечение игрока с маркером
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m); // Удаление маркера из списка объектов
                marker = null; // Установка маркера в null
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0); // Инициализация маркера

            // Добавление маркера и игрока в список объектов
            objects.Add(marker);
            objects.Add(player);

            // Добавление кругов на экран
            objects.Add(new Circle(200, 200, 0) { leftTime = maxTime });
            objects.Add(new Circle(100, 250, 0) { leftTime = maxTime });
            objects.Add(new Circle(300, 250, 0) { leftTime = maxTime });


            // Добавление красного круга на экран
            objects.Add(new RedCircle(150, 150, 0));
        }

        // Отсчет времени
        private const int maxTime = 450;

        // Метод для отрисовки на главном холсте
        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White); // Очистка холста

            // Обновление размера красного круга
            foreach (var obj in objects)
            {
                if (obj is RedCircle redCircle)
                {
                    redCircle.UpdateSize();
                }
            }

            updatePlayer(); // Обновление положения игрока

            // Пересчитываем пересечения и уменьшаем оставшееся время у кругов
            foreach (var obj in objects.ToList())
            {
                if (obj is Circle circle)
                {
                    circle.Tick(); // Обновление времени для круга
                    if (!circle.WasDecreased())
                    {
                        circle.leftTime -= 1; // Уменьшаем оставшееся время у круга
                    }
                    else
                    {
                        circle.ResetWasDecreased(); // Сброс флага уменьшения
                    }
                }

                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);

                    if (obj is Circle)
                    {
                        counter++; // Увеличение счетчика очков
                        UpdateScoreText(); // Обновление отображения счета
                    }
                    else if (obj is RedCircle)
                    {
                        if (counter > 0)
                        {
                            counter--; // Уменьшение счетчика очков
                            UpdateScoreText(); // Обновление отображения счета
                        }
                        ((RedCircle)obj).Reset(); // Сброс красного круга
                    }
                }
            }

            // Рендеринг объектов
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        // Метод для обновления положения игрока
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.8f;
                player.vY += dy * 0.8f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        // Метод для обработки события таймера
        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate(); // Перерисовка главного холста
        }

        // Метод для обработки события клика мыши на главном холсте
        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker); // Добавление маркера в список объектов
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        // Метод для обновления отображения счета
        private void UpdateScoreText()
        {
            txtCount.Clear(); // Очистка содержимого счетчика
            txtCount.AppendText($"Счет: {counter}\n"); // Добавление текста с текущим счетом
        }
    }
}