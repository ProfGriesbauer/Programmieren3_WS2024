using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
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
        public double Playersize { get; set; } = 85;

        public bool IsOnGround { get; set; }
        bool _Squished = false;
        int _waitTime = 0;

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
            if (field.FieldStyle == 2)
            {
                _groundLevel -= 0.05 * field.Height;
            }
            if (Pos_y + Playersize / 2 > _groundLevel)
            {
                Pos_y = _groundLevel - Playersize / 2;
                Velo_y = 0;
                IsOnGround = true;
            }
            else
            {
                IsOnGround = false;
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

        public void B_Paint_Player(Canvas canvas, int fieldStyle)
        {
            switch (fieldStyle)
            {
                case 0:
                    Ellipse player = new Ellipse
                    {
                        Width = Playersize,
                        Height = Playersize,
                        Fill = Brushes.Blue
                    };
                    Canvas.SetLeft(player, Pos_x - Playersize / 2);
                    Canvas.SetTop(player, Pos_y - Playersize / 2);
                    canvas.Children.Add(player);
                    break;
                case 1: //Image nicht vorhanden -> wie Style 0
                    Ellipse player_img = new Ellipse
                    {
                        Width = Playersize,
                        Height = Playersize,
                        Fill = Brushes.Blue
                    };
                    Canvas.SetLeft(player_img, Pos_x - Playersize / 2);
                    Canvas.SetTop(player_img, Pos_y - Playersize / 2);
                    canvas.Children.Add(player_img);
                    break;
                case 2:
                    var player_drw = new Image
                    {
                        Stretch = Stretch.Fill
                    };

                    //Set the correct Color for the Player
                    if (PlayerNumber == 1)
                    {
                        player_drw.Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Player_Red.PNG", UriKind.Relative));
                    }
                    else if (PlayerNumber == 2)
                    {
                        player_drw.Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Player_Blue.PNG", UriKind.Relative));
                    }

                    // Squish the Player when moving on Ground or jumping
                    _waitTime++;

                    if (IsOnGround && Velo_x != 0 && !_Squished || IsOnGround && Velo_y != 0 && !_Squished)
                    {
                        player_drw.Width = Playersize * 1.4;
                        player_drw.Height = Playersize * 1.2;
                        Canvas.SetTop(player_drw, Pos_y - Playersize / 2 + Playersize * 0.3);
                        if (_waitTime > 4)
                        {
                            _Squished = true;
                            _waitTime = 0;
                        }
                    }
                    else
                    {
                        player_drw.Width = Playersize * 1.2;
                        player_drw.Height = Playersize * 1.5;
                        Canvas.SetTop(player_drw, Pos_y - Playersize / 2);
                        if (_waitTime > 4)
                        {
                            _Squished = false;
                            _waitTime = 0;
                        }
                    }

                    Canvas.SetLeft(player_drw, Pos_x - Playersize / 2);
                    canvas.Children.Add(player_drw);
                    break;
            }

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
