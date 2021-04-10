using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Project_Dodge_Game.Classes
{
    public class Enemy : ObjectOnCanvas
    {
        protected Point _firePoint { get; set; }
        public Missile _weapon { get; set; }
        public int _scoreForKill { get; protected set; }

        public Enemy(double width, double height, double left, double top, string uri, string methodOfMovement)
            : base(width, height, left, top, uri, methodOfMovement) { }
       
        public override Image CreateAnImageToCanvas(string Path, double width, double height, double left, double top)=>       
             base.CreateAnImageToCanvas(Path, width, height, left, top);
        

        public virtual Image Shoot(double angle) => _weapon._image;


        //calc distance between two points
        protected double Distance(Point pointA, Point pointB)=>        
             Math.Sqrt(Math.Pow(pointA.X - pointB.X, 2) + Math.Pow(pointA.Y - pointB.Y, 2)); 
             
    }
}
