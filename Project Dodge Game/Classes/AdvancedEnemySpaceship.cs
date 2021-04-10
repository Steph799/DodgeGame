using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace Project_Dodge_Game.Classes
{
    class AdvancedEnemySpaceship : Enemy
    {
        public AdvancedEnemySpaceship(double left, double top)
            : base(75, 75, left, top, "/Assets/AdvancedEnemySpaceship.png", "random")
        {
            _movementSpeed = 7;
            _life = 10;
            _scoreForKill = 3000;
        }
        public override Image Shoot(double angle)
        {
            double distanceBetweenCentertoBorderOfEnemy = Distance(GetCenterOfMass(), GetCurrentTopPoint());
        
            _weapon = new AdvancedMissile(GetCenterOfMass().X, GetCenterOfMass().Y);
            double distanceBetweenCentertoBorderOfMissile = Distance(_weapon.GetCenterOfMass(), _weapon.GetCurrentTopPoint());

            _firePoint = new Point((GetCenterOfMass().X - _weapon.Width / 2)+ (distanceBetweenCentertoBorderOfEnemy+ distanceBetweenCentertoBorderOfMissile) * Math.Cos(angle),
                (GetCenterOfMass().Y - _weapon.Height / 2) + (distanceBetweenCentertoBorderOfEnemy + distanceBetweenCentertoBorderOfMissile)* Math.Sin(angle));
          
            double leftSpeed = Math.Cos(angle) * _weapon._movementSpeed;
            double topSpeed = Math.Sin(angle) * _weapon._movementSpeed;
            _weapon = new AdvancedMissile(_firePoint.X, _firePoint.Y, leftSpeed, topSpeed);
            return base.Shoot(angle);
        }
        public override Image CreateAnImageToCanvas(string Path, double width, double height, double left, double top)=>     
             base.CreateAnImageToCanvas(Path, width, height, left, top);       
    }
}
