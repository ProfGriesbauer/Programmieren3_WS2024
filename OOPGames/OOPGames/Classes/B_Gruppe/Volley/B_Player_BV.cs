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
    public abstract class B_Player_BV : IB_Player_BV
    {
        int _PlayerNumber = 0;
        public double Pos_x { get; set; }
        public double Pos_y { get; set; }
        public double Velo_x { get; set; }
        public double Velo_y { get; set; }
        public double Playersize { get; set; } = 50;

        public abstract string Name
        {
            get;
        }

        public int PlayerNumber
        {
            get
            {
                return _PlayerNumber;
            }
        }

        public void B_Move_Player(IB_Field_BV field)
        {
            // Update player position based on velocity
            Pos_x += Velo_x;
            Pos_y += Velo_y;


            // Keep player within boundaries
            if (Pos_x < Playersize / 2) Pos_x = Playersize / 2;
            if (Pos_x > field.Width - Playersize / 2) Pos_x = field.Width - Playersize / 2; // Assuming 1000 as canvas width
            if (Pos_y > field.Height - Playersize / 2) Pos_y = field.Height - Playersize / 2; // Assuming 500 as canvas height
        }


        public Canvas B_Paint_Player(Canvas canvas)
        {
            Ellipse player = new Ellipse
            {
                Width = Playersize,
                Height = Playersize,
                Fill = Brushes.Blue
            };
            Canvas.SetLeft(player, Pos_x - Playersize / 2);
            Canvas.SetTop(player, Pos_y - Playersize / 2);
            canvas.Children.Add(player);
            return canvas;
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is IB_Rules_BV;
        }

        public abstract IGamePlayer Clone();
        

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }
}
