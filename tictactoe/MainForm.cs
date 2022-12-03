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
        public MainForm()
        {
            InitializeComponent();
        }

        private void TictacLabel_Click(object sender, EventArgs e)
        {
            this.Hide();
            TicTacToe game = new TicTacToe();
            game.Show();
        }
    }
}
