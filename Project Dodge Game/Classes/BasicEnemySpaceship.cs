using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Project_Dodge_Game.Classes
{
    class BasicEnemySpaceship : Enemy
    {
        public BasicEnemySpaceship(double left, double top)
            : base(50, 50, left, top, "/Assets/basicenemyspaceship.png", "random")
        {
            _movementSpeed = 5;
            _life = 4;
            _scoreForKill = 1000;
        }

        public override Image CreateAnImageToCanvas(string Path, double width, double height, double left, double top)=>
            base.CreateAnImageToCanvas(Path, width, height, left, top);   

        public override Image Shoot(double angle)
        {
            double distanceBetweenCentertoBorderOfEnemy = Distance(GetCenterOfMass(), GetCurrentTopPoint());
            _weapon = new AdvancedMissile(GetCenterOfMass().X, GetCenterOfMass().Y);
            double distanceBetweenCentertoBorderOfMissile = Distance(_weapon.GetCenterOfMass(), _weapon.GetCurrentTopPoint());

            _firePoint = new Point((GetCenterOfMass().X - _weapon.Width / 2) + (distanceBetweenCentertoBorderOfEnemy + distanceBetweenCentertoBorderOfMissile)
                * Math.Cos(angle), (GetCenterOfMass().Y - _weapon.Height / 2) + (distanceBetweenCentertoBorderOfEnemy + distanceBetweenCentertoBorderOfMissile) * 
                Math.Sin(angle));

            _weapon = new HomeInMissile(_firePoint.X, _firePoint.Y);
            return base.Shoot(angle);
        }
    }
}
