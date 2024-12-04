﻿using OOPGames.Classes.C_Gruppe.OOPGames;
using OOPGames.Classes.C_Gruppe.OOPGames.Classes.C_Gruppe.OOPGames;
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

namespace OOPGames
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IGamePlayer _CurrentPlayer = null;
        IPaintGame _CurrentPainter = null;
        IGameRules _CurrentRules = null;
        IGamePlayer _CurrentPlayer1 = null;
        IGamePlayer _CurrentPlayer2 = null;

        System.Windows.Threading.DispatcherTimer _PaintTimer = null;

        public MainWindow()
        {
            //REGISTER YOUR CLASSES HERE
            //Painters
            OOPGamesManager.Singleton.RegisterPainter(new X_TicTacToePaint());
            OOPGamesManager.Singleton.RegisterPainter(new D_PainterTikTokToo());
            OOPGamesManager.Singleton.RegisterPainter(new B_Paint_TTT());
            OOPGamesManager.Singleton.RegisterPainter(new B_Painter_BV());
            OOPGamesManager.Singleton.RegisterPainter(new C_Painter());
            OOPGamesManager.Singleton.RegisterPainter(new C_MemoryGamePainter());
            OOPGamesManager.Singleton.RegisterPainter(new FlappyPainter());
            OOPGamesManager.Singleton.RegisterPainter(new oX_TicTacToePaint()); 
            OOPGamesManager.Singleton.RegisterPainter(new omSnakePaint()); 
            OOPGamesManager.Singleton.RegisterPainter(new omm_BugPaint());
            OOPGamesManager.Singleton.RegisterPainter(new A_Painter());
            OOPGamesManager.Singleton.RegisterPainter(new G_Painter());
            //Rules
            OOPGamesManager.Singleton.RegisterRules(new X_TicTacToeRules());
            OOPGamesManager.Singleton.RegisterRules(new D_RulesTikTokToo());
            OOPGamesManager.Singleton.RegisterRules(new B_Rules_TTT());
            OOPGamesManager.Singleton.RegisterRules(new B_Rules_BV());
            OOPGamesManager.Singleton.RegisterRules(new C_Rules());
            OOPGamesManager.Singleton.RegisterRules(new C_MemoryGameRules());
            OOPGamesManager.Singleton.RegisterRules(new FlappyRules());
            OOPGamesManager.Singleton.RegisterRules(new oX_TicTacToeRules());
            OOPGamesManager.Singleton.RegisterRules(new omSnakeRules());
            OOPGamesManager.Singleton.RegisterRules(new A_Rules());
            OOPGamesManager.Singleton.RegisterRules(new G_Bug_Rules());
            //Players
            OOPGamesManager.Singleton.RegisterPlayer(new X_TicTacToeHumanPlayer());
            OOPGamesManager.Singleton.RegisterPlayer(new X_TicTacToeComputerPlayer());
            OOPGamesManager.Singleton.RegisterPlayer(new D_HumanPTikTokToo());
            OOPGamesManager.Singleton.RegisterPlayer(new D_ComputerPTikTokToo());
            OOPGamesManager.Singleton.RegisterPlayer(new B_HumanPlayer_TTT());
            OOPGamesManager.Singleton.RegisterPlayer(new B_ComputerPlayer_TTT());
            OOPGamesManager.Singleton.RegisterPlayer(new B_HumanPlayer_BV());
            OOPGamesManager.Singleton.RegisterPlayer(new B_ComputerPlayer_BV());
            OOPGamesManager.Singleton.RegisterPlayer(new B_ComputerPlayerSchlau_TTT());
            OOPGamesManager.Singleton.RegisterPlayer(new C_Computer());
            OOPGamesManager.Singleton.RegisterPlayer(new C_Human());
            OOPGamesManager.Singleton.RegisterPlayer(new C_HumanMemoryPlayer());
            OOPGamesManager.Singleton.RegisterPlayer(new C_ComputerMemoryPlayer());
            OOPGamesManager.Singleton.RegisterPlayer(new FlappyPlayer());
            OOPGamesManager.Singleton.RegisterPlayer(new oX_TicTacToeHumanPlayer());
            OOPGamesManager.Singleton.RegisterPlayer(new oX_TicTacToeComputerPlayer()); 
            OOPGamesManager.Singleton.RegisterPlayer(new omSnakeHumanPlayer());
            OOPGamesManager.Singleton.RegisterPlayer(new A_Human_Player());
            OOPGamesManager.Singleton.RegisterPlayer(new A_Computer_Player());

            OOPGamesManager.Singleton.RegisterPlayer(new G_Bug());
            OOPGamesManager.Singleton.RegisterPlayer(new G_Apple());

            InitializeComponent();
            PaintList.ItemsSource = OOPGamesManager.Singleton.Painters;
            Player1List.ItemsSource = OOPGamesManager.Singleton.Players;
            Player2List.ItemsSource = OOPGamesManager.Singleton.Players;
            RulesList.ItemsSource = OOPGamesManager.Singleton.Rules;

            _PaintTimer = new System.Windows.Threading.DispatcherTimer();
            _PaintTimer.Interval = new TimeSpan(0, 0, 0, 0, 40);
            _PaintTimer.Tick += _PaintTimer_Tick;
            _PaintTimer.Start();
        }

        private void _PaintTimer_Tick(object sender, EventArgs e)
        {
            if (_CurrentPainter != null &&
                _CurrentRules != null)
            {
                if (_CurrentPainter is IPaintGame2 &&
                    _CurrentRules.CurrentField != null &&
                    _CurrentRules.CurrentField.CanBePaintedBy(_CurrentPainter))
                {
                    ((IPaintGame2)_CurrentPainter).TickPaintGameField(PaintCanvas, _CurrentRules.CurrentField);
                }

                if (_CurrentRules is IGameRules2)
                {
                    ((IGameRules2)_CurrentRules).TickGameCall();
                }

                if (_CurrentRules is IGameRules3 &&
                                ((IGameRules3)_CurrentRules).StatusBar() != null)
                {
                    Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
                }
            }
        }

        private void StartGame_Click(object sender, RoutedEventArgs e)
        {
            List<IGamePlayer> activePlayers = new List<IGamePlayer>();
            _CurrentPlayer1 = null;
            if (Player1List.SelectedItem is IGamePlayer)
            {
                _CurrentPlayer1 = ((IGamePlayer)Player1List.SelectedItem).Clone();
                _CurrentPlayer1.SetPlayerNumber(1);
                activePlayers.Add(_CurrentPlayer1);
            }
            _CurrentPlayer2 = null;
            if (Player2List.SelectedItem is IGamePlayer)
            {
                _CurrentPlayer2 = ((IGamePlayer)Player2List.SelectedItem).Clone();
                _CurrentPlayer2.SetPlayerNumber(2);
                activePlayers.Add(_CurrentPlayer2);
            }

            _CurrentPlayer = null;
            _CurrentPainter = PaintList.SelectedItem as IPaintGame;
            _CurrentRules = RulesList.SelectedItem as IGameRules;

            OOPGamesManager.Singleton.RegisterActivePlayers(activePlayers);
            OOPGamesManager.Singleton.RegisterActivePainter(_CurrentPainter);
            OOPGamesManager.Singleton.RegisterActiveRules(_CurrentRules);

            if (_CurrentRules is IGameRules2)
            {
                ((IGameRules2)_CurrentRules).StartedGameCall();
            }

            if (_CurrentPainter != null &&
                _CurrentRules != null && _CurrentRules.CurrentField.CanBePaintedBy(_CurrentPainter))
            {
                _CurrentPlayer = _CurrentPlayer1;
                if (_CurrentRules is IGameRules3 &&
                                ((IGameRules3)_CurrentRules).StatusBar() != null)
                {
                    Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
                }
                else
                {
                    Status.Text = "Game startet!";
                    Status.Text = "Player " + _CurrentPlayer.PlayerNumber + "'s turn!";
                }

                _CurrentRules.ClearField();
                _CurrentPainter.PaintGameField(PaintCanvas, _CurrentRules.CurrentField);
                DoComputerMoves();
            }
        }

        private void DoComputerMoves()
        {
            int winner = _CurrentRules.CheckIfPLayerWon();
            if (_CurrentRules is IGameRules3 &&
                ((IGameRules3)_CurrentRules).StatusBar() != null)
            {
                Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
            }
            else if (winner > 0)
            {
                Status.Text = "Player" + winner + " Won!";
            }

            if (winner <= 0)
            {
                while (_CurrentRules.MovesPossible &&
                       winner <= 0 &&
                       _CurrentPlayer is IComputerGamePlayer)
                {
                    IPlayMove pm = ((IComputerGamePlayer)_CurrentPlayer).GetMove(_CurrentRules.CurrentField);
                    if (pm != null)
                    {
                        _CurrentRules.DoMove(pm);
                        _CurrentPainter.PaintGameField(PaintCanvas, _CurrentRules.CurrentField);
                        _CurrentPlayer = _CurrentPlayer == _CurrentPlayer1 ? _CurrentPlayer2 : _CurrentPlayer1;

                        if (_CurrentRules is IGameRules3 &&
                                ((IGameRules3)_CurrentRules).StatusBar() != null)
                        {
                            Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
                        }
                        else
                        {
                            Status.Text = "Player " + _CurrentPlayer.PlayerNumber + "'s turn!";
                        }
                    }

                    winner = _CurrentRules.CheckIfPLayerWon();
                    if (winner > 0)
                    {
                        if (_CurrentRules is IGameRules3 &&
                                ((IGameRules3)_CurrentRules).StatusBar() != null)
                        {
                            Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
                        }
                        else
                        {
                            Status.Text = "Player " + winner + " Won!";
                        }
                    }
                }
            }
        }

        private void PaintCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int winner = _CurrentRules.CheckIfPLayerWon();
            if (_CurrentRules is IGameRules3 &&
                ((IGameRules3)_CurrentRules).StatusBar() != null)
            {
                Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
            }
            else if (winner > 0)
            {
                Status.Text = "Player" + winner + " Won!";
            }

            if (winner <= 0)
            {
                if (_CurrentRules.MovesPossible &&
                    _CurrentPlayer is IHumanGamePlayer)
                {
                    IPlayMove pm = ((IHumanGamePlayer)_CurrentPlayer).GetMove(new ClickSelection((int)e.GetPosition(PaintCanvas).X,
                        (int)e.GetPosition(PaintCanvas).Y, (int)e.ChangedButton), _CurrentRules.CurrentField);
                    if (pm != null)
                    {
                        _CurrentRules.DoMove(pm);
                        _CurrentPainter.PaintGameField(PaintCanvas, _CurrentRules.CurrentField);
                        _CurrentPlayer = _CurrentPlayer == _CurrentPlayer1 ? _CurrentPlayer2 : _CurrentPlayer1;

                        if (_CurrentRules is IGameRules3 &&
                                ((IGameRules3)_CurrentRules).StatusBar() != null)
                        {
                            Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
                        }
                        else
                        {
                            Status.Text = "Player " + _CurrentPlayer.PlayerNumber + "'s turn!";
                        }
                    }

                    DoComputerMoves();
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _PaintTimer.Tick -= _PaintTimer_Tick;
            _PaintTimer.Stop();
            _PaintTimer = null;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_CurrentRules == null) return;
            int winner = _CurrentRules.CheckIfPLayerWon();
            if (_CurrentRules is IGameRules3 &&
                ((IGameRules3)_CurrentRules).StatusBar() != null)
            {
                Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
            }
            else if (winner > 0)
            {
                Status.Text = "Player" + winner + " Won!";
            }

            if (winner <= 0)
            {
                if (_CurrentRules.MovesPossible &&
                    _CurrentPlayer is IHumanGamePlayer)
                {
                    IPlayMove pm = ((IHumanGamePlayer)_CurrentPlayer).GetMove(new KeySelection(e.Key), _CurrentRules.CurrentField);
                    if (pm != null)
                    {
                        _CurrentRules.DoMove(pm);
                        _CurrentPlayer = _CurrentPlayer == _CurrentPlayer1 ? _CurrentPlayer2 : _CurrentPlayer1;

                        if (_CurrentRules is IGameRules3 &&
                                ((IGameRules3)_CurrentRules).StatusBar() != null)
                        {
                            Status.Text = ((IGameRules3)_CurrentRules).StatusBar();
                        }
                        else
                        {
                            Status.Text = "Player " + _CurrentPlayer.PlayerNumber + "'s turn!";
                        }
                    }

                    DoComputerMoves();
                }
            }
        }
    }
}
