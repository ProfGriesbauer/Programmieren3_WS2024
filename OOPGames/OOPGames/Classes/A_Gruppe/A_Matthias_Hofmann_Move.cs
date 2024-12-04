using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPGames
{
    public class A_Move : IRowMove, IColumnMove
    {
        int _actualRow = 0;
        int _actualColumn = 0;
        int _actualPlayerNumber = 0;

        public A_Move (int actualRow, int actualColumn, int actualPlayerNumber)
        {
            _actualRow = actualRow;
            _actualColumn = actualColumn;
            _actualPlayerNumber = actualPlayerNumber;
        }

        public int PlayerNumber {  get { return _actualPlayerNumber; } }

        public int Row {  get { return _actualRow; } }

        public int Column {  get { return _actualColumn; } }
    }
}
