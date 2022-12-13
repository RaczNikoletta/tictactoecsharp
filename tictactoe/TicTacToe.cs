using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        List<Button> buttons;
        string userName;
        int userScore;
        bool playerMoved = false;


        public TicTacToe()
        {
            InitializeComponent();
            RestartGame();
        }

        public TicTacToe(string user, int score)
        { 
            userName = user;
            userScore = score;
            InitializeComponent();
            RestartGame();


        }
        private void RestartGame()
        {
            //set default values for all button
            buttons = new List<Button> {button1,button2,
                button3, button4, button5, button6, button7, button8, button9};

            foreach (Button x in buttons)
            {
                x.Enabled = true;
                x.Text = "?";
                x.BackColor = DefaultBackColor;
                    
            }
           
            
           
        }

        private void CPUMove(object sender, EventArgs e)
        {
            if (buttons.Count > 0)
            {

                int index = random.Next(buttons.Count);
                buttons[index].Enabled = false;
                currentPlayer = Player.O;
                buttons[index].Text = currentPlayer.ToString();
                buttons[index].BackColor = Color.OrangeRed;
                buttons.RemoveAt(index);
                CheckGame();
            }
            playerMoved = false;
            timer1.Stop();

        }

        private void PlayerClickButton(object sender, EventArgs e)
        {
            
            var button = (Button)sender;
            if (!playerMoved)
            {
                playerMoved = true;
                currentPlayer = Player.X;
                button.Text = currentPlayer.ToString();
                button.Enabled = false;
                button.BackColor = Color.AliceBlue;
                buttons.Remove(button);
                CheckGame();
                timer1.Start();
            }
            else {
                MessageBox.Show("CPU moves", "Please wait for CPU");
            }
        }

        private void RestartGame(object sender, EventArgs e)
        {
            RestartGame();
        }

        private void CheckGame()
        {
            if (button1.Text == "X" && button2.Text == "X" && button3.Text == "X"
                || button4.Text == "X" && button5.Text == "X" && button6.Text == "X"
                || button7.Text == "X" && button8.Text == "X" && button9.Text == "X"
                || button1.Text == "X" && button4.Text == "X" && button7.Text == "X"
                || button2.Text == "X" && button5.Text == "X" && button8.Text == "X"
                || button3.Text == "X" && button6.Text == "X" && button9.Text == "X"
                || button1.Text == "X" && button5.Text == "X" && button9.Text == "X"
                || button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                timer1.Stop();
                MessageBox.Show("Player Wins", "Player wins");
                playerWinCount++;
                label1.Text = "Player wins: " + playerWinCount;
                RestartGame();
            }

            else if (button1.Text == "O" && button2.Text == "O" && button3.Text == "O"
            || button4.Text == "O" && button5.Text == "O" && button6.Text == "O"
            || button7.Text == "O" && button8.Text == "O" && button9.Text == "O"
            || button1.Text == "O" && button4.Text == "O" && button7.Text == "O"
            || button2.Text == "O" && button5.Text == "O" && button8.Text == "O"
            || button3.Text == "O" && button6.Text == "O" && button9.Text == "O"
            || button1.Text == "O" && button5.Text == "O" && button9.Text == "O"
            || button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            { 
                timer1.Stop();
            MessageBox.Show("CPU Wins", "CPU Wins");
            CPUWinCount++;
            label2.Text = "CPU wins: " + CPUWinCount;
            RestartGame();


        }
        
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            // save score to database

            string cs = @"URI=file:" + Application.StartupPath + "\\userdata.db";
            SQLiteConnection con;
            SQLiteCommand cmd = new SQLiteCommand();
            using (con = new SQLiteConnection(cs))
            {
                con.Open();
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    cmd.Connection = con;

                    command.CommandText =
                        @"update user set score = @score where username = @username";

                    command.Parameters.Add("score", DbType.Int32).Value = userScore+playerWinCount;
                    command.Parameters.Add("username", DbType.String).Value = userName;
                    command.ExecuteNonQuery();

                }
            }

            MainForm main = new MainForm(userName, userScore + playerWinCount);
            this.Hide();
            main.ShowDialog();
            


        }
    }
}
