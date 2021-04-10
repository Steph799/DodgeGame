using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Project_Dodge_Game.Classes
{
    public class Player : ObjectOnCanvas
    {
        public Player(double left, double top)
            : base(30, 50, left, top, "/Assets/Myspaceship up.png", "manual")
        {
            _movementSpeed = 10;
            _life = 100;
        }     

        public override Image CreateAnImageToCanvas(string Path, double width, double height, double left, double top)=>       
             base.CreateAnImageToCanvas(Path, width, height, left, top);          
    }
}
