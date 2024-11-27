using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class BV_Ball : IB_Ball_BV
    {
        public double Pos_x { get; set; }
        public double Pos_y { get; set; }
        public double Velo_x { get; set; }
        public double Velo_y { get; set; }
        public double Ballsize { get; set; } = 20; // Default ball size

        // Schwerkraftstärke
        private const double Gravity = 0.5; // Beschleunigung nach unten pro Frame
        private const double BounceDamping = 0.8; // Faktor, um Energieverlust beim Aufprall zu simulieren

        public Canvas B_Paint_Ball(Canvas canvas)
        {
            Ellipse ball = new Ellipse
            {
                Width = Ballsize,
                Height = Ballsize,
                Fill = Brushes.Red
            };
            Canvas.SetLeft(ball, Pos_x - Ballsize / 2);
            Canvas.SetTop(ball, Pos_y - Ballsize / 2);
            canvas.Children.Add(ball);
            return canvas;
        }

        public void B_Move_Ball(IB_Field_BV field)
        {
            // Anwenden der Schwerkraft
            Velo_y += Gravity;

            // Aktualisieren der Position basierend auf der Geschwindigkeit
            Pos_x += Velo_x;
            Pos_y += Velo_y;

            // Kollision mit Decke
            if (Pos_y <= Ballsize / 2 || Pos_y >= field.Height - Ballsize / 2)
            { 
                Pos_y = Ballsize / 2;
                Velo_y = -Velo_y; // Geschwindigkeit umkehren
            }

            // Kollision mit den Wänden (links und rechts)
            if (Pos_x <= Ballsize / 2 || Pos_x >= field.Width - Ballsize / 2) // Assuming 1000 as canvas width
            {
                Velo_x = -Velo_x; // Geschwindigkeit umkehren
            }
        }

        public int B_On_Ground()
        {
            if (Pos_y > Ballsize / 2) return 1; // Bottom collision left
            if (Pos_y > Ballsize / 2) return 0; // Bottom collision right
            return -1; //no collision

        }
        public void HandlePlayerCollision(IB_Player_BV player)
        {
            double dx = Pos_x - player.Pos_x;
            double dy = Pos_y - player.Pos_y;
            double distance = Math.Sqrt(dx * dx + dy * dy);

            // Check collision
            if (distance <= Ballsize / 2 + player.Playersize / 2)
            {
                // Ball zurückstoßen basierend auf der Kollisionsrichtung
                Velo_x = dx > 0 ? Math.Abs(Velo_x) : -Math.Abs(Velo_x);
                Velo_y = dy > 0 ? Math.Abs(Velo_y) : -Math.Abs(Velo_y);

                // Leichte Abstoßung
                Pos_x += Velo_x * 0.1;
                Pos_y += Velo_y * 0.1;
            }
        }
    }
}
