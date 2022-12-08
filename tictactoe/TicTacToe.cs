using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tictactoe
{
    public partial class TicTacToe : Form
    {
        public enum Player
        { 
        
            X, O
        }

        Player currentPlayer;
        Random random = new Random();
        int playerWinCount = 0;
        int CPUWinCount = 0;


        public TicTacToe()
        {
            InitializeComponent();
            RestartGame();
        }

        private void RestartGame()
        {
            List<Button> buttons = new List<Button> {button1,button2,
                button3, button4, button5, button6, button7, button8, button9};

            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.Text = "?";
                x.BackColor = Color.White;
            }
           
            
           
        }
    }
}
