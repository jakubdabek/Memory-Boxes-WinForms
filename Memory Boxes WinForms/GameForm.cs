using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Boxes_WinForms.Game
{
    public partial class GameForm : Form
    {
        readonly Size gridSize;
        GameBoard gameBoard;

        public GameForm(Size size)
        {
            gridSize = size;
            DoubleBuffered = true;
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Activate double buffering at the form level.  All child controls will be double buffered as well.
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                return cp;
            }
        }        

        private void GameForm_Load(object sender, EventArgs e)
        {
            gameBoard = new GameBoard(gridSize, this);
            this.Size = gameBoard.Size + _Padding;
            Utility.CenterInControl(gameBoard, this);
            this.Size += new Size(0, 30);
            gameBoard.Location += new Size(0, 30);
            this.CenterToScreen();
            //timeDisplayTimer.Start();
        }

        const int CellSize = 80;
        ///<summary>Used for manually setting form's size with a centered control</summary>
        ///<remarks>Distance from the inner edges of the window is 12px</remarks>
        static readonly Size _Padding = new Size(36, 58);

        public ImageList.ImageCollection Images => imageList.Images;        

        private void timeTimer_Tick(object sender, EventArgs e)
        {
            timeDisplayLabel.Text = gameBoard.gameDurationStopwatch.Elapsed.ToString(@"mm\:ss\.f");
        }

        private void pausePlayButton_Click(object sender, EventArgs e)
        {
            gameBoard.PauseUnpause();

            //gameBoard.Win();
        }
    }
}
