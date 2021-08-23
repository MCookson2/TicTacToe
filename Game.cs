using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    public partial class MainWindow
    {
        public class Game
        {
            public static bool First { get; set; } = true;
            public static int[] Board { get; set; } = new int[9];

            public static void Turn(object sender, RoutedEventArgs e)
            {
                Button myButton = (Button)sender;
                int index = int.Parse(myButton.Name.Substring(1));
                bool hasWon;

                if (First)
                {
                    myButton.Content = "X";
                    myButton.Foreground = Brushes.Red;
                    myButton.IsEnabled = false;
                    First = false;
                    Board[index] = 1;
                    hasWon = CheckWinner();
                    if (hasWon)
                    {
                        MessageBox.Show("CROSS WINS!");
                        First = true;
                        Restart();
                    }
                }
                    
                else
                {
                    myButton.Content = "O";
                    myButton.Foreground = Brushes.Blue;
                    myButton.IsEnabled = false;
                    First = true;
                    Board[index] = 2;
                    hasWon = CheckWinner();
                    if (hasWon)
                    {
                        MessageBox.Show("NOUGHTS WINS!");
                        First = true;
                        Restart();
                    }
                }

                if (!hasWon && Board.All(a => a != 0))
                {
                    MessageBox.Show("DRAW!");
                    First = true;
                    Restart();
                }
            }

            public static List<List<int>> SplitToSublists(int[] source)
            {
                return source
                         .Select((x, i) => new { Index = i, Value = x })
                         .GroupBy(x => x.Index / 3)
                         .Select(x => x.Select(v => v.Value).ToList())
                         .ToList();
            }

            public static bool CheckWinner()
            {
                List<List<int>> vals = SplitToSublists(Board);

                // Horizontal checks
                if (vals[0][0] == vals[0][1] && vals[0][1] == vals[0][2] && vals[0][0] != 0)
                    return true;

                else if (vals[1][0] == vals[1][1] && vals[1][1] == vals[1][2] && vals[1][0] != 0)
                    return true;

                else if (vals[2][0] == vals[2][1] && vals[2][1] == vals[2][2] && vals[2][0] != 0)
                    return true;

                // Vertical Checks
                else if (vals[0][0] == vals[1][0] && vals[1][0] == vals[2][0] && vals[0][0] != 0)
                    return true;

                else if (vals[0][1] == vals[1][1] && vals[1][1] == vals[2][1] && vals[0][1] != 0)
                    return true;

                else if (vals[0][2] == vals[1][2] && vals[1][2] == vals[2][2] && vals[0][2] != 0)
                    return true;

                // Diagonal Checks
                else if (vals[0][0] == vals[1][1] && vals[1][1] == vals[2][2] && vals[0][0] != 0)
                    return true;

                else if (vals[0][2] == vals[1][1] && vals[1][1] == vals[2][0] && vals[0][2] != 0)
                    return true;

                else return false;
            }

            public static void Restart()
            {
                Array.Clear(Board, 0, Board.Length);
                List<Button> buttons = new List<Button>();
                MainWindow wnd = (MainWindow)Application.Current.MainWindow;

                for (int i = 0; i <= 8; i++)
                {
                    string buttonName = $"b{i}";
                    buttons.Add((Button)wnd.FindName(buttonName));
                }

                foreach (Button button in buttons)
                {
                    button.Content = string.Empty;
                    button.IsEnabled = true;
                }
            }
        }
    }
}
