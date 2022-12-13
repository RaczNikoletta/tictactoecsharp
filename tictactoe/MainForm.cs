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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace tictactoe
{
    public partial class MainForm : Form
    {
        string path = "userdata.db";
        string cs = @"URI=file:" + Application.StartupPath + "\\userdata.db";
        SQLiteConnection con;
        SQLiteCommand cmd = new SQLiteCommand();
        SQLiteDataAdapter dr;
        string userName;
        int userScore;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string user, int score)
        {
        
            InitializeComponent();
            UserLabel.Text = user.ToString();
            ScoreLabel.Text = score.ToString();
            userName = user.ToString();
            userScore = score;

        }


        private void TictacLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            TicTacToe game = new TicTacToe(userName,userScore);
            game.ShowDialog();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // Get value from LoginForm
            using (con = new SQLiteConnection(cs))
            {
                con.Open();
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    cmd.Connection = con;
                    
                    command.CommandText =
                        "update user set isLogged = @islogged where username = @username";

                    command.Parameters.Add("islogged", DbType.Int32).Value = 1;
                    command.Parameters.Add("username", DbType.String).Value = userName;
                    command.ExecuteNonQuery();

                }
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {
            //label2.Text = username;
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            using (con = new SQLiteConnection(cs))
            {
                con.Open();
                using (SQLiteCommand command = new SQLiteCommand(con))
                {
                    cmd.Connection = con;

                    command.CommandText =
                        "update user set isLogged = @islogged where username = @username";

                    command.Parameters.Add("islogged", DbType.Int32).Value = 0;
                    command.Parameters.Add("username", DbType.String).Value = userName;
                    command.ExecuteNonQuery();

                }
            }
            LoginForm login = new LoginForm();
            this.Hide();
            login.ShowDialog();
            
        }
    }
}
