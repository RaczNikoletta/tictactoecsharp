using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;
using System.Xml;
using static System.Formats.Asn1.AsnWriter;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
        MainForm main;
        SQLiteCommand cmd;
        SQLiteDataAdapter dr;
        bool isLogged = false;
        public LoginForm()
        {
            InitializeComponent();
            try
            {
                con = new SQLiteConnection(cs);
                con.Open();
                string stm = "CREATE TABLE IF NOT EXISTS user(id INTEGER PRIMARY KEY AUTOINCREMENT, username TEXT (51), password TEXT(50), score INTEGER DEFAULT(0), isLogged INTEGER DEFAULT(0));";
                var cmd = new SQLiteCommand(stm, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            { 
                MessageBox.Show(e.Message);
            }

        }
        private void label3_Click(object sender, EventArgs e)
        {
            RegisterForm reg = new RegisterForm();
            this.Hide();
            reg.ShowDialog();

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
                            main = new MainForm(username,score);
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
                
                }
            con.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (!exists)
                {
                    MessageBox.Show("Invalid username");
                }
                else {
                this.Hide();
                main.ShowDialog();
              
               
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

        private void LoginForm_Load(object sender, EventArgs e)
        {
            con = new SQLiteConnection(cs);
            con.Open();
            string stm = "SELECT username, password, score, isLogged from user";
            var cmd = new SQLiteCommand(stm, con);
            var dr = cmd.ExecuteReader();


            while (dr.Read())
            {
                try
                {
                    if (dr.GetInt32(3) == 1) /*check logged user*/
                    {
                        isLogged = true;
                        var score = dr.GetInt32(2);
                        main = new MainForm(dr.GetString(0), dr.GetInt32(2));




                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show("Invalid credintials" + ex.Message);
                }

            }
            con.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (isLogged)
            {
                // Show the main form
                
                main.ShowDialog();
                this.Close();

            }
        }
    }
}