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

        public void B_Paint_Net(Canvas canvas, int fieldStyle)
        {
            Width = canvas.ActualWidth * 0.05;
            Height = canvas.ActualHeight * 0.4;

            switch (fieldStyle)
            {
                case 0:
                    Rectangle net = new Rectangle
                    {
                        Width = Width,
                        Height = Height,
                        Fill = Brushes.Gray
                    };
                    Canvas.SetLeft(net, canvas.ActualWidth / 2 - Width / 2);
                    Canvas.SetTop(net, canvas.ActualHeight - Height);
                    canvas.Children.Add(net);
                    break;
                case 1:
                    var net_img = new Image
                    {
                        Width = Width,
                        Height = Height * 1.1,
                        Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Net.PNG", UriKind.Relative)),
                        Stretch = Stretch.Fill
                    };
                    Canvas.SetLeft(net_img, canvas.ActualWidth / 2 - Width / 2);
                    Canvas.SetTop(net_img, canvas.ActualHeight - Height);
                    canvas.Children.Add(net_img);
                    break;
                case 2:
                    var net_drw = new Image
                    {
                        Width = Width,
                        Height = Height * 0.95,
                        Source = new BitmapImage(new Uri("/Classes/B_Gruppe/Volley/Grafiken/Net_Drawing.PNG", UriKind.Relative)),
                        Stretch = Stretch.Fill
                    };
                    Canvas.SetLeft(net_drw, canvas.ActualWidth / 2 - Width / 2);
                    Canvas.SetTop(net_drw, canvas.ActualHeight - Height);
                    canvas.Children.Add(net_drw);
                    break;
            }
        }
    }
}
