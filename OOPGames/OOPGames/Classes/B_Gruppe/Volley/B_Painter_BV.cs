﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;

namespace OOPGames
{
    public class B_Painter_BV : IB_Painter_BV
    {
        public string Name => "Blobby Volley Painter";

        public void PaintBlobbyVolley(Canvas canvas, IB_Field_BV field)
        {
            field.Height = canvas.ActualHeight;
            field.Width = canvas.ActualWidth;

            canvas.Children.Clear();

            //Hinetrgrund zeichnen
            switch (field.FieldStyle)
            {
                case 0:
                    field.Ground.B_Paint_Ground(canvas);
                    break;
                case 1:
                    var background = new Image
                    {
                        Width = field.Width,
                        Height = field.Height,
                        Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Background.PNG", UriKind.Relative)),
                        Stretch = Stretch.Fill
                    };
                    Canvas.SetLeft(background, 0);
                    Canvas.SetTop(background, 0);
                    canvas.Children.Add(background);
                    break;
                case 2:
                    var background_drw = new Image
                    {
                        Width = field.Width,
                        Height = field.Height,
                        Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Background_Drawing.PNG", UriKind.Relative)),
                        Stretch = Stretch.Fill
                    };
                    Canvas.SetLeft(background_drw, -field.Width * 0.01);
                    Canvas.SetTop(background_drw, 0);
                    canvas.Children.Add(background_drw);
                    break;
            }


            // Netz zeichnen
            field.Net.B_Paint_Net(canvas, field.FieldStyle);

            // Spieler zeichnen
            field.Player[0].B_Paint_Player(canvas, field.FieldStyle);
            field.Player[1].B_Paint_Player(canvas, field.FieldStyle);

            // Ball zeichnen
            field.Ball.B_Paint_Ball(canvas, field.FieldStyle);

            // Score zeichnen
            TextBlock scorePlayer1 = new TextBlock
            {
                Text = $"{field.Rules_BV.Points[0]}",
                FontSize = 24,
                Foreground = Brushes.Black
            };
            Canvas.SetLeft(scorePlayer1, 10);
            Canvas.SetTop(scorePlayer1, 10);
            canvas.Children.Add(scorePlayer1);

            TextBlock scorePlayer2 = new TextBlock
            {
                Text = $"{field.Rules_BV.Points[1]}",
                FontSize = 24,
                Foreground = Brushes.Black
            };
            Canvas.SetRight(scorePlayer2, 10);
            Canvas.SetTop(scorePlayer2, 10);
            canvas.Children.Add(scorePlayer2);
        }


        public void PaintGameField(Canvas canvas, IGameField playField)
        {
            if (playField is IB_Field_BV)
            {
                PaintBlobbyVolley(canvas, (IB_Field_BV)playField);
            }
        }

        public void TickPaintGameField(Canvas canvas, IGameField playField)
        {
            if (playField is IB_Field_BV)
            {
                PaintBlobbyVolley(canvas, (IB_Field_BV)playField);
            }
        }
    }
}
