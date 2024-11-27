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
    public class B_Ground_BV : IB_Ground_BV
    {
        public double Height { get; set; }

        public void B_Paint_Ground(Canvas canvas)
        {
            Height = canvas.ActualHeight * 0.1;
            Rectangle ground = new Rectangle
            {
                Width = canvas.ActualWidth,
                Height = Height,
                Fill = Brushes.Green
            };
            Canvas.SetTop(ground, canvas.ActualHeight - Height);
            canvas.Children.Add(ground);
        }
    }
}
