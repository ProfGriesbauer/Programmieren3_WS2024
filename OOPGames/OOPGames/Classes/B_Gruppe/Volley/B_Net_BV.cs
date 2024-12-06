using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
            Height = canvas.ActualHeight * 0.4;
            //Net as Rectangle
            /*
            Rectangle net = new Rectangle
            {
                Width = Width,
                Height = Height,
                Fill = Brushes.Gray
            };
            */
            //Net as Image
            var net = new Image
            {
                Width = Width,
                Height = Height * 1.1,
                Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Net.PNG", UriKind.Relative)),
                Stretch = Stretch.Fill
            };

            Canvas.SetLeft(net, canvas.ActualWidth / 2 - Width / 2);
            Canvas.SetTop(net, canvas.ActualHeight - Height);
            canvas.Children.Add(net);
        }
    }
}
