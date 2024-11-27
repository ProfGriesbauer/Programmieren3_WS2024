using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public class B_Rules_BV : IB_Rules_BV
    {
        B_Field_BV _Field;

        public B_Rules_BV()
        {
            _Field = new B_Field_BV(this);
        }
        public string Name => "Blobby Volley Rules";
        public int[,] Points { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IB_Field_BV Field_BV => throw new NotImplementedException();


        public IGameField CurrentField => throw new NotImplementedException();

        public bool MovesPossible => throw new NotImplementedException();

        public int CheckIfPLayerWon()
        {
            throw new NotImplementedException();
        }

        public int CheckIfPLayerWon_Volley(int[,] points)
        {
            throw new NotImplementedException();
        }

        public void ClearField()
        {
            throw new NotImplementedException();
        }

        public void DoMove(IPlayMove move)
        {
            throw new NotImplementedException();
        }

        public void StartedGameCall()
        {
            throw new NotImplementedException();
        }

        public void TickGameCall()
        {
            throw new NotImplementedException();
        }
    }
}
