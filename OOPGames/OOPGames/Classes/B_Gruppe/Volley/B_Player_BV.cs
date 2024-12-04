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
        int _PlayerNumber;
        public int PlayerNumber
        {
            get
            {
                return _PlayerNumber;
            }
        }
        public double Pos_x { get; set; }
        public double Pos_y { get; set; }
        public double Velo_x { get; set; }
        public double Velo_y { get; set; }
        public double Playersize { get; set; } = 75;

        public abstract string Name
        {
            get;
        }

        public void B_Move_Player(IB_Field_BV field)
        {
            double _groundLevel = field.Height - field.Ground.Height;
            //Decrease Velo_y for Gravity
            Velo_y += 0.007 * field.Height;

            // Update player position based on velocity
            Pos_x += Velo_x;
            Pos_y += Velo_y;

            // Keep player within boundaries
            // Keep above Ground
            if (Pos_y + Playersize / 2 > _groundLevel)
            {
                Pos_y = _groundLevel - Playersize / 2;
                Velo_y = 0;
            }
            //Keep in Playfield and on the correct side
            if (PlayerNumber == 1)
            {
                if (Pos_x - Playersize / 2 < 0)
                {
                    Pos_x = Playersize / 2;
                    Velo_x = 0;
                }
                if (Pos_x + Playersize / 2 > field.Width / 2 - field.Net.Width / 2)
                {
                    Pos_x = field.Width / 2 - field.Net.Width / 2 - Playersize / 2;
                    Velo_x = 0;
                }
            }
            if (PlayerNumber == 2)
            {
                if (Pos_x + Playersize / 2 > field.Width)
                {
                    Pos_x = field.Width - Playersize / 2;
                    Velo_x = 0;
                }
                if (Pos_x - Playersize / 2 < field.Width / 2 + field.Net.Width / 2)
                {
                    Pos_x = field.Width / 2 + field.Net.Width / 2 + Playersize / 2;
                    Velo_x = 0;
                }
            }
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
