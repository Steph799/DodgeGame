using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Project_Dodge_Game.Classes
{
    public class GameManager
    {
        const int GAME_OVER_TIME = 100 * 6 * 5; // 5 minutes

        int counterCreateEnemy = 0;
        int counterChangeDirection = 0;
        List<int> _enemyDirection;
        private readonly MediaElement _explosion;
        public bool _win { get; set; }
        public Canvas _gameBoard { get; set; }
        Random ran;
        Enemy _enemyObject;
        string _enemyKey;
        public Player _player { get; set; }
        public List<Enemy> _enemies { get; }
        public List<Missile> _missiles { get; }
        public int gameCounter { get; set; } //in general 
        public int _score { get; set; }
        public DispatcherTimer dt { get; set; }
        public bool isGameRunning { get; set; }
        public double _fireDegree { get; set; }

        public GameManager(Canvas canvas, MediaElement explosion)
        {
            gameCounter = 0;
            _win = false;
            ran = new Random();
            dt = new DispatcherTimer();         
            dt.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dt.Start();
            isGameRunning = true;
            _enemies = new List<Enemy>();
            _missiles = new List<Missile>();
            _enemyDirection = new List<int>();
            _player = new Player(550, 360);
            _gameBoard = canvas;
            _score = 0;
            _fireDegree = 0;
            _explosion = explosion;
        }
        public void InitializeGame() 
        {
            _player.CreateAnImageToCanvas("/Assets/Myspaceship up.png", 30, 50, 550, 360);
            _player.AddToCanvas(_gameBoard);
        }

        public void InitializeEnemy()
        {
            gameCounter++;
            double randomSide;
            double randomtop = 0;

            if (counterCreateEnemy % 300 == 0)
            {
                if (gameCounter >= GAME_OVER_TIME) //arrived to 5 minuts. next challenge- no missiles & no enmies at the same time
                {
                    if (_enemies.Count == 0 && _missiles.Count == 0) 
                    {
                        _win = true;
                        return;
                    }
                }
                randomSide = ran.Next(2);
                randomtop = ran.Next(1,(int)(_gameBoard.ActualHeight - 40)); 
                if (randomSide == 1)
                {
                    randomSide = _gameBoard.ActualWidth - 50;
                }
                _enemyObject = new BasicEnemySpaceship(randomSide, randomtop);
                CreateEnemy(_enemyObject);
            }
            if (gameCounter >= 2* 300 && gameCounter < GAME_OVER_TIME)
            {
                if (counterCreateEnemy % 350 == 0)
                {
                    randomSide = ran.Next(2);
                    randomtop = ran.Next((int)(_gameBoard.ActualHeight - 80));
                    if (randomSide == 1)
                    {
                        randomSide = _gameBoard.ActualWidth - 80;
                    }
                    _enemyObject = new AdvancedEnemySpaceship(randomSide, randomtop);
                    CreateEnemy(_enemyObject);
                }
            }
            if (gameCounter >= 2 * 300)
            {
                if (counterCreateEnemy % 450 == 0)
                {
                    randomSide = ran.Next(2);
                    randomtop = ran.Next((int)(_gameBoard.ActualHeight - 40));
                    if (randomSide == 1)
                    {
                        randomSide = _gameBoard.ActualWidth - 50;
                    }
                    _enemyObject = new NeutronBomb(randomSide, randomtop);
                    CreateEnemy(_enemyObject);
                }
            }
            counterCreateEnemy++;
        }

        public void CreateEnemy(Enemy enemy)
        {
            _gameBoard.Children.Add(enemy._image); //add image of an enemy spaceship
            _enemies.Add(enemy); //add an enemy item to the list
            _enemyDirection.Add(ran.Next(4)); //add a random direction                                          
        }
        public void HandleEnemyMovement()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                if (counterChangeDirection % 25 == 0)
                {
                    _enemyDirection[i] = ran.Next(4);
                    counterChangeDirection = 0;
                }
                switch (_enemyDirection[i])
                {
                    case 0:
                        _enemyKey = "Right";   //send to enemy.move(right)                     
                        break;
                    case 1:
                        _enemyKey = "Left";   //send to enemy.move(left)                  
                        break;
                    case 2:
                        _enemyKey = "Up";   //send to enemy.move(up)                  
                        break;
                    case 3:
                        _enemyKey = "Down";//send to enemy.move(down)
                        break;
                }
                CheckEnemyCollisionWithBorder(_enemies[i]._image, _enemies[i]._movementSpeed, _enemyKey);
            }
            counterChangeDirection++;
        }

        private void CheckEnemyCollisionWithBorder(Image enemySpaceship, int speed, string key)
        {
            double top = Canvas.GetTop(enemySpaceship);
            double left = Canvas.GetLeft(enemySpaceship);

            if (key == "Right")
            {
                if (enemySpaceship.ActualWidth + left + speed <= _gameBoard.ActualWidth - 50)               
                    Canvas.SetLeft(enemySpaceship, left + speed);               
            }
            else if (key == "Left")
            {
                if (left - speed >= 50) Canvas.SetLeft(enemySpaceship, left - speed);
            }
            else if (key == "Up")
            {
                if (top - speed >= 50) Canvas.SetTop(enemySpaceship, top - speed);
            }
            else if (key == "Down")
            {
                if (enemySpaceship.ActualHeight + top + speed <= _gameBoard.ActualHeight - 50)             
                    Canvas.SetTop(enemySpaceship, top + speed);               
            }
        }

        public void CreateMissile(Missile missile)
        {
            _gameBoard.Children.Add(missile._image); //add the missile image to canvas
            _missiles.Add(missile); //add a missile to the list
        }
        public void Attack()
        {
            for (int i = 0; i < _enemies.Count; i++)
            {
                _fireDegree = Angle(_enemies[i].GetCenterOfMass(), _player.GetCenterOfMass());
                _enemies[i].Shoot(_fireDegree); //shoot func create a missile (return the picture of the missile with its data)            
                CreateMissile(_enemies[i]._weapon); //show picture of missile               
            }
        }

        private void CreatingExplosion(Missile missile, int index) //when the exlode occure
        {
            Explosion _explosionObject = new Explosion(Canvas.GetLeft(missile._image), Canvas.GetTop(missile._image));
            missile.RemoveFromCanvas(_gameBoard); //remove the missile picutre

            _missiles.RemoveAt(index); //remove the missile from the list (note that we use removeAt to remove the specific missile and not the first missile
                                       //that appear on the list wich have the same value)

            _gameBoard.Children.Add(_explosionObject._image);  //   //creat an image of explosion         
            _explosion.Play();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2); //wait 2 second before desappearing the image
            timer.Tick += (sender, args) => ExplosionTimer(_explosionObject);
            timer.Start();
        }

        private void ExplosionTimer(Explosion explosion)
        {
            explosion.RemoveFromCanvas(_gameBoard);
            explosion = null;
        }


        // calc angle (in Rad) in order to know how to aim
        public double Angle(Point pointA, Point pointB)=> Math.Atan2(pointB.Y - pointA.Y, pointB.X - pointA.X);


        public void MissileMoveBehavior(List<Missile> missiles, List<Enemy> enemies)
        {
            for (int i = 0; i < missiles.Count; i++)
            {
                double topMissile = Canvas.GetTop(missiles[i]._image);
                double leftMissile = Canvas.GetLeft(missiles[i]._image);
                if (missiles[i]._methodOfMovement == "homeIn")
                {
                    double updatedDegree = Angle(missiles[i].GetCenterOfMass(), _player.GetCenterOfMass());
                    double leftMove = Math.Cos(updatedDegree) * missiles[i]._movementSpeed;
                    double TopMove = Math.Sin(updatedDegree) * missiles[i]._movementSpeed;

                    Canvas.SetLeft(missiles[i]._image, Canvas.GetLeft(missiles[i]._image) + leftMove);
                    Canvas.SetTop(missiles[i]._image, Canvas.GetTop(missiles[i]._image) + TopMove);

                    missiles[i].Collision(false); //run timer
                    if (missiles[i].destructed || !CollisionWithBorder(missiles[i], leftMissile, topMissile)) //time is up or collision with border
                    {
                        CreatingExplosion(missiles[i], i);
                        i--;
                        continue;
                    }
                }
                else //linear
                {                    
                    if (CollisionWithBorder(missiles[i], leftMissile, topMissile))  //collision with border
                    {
                        Canvas.SetLeft(missiles[i]._image, leftMissile + missiles[i]._leftMovement);
                        Canvas.SetTop(missiles[i]._image, topMissile + missiles[i]._topMovement);
                    }
                    else
                    {
                        CreatingExplosion(missiles[i], i);
                        i--;
                        continue;
                    }
                }
                //in a case there was no explosion...
                double topPlayer = Canvas.GetTop(_player._image);
                double leftPlayer = Canvas.GetLeft(_player._image);

                if (topMissile <= topPlayer + 22.5 + _player.Height - 42.5 && topMissile + missiles[i]._image.Height >= topPlayer + 22.5 && leftMissile +
              missiles[i]._image.Width >= leftPlayer + 12.5 && leftMissile <= leftPlayer + 12.5 + _player.Width - 22.5) //collsion with the user
                {
                    _score -= 100 * missiles[i]._lifeTaking;
                    _player._life -= missiles[i]._lifeTaking;
                    CreatingExplosion(missiles[i], i);
                    i--;
                    if (_player._life <= 0) _player.RemoveFromCanvas(_gameBoard);
              
                    continue;
                }
                for (int j = 0; j < enemies.Count; j++)
                {
                    double topEnemySpaceship = Canvas.GetTop(enemies[j]._image);
                    double leftEnemySpaceship = Canvas.GetLeft(enemies[j]._image);

                    if (topMissile <= topEnemySpaceship + 20 + enemies[j].Height - 40 && topMissile + missiles[i]._image.Height >= topEnemySpaceship + 20 && leftMissile +
            missiles[i]._image.Width >= leftEnemySpaceship + 20 && leftMissile <= leftEnemySpaceship + 20 + enemies[j].Width - 40) //colision with enemy
                    {
                        enemies[j]._life -= missiles[i]._lifeTaking;
                        CreatingExplosion(missiles[i], i);
                        i--;
                        if (enemies[j]._life <= 0)
                        {
                            enemies[j].dead = true;
                            _score += enemies[j]._scoreForKill;
                            enemies[j].RemoveFromCanvas(_gameBoard); //remove the picture of the enemy              
                            _enemies.Remove(enemies[j]);    //remove from the list of enemies                           
                            j--;
                        }
                        if (i < 0) break;                
                    }
                }
            }
        }

        private bool CollisionWithBorder(Missile missile, double left, double top)
        {
            if (missile._image.ActualWidth + left + missile._leftMovement < _gameBoard.ActualWidth && left +
                missile._leftMovement > 0 && top + missile._topMovement > 0 && missile._image.ActualHeight + top +
                missile._topMovement < _gameBoard.ActualHeight) return true;
   
            return false;
        }

        /// <summary>
        /// Toggles the game loop timer on or off
        /// </summary>
        /// <returns>The message to be displayed in the button.</returns>
        public string ToggleTimer()
        {
            string buttonText;
            if (isGameRunning)
            {
                dt.Stop();
                buttonText = "Continue";
            }
            else
            {
                dt.Start();
                buttonText = "Pause";
            }
            isGameRunning = !isGameRunning;
            return buttonText;
        }
    }
}

