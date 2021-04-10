using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Dodge_Game.Classes
{ 
    class AdvancedMissile : Missile
    {
        public AdvancedMissile(double left, double top, double LeftMovement, double TopMovement)
            : base(50, 50, left, top, "/Assets/missile 2.png", "linear")
        {
            _movementSpeed = 12;
            _lifeTaking = 5;
            _leftMovement = LeftMovement;
            _topMovement = TopMovement;
        }
        public AdvancedMissile(double left, double top) : base(50, 50, left, top, "/Assets/missile 2.png", "linear")
        {
            _movementSpeed = 12;
            _lifeTaking = 5;
        }
    }
}
