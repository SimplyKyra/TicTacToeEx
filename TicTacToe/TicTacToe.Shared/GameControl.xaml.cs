using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TicTacToe
{
    public sealed partial class GameControl : UserControl
    {
        private string CurrentPlayer { get; set; }
        private bool IsGameOver { get; set; }   

        public GameControl()
        {
            this.InitializeComponent();

            SetRandomCurrentPlayer();
            IsGameOver = false;

        }

        // HELPER Functions

        /// <summary>
        /// Switch the current player
        /// </summary>
        private void SwitchCurrentPlayer()
        {
            CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
            CurrentPlayerLabel.Text = CurrentPlayer;
        }

        /// <summary>
        /// Set the current player to a random player (X or O)
        /// </summary>
        private void SetRandomCurrentPlayer()
        {
            Random random = new Random();
            CurrentPlayer = random.Next() % 2 > 0 ? "X" : "O";
            CurrentPlayerLabel.Text = CurrentPlayer;
        }

        /// <summary>
        /// End the current game by showing the "Game Over" dialog and starting a new game.
        /// </summary>
        /// <param name="isDraw"></param>
        private void EndGame(bool isDraw)
        {
            // For the next time 
            IsGameOver = false;

            winnerTextBlock.Text = isDraw ? "Draw!" : CurrentPlayer + " Wins!!!!";
            FlyoutBase.ShowAttachedFlyout(this);

        }

        /// <summary>
        /// Check if the game is over
        /// </summary>
        private bool GameOver(out bool isDraw)
        {
            isDraw = false;

            // There are a few ways a game of tic-tac-toe can be over: Draw (all boxes are filled and no-one has won), X or O wins (there are 3 Xs or Os either vertically, horizontally or diagonally.

            // if all boxes have content, draw
            if (Button_0_0.Content != null &&
                Button_0_1.Content != null &&
                Button_0_2.Content != null &&
                Button_1_0.Content != null &&
                Button_1_1.Content != null &&
                Button_1_2.Content != null &&
                Button_2_0.Content != null &&
                Button_2_1.Content != null &&
                Button_2_2.Content != null)
            {
                isDraw = true;
                return true;
            }

            //Check 3 horizontal, 3 vertical, or 2 diagonal 
            // Check all winning scenarios + draw scenario
            return
                // Check rows
                (Button_0_0.Content != null && Button_0_1.Content != null && Button_0_2.Content != null &&
                Button_0_0.Content.ToString() == Button_0_1.Content.ToString() &&
                Button_0_0.Content.ToString() == Button_0_2.Content.ToString()) ||
                (Button_1_0.Content != null && Button_1_1.Content != null && Button_1_2.Content != null &&
                Button_1_0.Content.ToString() == Button_1_1.Content.ToString() &&
                Button_1_0.Content.ToString() == Button_1_2.Content.ToString()) ||
                (Button_2_0.Content != null && Button_2_1.Content != null && Button_2_2.Content != null &&
                Button_2_0.Content.ToString() == Button_2_1.Content.ToString() &&
                Button_2_0.Content.ToString() == Button_2_2.Content.ToString()) ||
                // Check columns
                (Button_0_0.Content != null && Button_1_0.Content != null && Button_2_0.Content != null &&
                Button_0_0.Content.ToString() == Button_1_0.Content.ToString() &&
                Button_0_0.Content.ToString() == Button_2_0.Content.ToString()) ||
                (Button_0_1.Content != null && Button_1_1.Content != null && Button_2_1.Content != null &&
                Button_0_1.Content.ToString() == Button_1_1.Content.ToString() &&
                Button_0_1.Content.ToString() == Button_2_1.Content.ToString()) ||
                (Button_0_2.Content != null && Button_1_2.Content != null && Button_2_2.Content != null &&
                Button_0_2.Content.ToString() == Button_1_2.Content.ToString() &&
                Button_0_2.Content.ToString() == Button_2_2.Content.ToString()) ||
                // Check diags
                (Button_0_0.Content != null && Button_1_1.Content != null && Button_2_2.Content != null &&
                Button_0_0.Content.ToString() == Button_1_1.Content.ToString() &&
                Button_0_0.Content.ToString() == Button_2_2.Content.ToString()) ||
                (Button_0_2.Content != null && Button_1_1.Content != null && Button_2_0.Content != null &&
                Button_0_2.Content.ToString() == Button_1_1.Content.ToString() &&
                Button_0_2.Content.ToString() == Button_2_0.Content.ToString());


        }

        // Action Events

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // If game is over nothing should happen
            if (IsGameOver)
            {
                return;
            }

            // if this button has already been filled, skip...
            Button clickedButton = sender as Button;
            if (clickedButton.Content != null)
            {
                return;
            }
            clickedButton.Content = CurrentPlayer;

            bool isDraw = false;
            if (GameOver(out isDraw))
            {
                EndGame(isDraw);
                return;
            }

            SwitchCurrentPlayer();
        }



    }

}
