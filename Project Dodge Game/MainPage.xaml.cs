using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Project_Dodge_Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {  
        public MainPage() => this.InitializeComponent();   

        private void btnStart_Click(object sender, RoutedEventArgs e) => Frame.Navigate(typeof(Game));
  
        private void btnExit_Click(object sender, RoutedEventArgs e) => Application.Current.Exit();
    
        private void btnStart_PointerExited(object sender, PointerRoutedEventArgs e)=>       
            btnStart.Background = new SolidColorBrush(Colors.Orange);
        
        private void btnExit_PointerExited(object sender, PointerRoutedEventArgs e)=>        
            btnExit.Background = new SolidColorBrush(Colors.Orange);
        

        private void btnInstructions_Click(object sender, RoutedEventArgs e) => ShowMessageDailog();
   
        private async void ShowMessageDailog()
        {
            await new MessageDialog("You need to dodge all the missiles and target them to the enemies themselves.\nIf you managed to survive more than 5 minutes" +
                " and if there is no enemy nor missile left - you win! remember- as long as there is a missile or an enemy - the game goes on and new enemies will" +
                " keep appearing! Use only the arrow keys in your keyboard.\n\nGood luck!").ShowAsync();
        }
    }
}
