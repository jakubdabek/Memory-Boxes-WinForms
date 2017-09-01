using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using Memory_Boxes_WinForms.Game;

namespace Memory_Boxes_WinForms.Menu
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            titleRainbowTimer.Interval = 200;

            //SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            //typeof(Panel).InvokeMember("DoubleBuffered",
            //BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            //null, TitlePanel, new object[] { true });
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

        private void Form1_Load(object sender, EventArgs e)
        {
            Rectangle textRectF;
            using(Graphics gr = titlePanel.CreateGraphics())
                textRectF = new Rectangle(titleInitLocation, Size.Ceiling(gr.MeasureString(titleText, titleFont)));

            titlePanel.Width = textRectF.Width + 20;

            Utility.CenterInControl(titlePanel, this, Utility.CenterStyle.Horizontal);
            Utility.CenterInControl(startButton, titlePanel, Utility.CenterStyle.Horizontal);

            titleInitLocation = Utility.GetCenterPositionInControl(
                titlePanel,
                Rectangle.Round(textRectF),
                Utility.CenterStyle.Horizontal
                ).Location;
        }

        GameForm gameForm;

        private void startButton_Click(object sender, EventArgs e)
        {
            using(SizeDialogForm dialog = new SizeDialogForm())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    var gridSize = dialog.GridSize;
                    this.Visible = false;
                    gameForm = new GameForm(gridSize, () => this.Visible = true);
                    gameForm.Show();
                }
            }
        }


    #region Rainbow text init

        volatile bool nextRainbowStep = true;

        string titleText = "Memory Boxes";
        Point titleInitLocation = new Point(10, 50);
        Font titleFont = new Font("Microsoft Sans Serif", 30, FontStyle.Bold);

        RainbowUtilities.TextDrawer TextDrawer;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("paint!");
            if(!titleRainbowTimer.Enabled)
                titleRainbowTimer.Start();

            if(TextDrawer is null)
            {                
                TextDrawer = new RainbowUtilities.TextDrawer(titleText, e.Graphics, titleFont, titleInitLocation);
            }

            TextDrawer.Draw(e.Graphics, nextRainbowStep);            
            nextRainbowStep = false;
        }

        private void titleRainbowTimer_Tick(object sender, EventArgs e)
        {
            nextRainbowStep = true;
            titlePanel.Refresh();
        }

    #endregion

    }
}
