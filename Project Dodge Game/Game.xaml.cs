using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Project_Dodge_Game.Classes;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Project_Dodge_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        const double MOVEMENT_SPEED = 10;

        object sender; 
        RoutedEventArgs e;
        GameManager _manager;
        Button _btnToggleTimer;
        string _key;
        List<string> _keys;

        public Game()
        {
            this.InitializeComponent();
            _manager = new GameManager(GameBoard, explosion);
            _btnToggleTimer = btnToggleTimer;
            _keys = new List<string>();
            
            Window.Current.CoreWindow.KeyDown += User_KeyDown;
            Window.Current.CoreWindow.KeyUp += User_KeyUp;

            _manager.InitializeGame();
            _manager.dt.Tick += GameLoop;
        }
        private void User_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            _key = "";
            _keys.RemoveAll(key => key == args.VirtualKey.ToString());
        }
        private void User_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            _key = args.VirtualKey.ToString();
            _keys.Add(_key);
        }
        private void GameLoop(object sender, object e)
        {
            HandlePlayerMovement(_manager._player, MOVEMENT_SPEED);
            if (_manager._win) ShowMessageDailog("Win!");

            if(_manager._enemies.Count > 0 && _manager.gameCounter%70 == 0) _manager.Attack();    //fire
 
            _manager.MissileMoveBehavior(_manager._missiles, _manager._enemies);
            _manager.InitializeEnemy();
            _manager.HandleEnemyMovement();
            txtScore.Text = _manager._score.ToString();
            Life.Value = _manager._player._life;
            if (Life.Value == 0) ShowMessageDailog("lost!"); 
        }

        private async void ShowMessageDailog(string result)
        {
           _manager.dt.Stop();
            await new MessageDialog($"You {result}", "Game over").ShowAsync();
            btnNewGame_Click(sender, e);        
        }

        private void HandlePlayerMovement(Player user, double speed)
        {
            double left = Canvas.GetLeft(user._image);
            double top = Canvas.GetTop(user._image);
            if (_keys.Contains("Right"))
            {
                if (user._image.ActualWidth + left + speed <= _manager._gameBoard.ActualWidth)                
                    Canvas.SetLeft(user._image, left + speed);               
            }
            if (_keys.Contains("Left"))
            {
                if (left - speed >= 0)                
                    Canvas.SetLeft(user._image, left - speed);
                
            }
            if (_keys.Contains("Up"))
            {
                if (top - speed >= 0)                
                    Canvas.SetTop(user._image, top - speed);                
            }
            if (_keys.Contains("Down"))
            {
                if (user._image.ActualHeight + top + speed <= _manager._gameBoard.ActualHeight)                
                    Canvas.SetTop(user._image, top + speed);               
            }
        } 
        
        private void btnToggleTimer_Click(object sender, RoutedEventArgs e)
        {
            if (_manager.isGameRunning) backgroundSound.Pause();
            else backgroundSound.Play();
       
            btnToggleTimer.Content = _manager.ToggleTimer();
        }
        public void btnNewGame_Click(object sender, RoutedEventArgs e)
        {       
            backgroundSound.Pause();
            _manager.dt.Stop();   
            Frame.Navigate(typeof(MainPage)); 
        }
        private void btnExit_Click(object sender, RoutedEventArgs e) => Application.Current.Exit();
  
        private void sliderVolume_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            explosion.Volume = backgroundSound.Volume = sliderVolume.Value;
        }
    }
}


 