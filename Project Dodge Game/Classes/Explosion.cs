using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Dodge_Game.Classes
{
    class Explosion : ObjectOnCanvas
    {
        public Explosion(double left, double top) : base(45, 45, left, top, "/Assets/collision.png", "static") { }      
    }
}
