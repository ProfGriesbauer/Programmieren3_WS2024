
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace OOPGames
{
   /* public static class A_Cell
    {
        public static int _rows = 3;
       
        public static int _columns = 3;

        public static double Cell_Width(Canvas canvas)
        {
            return canvas.ActualWidth / _columns;
        }

        public static double Cell_Height(Canvas canvas)
        {
            return canvas.ActualHeight / _rows;
        }

    }*/


    public class A_Painter : IPaintGame
    {
        public string Name 
        { 
        get { return "Painter Gruppe A"; }
        
        }

        A_X_Shape _xShape = new A_X_Shape(); //Static oder nicht vor und nachteil ?
        A_Circle_Shape _circleShape = new A_Circle_Shape(); //Jedesmal ein neues Objekt erstellen carbage collector ?

        public void PaintGameField(Canvas canvas, IGameField currentField)
        {
            if (currentField is IA_TicTacToeField)
            {
                IA_TicTacToeField myCurrentField = (IA_TicTacToeField)currentField;
                myCurrentField.AdoptCanvas(canvas);
            
                // Canvas leeren, um ein erneutes Zeichnen zu vermeiden
                canvas.Children.Clear();

                // Durchlaufen der Spielfelder, um jede Zelle zu zeichnen
                for (int row = 0; row <= myCurrentField.rows-1; row++)
                {
                    for (int col = 0; col <= myCurrentField.columns-1; col++)
                    {
                        // Rechteck für jede Zelle erstellen
                        Rectangle cell = new Rectangle
                        {
                            Width = myCurrentField.cellWidth,
                            Height = myCurrentField.cellHeight,
                            Fill = Brushes.White,   
                            Stroke = Brushes.Black, 
                            StrokeThickness = 1
                        };

                        // Position der Zelle auf der Canvas setzen
                        Canvas.SetLeft(cell, col * myCurrentField.cellWidth);
                        Canvas.SetTop(cell, row * myCurrentField.cellHeight);

                        // Zelle zum Canvas hinzufügen
                        canvas.Children.Add(cell);

                   
                            if (myCurrentField[row, col] == 1)
                            {
                                _xShape.DrawX(canvas, row, col, myCurrentField);
                            }
                            else if (myCurrentField[row, col] == 2)
                            {
                                _circleShape.DraxCircle(canvas, row, col, myCurrentField);
                            }
                    }
                }
            }
            else
            {
                return;
            }

        }
    }



   
    public class A_X_Shape
    {
        
        private double _rightTopPointX;
        private double _rightTopPointY;

        private double _leftBottomPointX;
        private double _leftBottomPointY;


        private double _leftTopPointX;
        private double _leftTopPointY;

        private double _rightBottomPointX;
        private double _rightBottomPointY;

        private Color _lineColor;
        private Brush _lineStroke;



        public void DrawX(Canvas canvas, int row, int column, IA_TicTacToeField field)
        {
            _rightTopPointX = (column + 1) * field.cellWidth;
            _rightTopPointY = row * field.cellHeight;

            _leftBottomPointX = column * field.cellWidth;
            _leftBottomPointY = (row+1) * field.cellHeight;

            _leftTopPointX = column * field.cellWidth;
            _leftTopPointY = row * field.cellHeight;

            _rightBottomPointX = (column+1) * field.cellWidth;
            _rightBottomPointY = (row+1) * field.cellHeight;

            _lineColor = Color.FromRgb(255, 0, 0);
            _lineStroke = new SolidColorBrush(_lineColor);

            Line line1 = new Line() { X1 = _leftBottomPointX, Y1 = _leftBottomPointY, X2 = _rightTopPointX, Y2 = _rightTopPointY, Stroke = _lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(line1);

            Line line2 = new Line() { X1 = _rightBottomPointX, Y1 = _rightBottomPointY, X2 = _leftTopPointX, Y2 = _leftTopPointY, Stroke = _lineStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(line2);
        }
    }



    public class A_Circle_Shape 
    {
        
        private double _radius;
        private double _leftDistance;
        private double _topDistance;

        private Color _circleColor;
        private Brush _circleStroke;
        
        public void DraxCircle(Canvas canvas, int row, int column, IA_TicTacToeField field)
        {
            _radius = field.cellHeight -10;
            _leftDistance =column * field.cellWidth;
            _topDistance = row * field.cellHeight;
            _circleColor = Color.FromRgb(0, 0, 255);
            _circleStroke = new SolidColorBrush(_circleColor);

            Ellipse circle = new Ellipse() { Margin = new Thickness(3.5 +(_leftDistance), 5 + (_topDistance), 0, 0), Width = _radius, Height = _radius, Stroke = _circleStroke, StrokeThickness = 3.0 };
            canvas.Children.Add(circle);
        }

    }



    
    //Beim erstellen des Objektes iterriert der Konstruktor über jedes Feld und schreibt eine null rein
    public class A_Field : IA_TicTacToeField
    {
        int[,] _field;
        int _rows;
        int _columns;
        int _width;
        int _height;

        public A_Field(int rows, int columns)
        {
            _columns = columns;
            _rows = rows;
            _field = new int[_rows, _columns];

            for (int row = 0; row < _rows; row++)
            {
                for (int col = 0; col < _columns; col++)
                {
                    _field[row, col] = 0;
                }
            }
        }

        public int this[int r, int c]
        { 
            get {  return _field[r, c]; }
            set { _field[r, c] = value; }
        }

        public int rows { get => _rows; }
        public int columns { get => _columns; }

        public int width { get => _width; }

        public int height { get => _height; }

        public int cellWidth { get => (int)(_width / columns); }

        public int cellHeight { get => (int)(_height / rows); }

        public void AdoptCanvas(Canvas canvas)
        {
            _width = (int)canvas.ActualWidth;
            _height = (int)canvas.ActualHeight;
        }

        public bool CanBePaintedBy(IPaintGame painter)
        {
            return painter is A_Painter;
        }
    }


}
