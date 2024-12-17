using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class B_ComputerPlayer_BV : B_Player_BV, IB_ComputerPlayer_BV
    {
        public override string Name
        {
            get
            {
                return "Blobby Computer Player";
            }
        }

        public override IGamePlayer Clone()
        {
            B_ComputerPlayer_BV computerPlayer = new B_ComputerPlayer_BV();
            computerPlayer.SetPlayerNumber(this.PlayerNumber);
            return computerPlayer;
        }

        public IPlayMove GetMove(IGameField field)
        {
            if (field is IB_Field_BV)
            {
                return GetMoveBV((IB_Field_BV)field);
            }
            else
            {
                return null;
            }
        }
        public IPlayMove GetTickMove(IGameField field)
        {
            if (field is IB_Field_BV)
            {
                return GetTickMoveBV((IB_Field_BV)field);
            }
            else
            {
                return null;
            }
        }
        public IB_Move_BV GetMoveBV(IB_Field_BV field) { return null; }
        public double LandingPosition(double x0, double y0, double vx, double vy, double y1)
        {
     

            // Zeit berechnen, bis der Ball den Boden erreicht
            double t = Math.Abs((y0 - y1) / vy);

            // x-Koordinate berechnen
            double _xLanding = x0 + vx * t;

            return _xLanding;
        }
        public IB_Move_BV GetTickMoveBV(IB_Field_BV field)
        {
            bool _MoveLeft = false;
            bool _MoveRight = false;
            bool _Jump = false;
            // Spielerpositionen
            double _playerX = Pos_x;
            double _playerY = Pos_y;
            double _Velo_x = field.Ball.Velo_x;
            double _Velo_y = field.Ball.Velo_y;

            // Ballposition
            double _ballX = field.Ball.Pos_x;
            double _ballY = field.Ball.Pos_y;

            // Spielfeldbreite
            double _fieldWidth = field.Width;
            

            if (field.Player[this.PlayerNumber - 1] == null)
            {
                field.Player[this.PlayerNumber - 1] = this;
            }

            if (this.PlayerNumber == 1 && _ballX <= _fieldWidth / 2)
            {
        if (LandingPosition(_ballX, _ballY, _Velo_x, _Velo_y, field.Height/4) > _playerX) // Ball rechts vom Spieler
                {
                    _MoveRight = true;
                }
                else if (LandingPosition(_ballX, _ballY, _Velo_x, _Velo_y, field.Height / 4) < _playerX || _ballX == _playerX) // Ball links vom Spieler
                {
                    _MoveLeft = true;
                }
            }
            else if (this.PlayerNumber == 2 && _ballX >= _fieldWidth/2)
            {
                if (LandingPosition(_ballX, _ballY, _Velo_x, _Velo_y, field.Height / 4) < _playerX) // Ball links vom Spieler
                {
                    _MoveLeft = true;
                }
                else if (LandingPosition(_ballX, _ballY, _Velo_x, _Velo_y, field.Height / 4) > _playerX || _ballX == _playerX) // Ball rechts vom Spieler
                {
                    _MoveRight = true;
                }
            }
            Random random = new Random();
            if (random.NextDouble() < 0.05) // 5% Wahrscheinlichkeit, nichts zu tun
            {
                _MoveLeft = false;
                _MoveRight = false;
            }




            // Logik für das Springen:
            // Springe nur, wenn der Ball über dem Spieler ist und er in Reichweite ist
            if (Math.Abs(field.Ball.Pos_x - Pos_x) < 50 && field.Ball.Pos_y < Pos_y)
            {
                _Jump = true;
            }

            return new B_Move_BV(this.PlayerNumber, _MoveLeft, _MoveRight, _Jump);
        }
    }
}
