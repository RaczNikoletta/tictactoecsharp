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
    public partial class MainForm : Form
    {
        LoginForm ownerForm = null;
        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(string user)
        {
        
            InitializeComponent();
            var test = user;
            label2.Text = "szoveg";
            label2.Text = user.ToString();
        }


        private void TictacLabel_Click(object sender, EventArgs e)
        {
            //label2.Text = username;
            //this.Hide();
            TicTacToe game = new TicTacToe();
            //game.Show();
        }

        public MainForm(LoginForm ownerForm)
        {
            InitializeComponent();
            this.ownerForm = ownerForm;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Get value from LoginForm
           

        }

        private void label2_Click(object sender, EventArgs e)
        {
            //label2.Text = username;
        }
    }
}
