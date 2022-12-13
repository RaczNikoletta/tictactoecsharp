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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace tictactoe
{
    public partial class RegisterForm : Form
    {
        bool exists = false;
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            //TODO: check if user is exists
            exists = false;
            string cs = @"URI=file:" + Application.StartupPath + "\\userdata.db";
            SQLiteConnection con;
            con = new SQLiteConnection(cs);
            con.Open();
            //UserNameTextBox.Text;
            SQLiteCommand selectSQL = new SQLiteCommand("SELECT username from user", con);
            var dr = selectSQL.ExecuteReader();


            while (dr.Read())
            {
                try
                {
                    if (dr.GetString(0) == textBox1.Text.ToString()) /*user is already exists*/
                    {
                        exists = true;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid credintials" + ex.Message);
                }



            }


            if (!exists)
            {
                SQLiteCommand insertSQL = new SQLiteCommand("INSERT INTO user (username, password) VALUES (@username, @password)", con);
                insertSQL.Parameters.Add("username", DbType.String).Value = textBox1.Text.ToString();
                insertSQL.Parameters.Add("password", DbType.String).Value = textBox2.Text.ToString();
                //insertSQL.Parameters.Add();
                try
                {
                    insertSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


                this.Hide();
                LoginForm log = new LoginForm();
                log.ShowDialog();


            }
            else
            {
                MessageBox.Show("Username is already taken");
            }
        }
    }
}
