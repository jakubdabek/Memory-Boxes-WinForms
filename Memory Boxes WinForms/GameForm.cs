using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Boxes_WinForms
{
    public partial class GameForm : Form
    {
        Action _showParent;
        public GameForm(Action showParent)
        {
            _showParent = showParent;
            InitializeComponent();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _showParent();
        }
    }
}
