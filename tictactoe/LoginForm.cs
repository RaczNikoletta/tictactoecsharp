using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace tictactoe
{
    public partial class LoginForm : Form
    {
        //path of database
       
        string path = "userdata.db";
        string cs = @"URI=file:" + Application.StartupPath + "\\userdata.db";
        SQLiteConnection con;
        SQLiteCommand cmd;
        SQLiteDataAdapter dr;
        public LoginForm()
        { 


            InitializeComponent();



        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Read credential

            string password = PasswordText.Text;
            string username = UserText.Text;

            //search for user - inejction protection
            con = new SQLiteConnection(cs);
            con.Open();
            string stm = "SELECT * from user";
            var cmd = new SQLiteCommand(stm, con);
            var dr = cmd.ExecuteReader();
            bool exists = false;

            while (dr.Read())
            {
                try
                {
                    if (username == dr.GetString(1))
                    {
                        exists = true;
                        if (password == dr.GetString(2))
                        {
                            //authentication sucessfull
                            var main = new MainForm();

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
    }
}