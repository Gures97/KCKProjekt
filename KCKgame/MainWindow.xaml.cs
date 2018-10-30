using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static KCKgra.Console;

namespace KCKgame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            ConsoleManager.Hide();
            InitializeComponent();
        }

        public void ClearKeyBuffer()
        {
            while (Console.KeyAvailable)
                Console.ReadKey(false);
        }

        private void OpenConsole(object sender, EventArgs e)
        {
            ConsoleManager.Show();
            Console.SetWindowSize(91, 33);
            Console.CursorVisible = false;
            Map stage = new Map();
            stage.InitializeMap();
            stage.DrawConsoleWindow();
            ConsoleKeyInfo keyinfo;
            do
            {
                System.Threading.Thread.Sleep(100);
                ClearKeyBuffer();// wyczyszczenie input buffera klawiatury (nie kolejkuje sie bardzo duzo inputow na raz)
                keyinfo = Console.ReadKey();
                switch (keyinfo.Key.ToString())
                {
                    case "DownArrow":
                        stage.MovePlayerY(1);
                        break;
                    case "UpArrow":
                        stage.MovePlayerY(-1);
                        break;
                    case "LeftArrow":
                        stage.MovePlayerX(-1);
                        break;
                    case "RightArrow":
                        stage.MovePlayerX(1);
                        break;
                    case "A":
                        stage.Attack();
                        break;
                    case "S":
                        stage.UseItem();
                        break;
                    case "D":
                        stage.PickupItem();
                        break;
                    case "F":
                        stage.PreviousItem();
                        break;
                    case "G":
                        stage.NextItem();
                        break;
                    case "L":
                        stage.DeleteCurrentItem();
                        break;
                    case "X":
                        stage.GetHelp();
                            break;
                }
                stage.UpdateEnemies();
                    Console.Clear();
                    stage.DrawConsoleWindow();

                }
            while (keyinfo.Key != ConsoleKey.Escape);
            this.Close();

        }

        private void OpenWindowed(object sender, EventArgs e)
        {
            Windowed gameWindow = new Windowed();
            gameWindow.Show();
            this.Close();

        }

        private void closeProgram(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();

        }


    }
}
