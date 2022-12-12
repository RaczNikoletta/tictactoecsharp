using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Xml;

namespace tictactoe
{
    public partial class LoginForm : Form
    {
        //path of database
        //felhaszn objektum
        public string username = "";
        public string password = "";
        public int scorepass = 0;
        string path = "userdata.db";
        string cs = @"URI=file:" + Application.StartupPath + "\\userdata.db";
        SQLiteConnection con;
        SQLiteCommand cmd;
        SQLiteDataAdapter dr;
        public LoginForm()
        {
           /* con = new SQLiteConnection(cs);
            con.Open();
            string stm = "SELECT username, password, score, isLogged from user";
            var cmd = new SQLiteCommand(stm, con);
            var dr = cmd.ExecuteReader();
            bool isLogged = false;

            while (dr.Read())
            {
                try
                {
                    if (dr.GetInt32(3) == 1) /*check logged user*/
                    /*{
                            isLogged = true;
                            var score = dr.GetInt32(2);
                            MainForm main = new MainForm(dr.GetString(0), dr.GetInt32(3));
                            // Show the main form
                            this.Hide();
                            main.Show();



                        }
                    
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Invalid credintials" + ex.Message);
                }
                
            }*/
            InitializeComponent();
        }
        private void label3_Click(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Read credential

            //search for user - inejction protection
            con = new SQLiteConnection(cs);
            con.Open();
            string stm = "SELECT username, password, score from user";
            var cmd = new SQLiteCommand(stm, con);
            var dr = cmd.ExecuteReader();
            bool exists = false;
            username = UserText.Text;
            password = PasswordText.Text;

            while (dr.Read())
            {
                try
                {
                    if (username == dr.GetString(0))
                    {
                        exists = true;
                        if (password == dr.GetString(1))
                        {
                            //authentication sucessfull


                            //Pass values to Mainform
                            var score = dr.GetInt32(2);
                            MainForm main = new MainForm(username,score);
                            // Show the main form
                            this.Hide();
                            main.Show();
                 


                        }
                        else
                        {
                            MessageBox.Show("Invalid password");
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Invalid credintials" + ex.Message);
                }
                if (!exists)
                {
                    MessageBox.Show("Invalid username");
                }
            }






        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}