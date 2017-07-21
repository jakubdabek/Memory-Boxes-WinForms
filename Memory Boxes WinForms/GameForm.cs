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
        Size gridSize;
        GamePanel mainGamePanel;

        public GameForm(Size size, Action showParent)
        {
            _showParent = showParent;
            this.gridSize = size;
            InitializeComponent();
            //mainGamePanel = new GamePanel(size, this);
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _showParent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            //this.Size = new Size(gridSize.Width * CellSize, gridSize.Height * CellSize) + _Padding;
            mainGamePanel = new GamePanel(gridSize, this);
            this.Size = mainGamePanel.Size + _Padding;
            Utility.CenterInControl(mainGamePanel, this);
            this.CenterToScreen();
        }

        const int CellSize = 80;
        static readonly Size _Padding = new Size(36, 58);
    }
}
