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
        readonly Size gridSize;
        GamePanel mainGamePanel;

        public GameForm(Size size, Action showParent)
        {
            _showParent = showParent;
            this.gridSize = size;
            InitializeComponent();
        }

        //Used after closing to show main menu
        readonly Action _showParent;
        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _showParent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            mainGamePanel = new GamePanel(gridSize, this);
            this.Size = mainGamePanel.Size + _Padding;
            Utility.CenterInControl(mainGamePanel, this);
            this.CenterToScreen();

            InitializeGame();
        }

        const int CellSize = 80;
        ///<summary>Used for manually setting form's size with a centered control</summary>
        ///<remarks>Distance from the inner edges of the window is 12px</remarks>
        static readonly Size _Padding = new Size(36, 58);
    }
}
