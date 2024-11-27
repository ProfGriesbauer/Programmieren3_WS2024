using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OOPGames
{
    public class B_HumanPlayer_BV : IB_Player_BV
    {
        public string Name => "Blobby Volley Player";
        public double Pos_x { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Pos_y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Velo_x { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Velo_y { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Playersize { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int PlayerNumber => throw new NotImplementedException();

        public void B_Move_Player()
        {
            throw new NotImplementedException();
        }

        public Canvas B_Paint_Player(Canvas canvas)
        {
            throw new NotImplementedException();
        }

        public bool CanBeRuledBy(IGameRules rules)
        {
            throw new NotImplementedException();
        }

        public IGamePlayer Clone()
        {
            throw new NotImplementedException();
        }

        public void SetPlayerNumber(int playerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
