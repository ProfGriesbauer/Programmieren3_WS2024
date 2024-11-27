using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OOPGames
{
    public class B_Net_BV : IB_Net_BV
    {
        public double Height { get; set; }
        public double Width { get; set; }

        public void B_Paint_Net(Canvas canvas, IB_Ground_BV ground)
        {
            Width = canvas.ActualWidth * 0.025;
            Height = canvas.ActualHeight * 0.6;
            Rectangle net = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = Brushes.Gray
            };
            Canvas.SetLeft(net, canvas.ActualWidth / 2 - Width / 2);
            Canvas.SetTop(net, canvas.ActualHeight - ground.Height - Height);
            canvas.Children.Add(net);
        }
    }
}
