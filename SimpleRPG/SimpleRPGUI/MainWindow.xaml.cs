using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using Engine.EventArgs;
using Engine.ViewModels;
using Engine.Models;

namespace SimpleRPGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession = new();

        public MainWindow()
        {
            InitializeComponent();

            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession;
        }

        private void OnClick_Move(object sender, RoutedEventArgs e)
        {
            _gameSession.Move((sender as Button).Content.ToString());
        }

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
        {
            _gameSession.AttackCurrentMonster();
        }

        private void GetKeyboardInput(object sender, KeyEventArgs e)
        {
            switch (e.Key)
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

        private void OnClick_OpenTradeScreen(object sender, RoutedEventArgs e)
        {
            TradeScreen tradeScreen = new TradeScreen();
            tradeScreen.Owner = this;
            tradeScreen.DataContext = _gameSession;
            tradeScreen.ShowDialog();
        }

        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
        {
            _gameSession.UseCurrentConsumable();
        }

        private void OnClick_Craft(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItem(recipe);
        }
    }
}
