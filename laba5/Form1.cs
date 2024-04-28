using laba5.Objects;
using System.Windows.Forms;

namespace laba5
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new();
        Player player;
        Marker marker;
        int counter = 0;

        public Form1()
        {
            InitializeComponent();
            UpdateScoreText();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);

            // добавл€ю реакцию на пересечение
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] »грок пересекс€ с {obj}\n" + txtLog.Text;
            };

            // добавил реакцию на пересечение с маркером
            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);

            objects.Add(marker);
            objects.Add(player);

            objects.Add(new Circle(200, 200, 0));
            objects.Add(new Circle(100, 250, 0));

            objects.Add(new RedCircle(150, 150, 0));
        }


        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            // ќбновл€ем размер красного круга
            foreach (var obj in objects)
            {
                if (obj is RedCircle redCircle)
                {
                    redCircle.UpdateSize();
                }
            }

            updatePlayer();

            // пересчитываем пересечени€ и уменьшаем размер круга
            foreach (var obj in objects.ToList())
            {
                if (obj is Circle circle)
                {
                    if (!circle.WasDecreased())
                    {
                        circle.DecreaseSize(0.2f); // ”меньшаем размер на 0.5 единиц при каждом обновлении
                    }
                    else
                    {
                        circle.ResetWasDecreased(); // —брасываем флаг уменьшени€
                    }
                }

                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);

                    if (obj is Circle)
                    {
                        counter++;
                        UpdateScoreText();
                    }
                    else if (obj is RedCircle)
                    {
                        if (counter > 0)
                        {
                            counter--;
                            UpdateScoreText();
                        }
                        ((RedCircle)obj).Reset();
                    }
                }
            }

            // рендерим объекты
            foreach (var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }


        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;

                player.vX += dx * 0.7f;
                player.vY += dy * 0.7f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;

            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            player.X += player.vX;
            player.Y += player.vY;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            // а это так и остаетс€
            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void UpdateScoreText()
        {
            txtCount.Clear(); // очистка содержимого txtCount
            txtCount.AppendText($"—чет: {counter}\n"); // добавление текста с количеством очков
        }
    }
}
