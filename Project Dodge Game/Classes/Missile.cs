using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Project_Dodge_Game.Classes
{
    public class Missile : ObjectOnCanvas
    {       
        public bool destructed { get; set; }
        public int _lifeTaking { get; set; }       
        public double _leftMovement { get; set; }
        public double _topMovement { get; set; }
        public Missile(double width, double height, double left, double top, string uri, string methodOfMovement)
            : base(width, height, left, top, uri, methodOfMovement)
        {
            destructed = false;
        }

        public virtual void Collision(bool impact) => destructed = true; 
    }
}
