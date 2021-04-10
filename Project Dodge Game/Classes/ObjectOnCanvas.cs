using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Project_Dodge_Game.Classes
{
    public class ObjectOnCanvas
    {
        public int _movementSpeed;   
        public int _life { get; set; }
        public string _methodOfMovement { get; set; }
        public bool dead { get; set; }
        public Image _image { get; set; }
        public double Width
        {
            get { return _image.ActualWidth; }

            set { _image.Width = value; }
        }
        public double Height
        {
            get { return _image.ActualHeight; }

            set { _image.Height = value; }
        }

        public double Left
        {
            get { return Canvas.GetLeft(_image); }
            set { Canvas.SetLeft(_image, value); }
        }
        public double Top
        {
            get { return Canvas.GetTop(_image); }
            set { Canvas.SetTop(_image, value); }
        }
        public string Uri
        {
            set
            {
                _image.Source = new BitmapImage(new Uri($"ms-appx://{value}"));
            }
        }
        public ObjectOnCanvas(double width, double height, double left, double top, string uri, string methodOfMovement)
        {
            _image = new Image();
            Width = width;
            Height = height;
            Left = left;
            Top = top;
            Uri = uri;
            _methodOfMovement = methodOfMovement;
            dead = false;
        }

        public void AddToCanvas(Canvas canvas) => canvas.Children.Add(_image);

        public void RemoveFromCanvas(Canvas canvas) => canvas.Children.Remove(_image);

        public virtual Image CreateAnImageToCanvas(string Path, double width, double height, double left, double top)
        {
            Image image = new Image();
            image.Width = width;
            image.Height = height;
            image.Source = new BitmapImage(new Uri($"ms-appx://{Path}"));
            Canvas.SetLeft(image, left);
            Canvas.SetTop(image, top);
            return image;
        }
 
        public Point GetCenterOfMass() => new Point(Canvas.GetLeft(_image) + Width / 2, Canvas.GetTop(_image) + Height / 2); 
   
        public Point GetCurrentTopPoint() => new Point(Left, Top);      
    }
}
