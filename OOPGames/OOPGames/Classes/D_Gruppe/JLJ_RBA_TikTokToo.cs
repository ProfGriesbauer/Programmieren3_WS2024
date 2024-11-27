using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class D_PainterTikTokToo : ID_PaintTTT
    {
        public string Name { get { return "JLJs_TTTPainter"; } }

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is ID_TTTGameField)
            {
                PaintTTTField(canvas, (ID_TTTGameField)currentField);
            }
        }

        public void PaintTTTField(Canvas canvas, ID_TTTGameField currentField)
        {
            IDrawableSymbol Symbol1 = new D_SmileySymbol();
            IDrawableSymbol Symbol2 = new D_SkullSymbol();

            // Paint funktionen 
            canvas.Children.Clear();
            Color bgColor = Color.FromRgb(255, 255, 255);
            canvas.Background = new SolidColorBrush(bgColor);
            Color lineColor = Color.FromRgb(0, 0, 0);
            Brush lineStroke = new SolidColorBrush(lineColor);
            

            Line l1 = new Line() { X1 = 120, Y1 = 20, X2 = 120, Y2 = 320, Stroke = lineStroke, StrokeThickness = 5.0 };
            canvas.Children.Add(l1);
            Line l2 = new Line() { X1 = 220, Y1 = 20, X2 = 220, Y2 = 320, Stroke = lineStroke, StrokeThickness = 5.0 };
            canvas.Children.Add(l2);
            Line l3 = new Line() { X1 = 20, Y1 = 120, X2 = 320, Y2 = 120, Stroke = lineStroke, StrokeThickness = 5.0 };
            canvas.Children.Add(l3);
            Line l4 = new Line() { X1 = 20, Y1 = 220, X2 = 320, Y2 = 220, Stroke = lineStroke, StrokeThickness = 5.0 };
            canvas.Children.Add(l4);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int x = 20 + (j * 100);
                    int y = 20 + (i * 100);

                    if (currentField[i, j] == 1)
                    {
                        Symbol1.Draw(canvas, x, y);
                    }
                    else if (currentField[i, j] == 2)
                    {
                        Symbol2.Draw(canvas, x, y);
                    }
                }
            }

        }

    }
    public class D_XSymbol : IDrawableSymbol
    {
        public void Draw(Canvas canvas, int x, int y)
        {
            Color XColor = Color.FromRgb(0, 255, 0);
            Brush XStroke = new SolidColorBrush(XColor);

            Line X1 = new Line() { X1 = x, Y1 = y, X2 = x + 100, Y2 = y + 100, Stroke = XStroke, StrokeThickness = 3.0 };
            Line X2 = new Line() { X1 = x, Y1 = y + 100, X2 = x + 100, Y2 = y, Stroke = XStroke, StrokeThickness = 3.0 };

            canvas.Children.Add(X1);
            canvas.Children.Add(X2);
        }
    }

    public class D_OSymbol : IDrawableSymbol
    {
        public void Draw(Canvas canvas, int x, int y)
        {
            Color OColor = Color.FromRgb(0, 0, 255);
            Brush OStroke = new SolidColorBrush(OColor);

            Ellipse OE = new Ellipse() { Margin = new Thickness(x, y, 0, 0), Width = 100, Height = 100, Stroke = OStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(OE);
        }
    }

    public class D_SmileySymbol : IDrawableSymbol
    {
        public void Draw(Canvas canvas, int x, int y)
        {
            // Farben für das Smiley-Gesicht
            Color faceColor = Color.FromRgb(255, 223, 0); // Gelbes Gesicht
            Color featureColor = Color.FromRgb(0, 0, 0);  // Schwarze Augen und Mund
            Brush faceBrush = new SolidColorBrush(faceColor);
            Brush featureBrush = new SolidColorBrush(featureColor);

            // Gesicht des Smileys (Kreis)
            Ellipse face = new Ellipse()
            {
                Margin = new Thickness(x, y, 0, 0),
                Width = 90,
                Height = 90,
                Fill = faceBrush
            };
            canvas.Children.Add(face);

            // Linkes Auge
            Ellipse leftEye = new Ellipse()
            {
                Margin = new Thickness(x + 25, y + 30, 0, 0),
                Width = 10,
                Height = 10,
                Fill = featureBrush
            };
            canvas.Children.Add(leftEye);

            // Rechtes Auge
            Ellipse rightEye = new Ellipse()
            {
                Margin = new Thickness(x + 65, y + 30, 0, 0),
                Width = 10,
                Height = 10,
                Fill = featureBrush
            };
            canvas.Children.Add(rightEye);

            // Lachender Mund (nach oben gebogener Bogen)
            PathFigure mouthFigure = new PathFigure() { StartPoint = new Point(x + 30, y + 60) };
            mouthFigure.Segments.Add(new ArcSegment()
            {
                Point = new Point(x + 70, y + 60),
                Size = new Size(20, 20),
                SweepDirection = SweepDirection.Counterclockwise // Mund nach oben gebogen
            });

            PathGeometry mouthGeometry = new PathGeometry();
            mouthGeometry.Figures.Add(mouthFigure);

            Path mouth = new Path()
            {
                Stroke = featureBrush,
                StrokeThickness = 2.0,
                Data = mouthGeometry
            };
            canvas.Children.Add(mouth);
        }
    }
    public class D_SkullSymbol : IDrawableSymbol
    {
        public void Draw(Canvas canvas, int x, int y)
        {
            // Farben für den Totenkopf
            Color skullColor = Color.FromRgb(220, 220, 220); // Hellgrauer Schädel
            Color featureColor = Color.FromRgb(0, 0, 0);     // Schwarze Augen und Mund
            Brush skullBrush = new SolidColorBrush(skullColor);
            Brush featureBrush = new SolidColorBrush(featureColor);

            // Kleiner Schädel (kleinerer Kreis)
            Ellipse skull = new Ellipse()
            {
                Margin = new Thickness(x + 20, y + 10, 0, 0),
                Width = 60,
                Height = 60,
                Fill = skullBrush
            };
            canvas.Children.Add(skull);

            // Linke Augenhöhle (kleiner und angepasst an die neue Schädelgröße)
            Ellipse leftEye = new Ellipse()
            {
                Margin = new Thickness(x + 35, y + 25, 0, 0),
                Width = 7,
                Height = 10,
                Fill = featureBrush
            };
            canvas.Children.Add(leftEye);

            // Rechte Augenhöhle
            Ellipse rightEye = new Ellipse()
            {
                Margin = new Thickness(x + 55, y + 25, 0, 0),
                Width = 7,
                Height = 10,
                Fill = featureBrush
            };
            canvas.Children.Add(rightEye);

            // Nasenöffnung (noch kleineres Oval)
            Ellipse nose = new Ellipse()
            {
                Margin = new Thickness(x + 45, y + 40, 0, 0),
                Width = 4,
                Height = 6,
                Fill = featureBrush
            };
            canvas.Children.Add(nose);

            // Kleinerer Mund
            Rectangle mouth = new Rectangle()
            {
                Margin = new Thickness(x + 40, y + 55, 0, 0),
                Width = 20,
                Height = 6,
                Stroke = featureBrush,
                StrokeThickness = 1.0
            };
            canvas.Children.Add(mouth);

            // Kleinere Zähne (vertikale Linien im Mundbereich)
            for (int i = 0; i < 2; i++)
            {
                Line tooth = new Line()
                {
                    X1 = x + 45 + (i * 10),
                    Y1 = y + 55,
                    X2 = x + 45 + (i * 10),
                    Y2 = y + 61,
                    Stroke = featureBrush,
                    StrokeThickness = 0.8
                };
                canvas.Children.Add(tooth);
            }

            //// Kleinere gekreuzte Knochen
            //Line bone1 = new Line()
            //{
            //    X1 = x + 25,
            //    Y1 = y + 75,
            //    X2 = x + 55,
            //    Y2 = y + 85,
            //    Stroke = featureBrush,
            //    StrokeThickness = 1.5
            //};
            //canvas.Children.Add(bone1);

            //Line bone2 = new Line()
            //{
            //    X1 = x + 25,
            //    Y1 = y + 85,
            //    X2 = x + 55,
            //    Y2 = y + 75,
            //    Stroke = featureBrush,
            //    StrokeThickness = 1.5
            //};
            //canvas.Children.Add(bone2);
        }
    }
    

    public class D_RulesTikTokToo : ID_TTTRules
    {
        D_FieldTikTokToo _Field = new D_FieldTikTokToo();


        public string Name { get { return "JLJs_TTTRules"; } }

        public ID_TTTGameField TTTField { get { return (ID_TTTGameField)_Field; } }
        public IGameField CurrentField { get { return TTTField; } }

        public bool MovesPossible
        {
            get
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (_Field[i, j] == 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }
        }



        public int CheckIfPLayerWon()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_Field[i, 0] > 0 && _Field[i, 0] == _Field[i, 1] && _Field[i, 1] == _Field[i, 2])
                {
                    return _Field[i, 0];
                }
                else if (_Field[0, i] > 0 && _Field[0, i] == _Field[1, i] && _Field[1, i] == _Field[2, i])
                {
                    return _Field[0, i];
                }
            }

            if (_Field[0, 0] > 0 && _Field[0, 0] == _Field[1, 1] && _Field[1, 1] == _Field[2, 2])
            {
                return _Field[0, 0];
            }
            else if (_Field[0, 2] > 0 && _Field[0, 2] == _Field[1, 1] && _Field[1, 1] == _Field[2, 0])
            {
                return _Field[0, 2];
            }

            return -1;
        }

        public void ClearField()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _Field[i, j] = 0;
                }
            }
        }


        public void DoMove(IPlayMove move)
        {
            if (move is ID_TTTMove)
            {
                DoTicTacToeMove((ID_TTTMove)move);
            }
        }

        public void DoTicTacToeMove(ID_TTTMove move)
        {
            if (move.Row >= 0 && move.Row < 3 && move.Column >= 0 && move.Column < 3)
            {
                _Field[move.Row, move.Column] = move.PlayerNumber;
            }
        }
    }

    public class D_FieldTikTokToo : ID_TTTGameField
    {


        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is ID_PaintTTT;
        }

        int[,] _Field = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        public int this[int r, int c]
        {
            get
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    return _Field[r, c];
                }
                else
                {
                    return -1;
                }
            }

            set
            {
                if (r >= 0 && r < 3 && c >= 0 && c < 3)
                {
                    _Field[r, c] = value;
                }
            }
        }
    }

    public class D_MoveTikTokToo : ID_TTTMove
    {
        int _PlayerNumber = 0;

        int _Row = 0;

        int _Column = 0;

        public D_MoveTikTokToo(int row, int column, int playerNumber)
        {
            _Row = row;
            _Column = column;
            _PlayerNumber = playerNumber;
        }

        public int Row { get { return _Row; } }

        public int Column { get { return _Column; } }

        public int PlayerNumber { get { return _PlayerNumber; } }
    }

    public class D_HumanPTikTokToo : ID_HumanTTTPlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "JLJs_TTTHumanPlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is ID_TTTRules;
        }

        public IGamePlayer Clone()
        {
            D_HumanPTikTokToo D_TTT_HP = new D_HumanPTikTokToo();
            D_TTT_HP.SetPlayerNumber(this._PlayerNumber);
            return D_TTT_HP;
        }

        public ID_TTTMove GetMove(IMoveSelection selection, ID_TTTGameField field)
        {


            if (selection is IClickSelection)
            {
                IClickSelection sel = (IClickSelection)selection;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (sel.XClickPos > 20 + (j * 100) && sel.XClickPos < 120 + (j * 100) &&
                            sel.YClickPos > 20 + (i * 100) && sel.YClickPos < 120 + (i * 100) &&
                            field[i, j] <= 0)
                        {
                            return new D_MoveTikTokToo(i, j, _PlayerNumber);
                        }
                    }
                }
            }
            return null;


        }

        public IPlayMove GetMove(IMoveSelection selection, IGameField field)
        {
            if (field is ID_TTTGameField)
            {
                return GetMove(selection, (ID_TTTGameField)field);
            }
            else
            {
                return null;
            }
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }
    }

    public class D_ComputerPTikTokToo : ID_ComputerTTTPlayer
    {
        int _PlayerNumber = 0;

        public string Name { get { return "JLJs_TTTComputerPlayer"; } }

        public int PlayerNumber { get { return _PlayerNumber; } }

        public bool CanBeRuledBy(IGameRules rules)
        {
            return rules is D_RulesTikTokToo;
        }

        public IGamePlayer Clone()
        {
            D_ComputerPTikTokToo D_TTT_CP = new D_ComputerPTikTokToo();
            D_TTT_CP.SetPlayerNumber(this._PlayerNumber);
            return D_TTT_CP;
        }
        public IPlayMove GetMove(IGameField field)
        {
            if (field is ID_TTTGameField)
            {
                return GetMove((ID_TTTGameField)field);
            }
            else { return null; }
        }

        public ID_TTTMove GetMove(ID_TTTGameField field)
        {
            // 1. Schritt: Prüfen, ob der Computer gewinnen kann und den Gewinnzug machen
            ID_TTTMove winningMove = FindWinningMove(field, _PlayerNumber);
            if (winningMove != null)
            {
                return winningMove;
            }

            // 2. Schritt: Prüfen, ob der Gegner gewinnen könnte, und blockieren
            int opponentNumber = _PlayerNumber == 1 ? 2 : 1;
            ID_TTTMove blockingMove = FindWinningMove(field, opponentNumber);
            if (blockingMove != null)
            {
                return blockingMove;
            }

            // 3. Schritt: Falls weder Gewinn noch Block möglich ist, Zentrum wählen
            if (field[1, 1] == 0)
            {
                return new D_MoveTikTokToo(1, 1, _PlayerNumber);
            }

            // 4. Schritt: Ecken besetzen, falls das Zentrum schon belegt ist
            int[,] ecken = new int[,] { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
            for (int i = 0; i < ecken.GetLength(0); i++)
            {
                int row = ecken[i, 0];
                int col = ecken[i, 1];
                if (field[row, col] == 0)
                {
                    return new D_MoveTikTokToo(row, col, _PlayerNumber);
                }
            }

            // 5. Schritt: Alle verbleibenden freien Felder der Reihe nach prüfen
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (field[i, j] == 0)
                    {
                        return new D_MoveTikTokToo(i, j, _PlayerNumber);
                    }
                }
            }
            return null;

        }

        // Hilfsmethode, um einen Gewinnzug für den Spieler zu finden
        private ID_TTTMove FindWinningMove(ID_TTTGameField field, int playerNumber)
        {
            // Reihen und Spalten durchgehen, um Gewinnmöglichkeiten zu prüfen
            for (int i = 0; i < 3; i++)
            {
                // Horizontale Reihen prüfen
                if (field[i, 0] == playerNumber && field[i, 1] == playerNumber && field[i, 2] == 0)
                    return new D_MoveTikTokToo(i, 2, _PlayerNumber);
                if (field[i, 0] == playerNumber && field[i, 2] == playerNumber && field[i, 1] == 0)
                    return new D_MoveTikTokToo(i, 1, _PlayerNumber);
                if (field[i, 1] == playerNumber && field[i, 2] == playerNumber && field[i, 0] == 0)
                    return new D_MoveTikTokToo(i, 0, _PlayerNumber);

                // Vertikale Spalten prüfen
                if (field[0, i] == playerNumber && field[1, i] == playerNumber && field[2, i] == 0)
                    return new D_MoveTikTokToo(2, i, _PlayerNumber);
                if (field[0, i] == playerNumber && field[2, i] == playerNumber && field[1, i] == 0)
                    return new D_MoveTikTokToo(1, i, _PlayerNumber);
                if (field[1, i] == playerNumber && field[2, i] == playerNumber && field[0, i] == 0)
                    return new D_MoveTikTokToo(0, i, _PlayerNumber);
            }

            // Diagonalen prüfen
            if (field[0, 0] == playerNumber && field[1, 1] == playerNumber && field[2, 2] == 0)
                return new D_MoveTikTokToo(2, 2, _PlayerNumber);
            if (field[0, 0] == playerNumber && field[2, 2] == playerNumber && field[1, 1] == 0)
                return new D_MoveTikTokToo(1, 1, _PlayerNumber);
            if (field[1, 1] == playerNumber && field[2, 2] == playerNumber && field[0, 0] == 0)
                return new D_MoveTikTokToo(0, 0, _PlayerNumber);

            if (field[0, 2] == playerNumber && field[1, 1] == playerNumber && field[2, 0] == 0)
                return new D_MoveTikTokToo(2, 0, _PlayerNumber);
            if (field[0, 2] == playerNumber && field[2, 0] == playerNumber && field[1, 1] == 0)
                return new D_MoveTikTokToo(1, 1, _PlayerNumber);
            if (field[1, 1] == playerNumber && field[2, 0] == playerNumber && field[0, 2] == 0)
                return new D_MoveTikTokToo(0, 2, _PlayerNumber);

            return null; // Kein Gewinnzug gefunden
        }

        public void SetPlayerNumber(int playerNumber)
        {
            _PlayerNumber = playerNumber;
        }


    }
}
