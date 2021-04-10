using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Dodge_Game.Classes
{
    public class Laser : Missile
    {
        const int LIFE_TIME = 300;
        public int _destructionTimer { get; set; }
        public Laser(double left, double top) : base(50, 50, left, top, "/Assets/green_lazer.png", "homeIn")
        {
            _destructionTimer = 0;
            _movementSpeed = 9;
            _lifeTaking = 1;
        }

        public override void Collision(bool impact)
        {
            if (impact)  //colided
            {
                base.Collision(true);
                return;
            }
            _destructionTimer++; //(not collided)- Active Timer 
            if (_destructionTimer == LIFE_TIME) base.Collision(true);  //time is up. do self-destruction (no impact)   
        }
    }
}
