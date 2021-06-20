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
using Engine.EventArgs;
using Engine.ViewModels;

namespace SimpleRPGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameSession _gameSession;

        public MainWindow()
        {
            InitializeComponent();

            _gameSession = new GameSession();

            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession;
        }

        private void OnClick_Move(object sender, RoutedEventArgs e)
        {
            _gameSession.Move((sender as Button).Content.ToString());
        }

        private void GetKeyboardInput(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Up:
                    _gameSession.Move("North");
                    break;

                case Key.Left:
                    _gameSession.Move("West");
                    break;

                case Key.Right:
                    _gameSession.Move("East");
                    break;

                case Key.Down:
                    _gameSession.Move("South");
                    break;

                default:
                    break;
            }
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }
    }
}
