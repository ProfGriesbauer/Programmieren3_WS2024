using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    internal class B_Field_BV : IB_Field_BV
    {
        IB_Rules_BV _Rules;
        public B_Field_BV(IB_Rules_BV rules)
        {
            _Rules = rules;
        }
        public double Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public double MarginPercentage => throw new NotImplementedException();

        public IB_Ball_BV Ball { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IB_Player_BV Player1 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IB_Player_BV Player2 { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IB_Rules_BV Rules_BV => throw new NotImplementedException();

        public bool CanBePaintedBy(IPaintGame painter)
        {
            throw new NotImplementedException();
        }
    }
}
