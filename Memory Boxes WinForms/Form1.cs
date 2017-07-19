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

namespace Memory_Boxes_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();            
            titleRainbowTimer.Interval = 200;

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
            using(Graphics gr = TitlePanel.CreateGraphics())
                textRectF = new Rectangle(titleStartLocation, Size.Ceiling(gr.MeasureString(titleText, titleFont)));

            TitlePanel.Width = textRectF.Width + 20;

            Utility.CenterInControl(TitlePanel, this, Utility.CenterStyle.Horizontal);
            Utility.CenterInControl(startButton, TitlePanel, Utility.CenterStyle.Horizontal);

            titleStartLocation = Utility.GetCenterPositionInControl(
                TitlePanel, 
                Rectangle.Round(textRectF), 
                Utility.CenterStyle.Horizontal
                ).Location;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            using(SizeDialogForm dialog = new SizeDialogForm())
            {
                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    var gridSize = dialog.GridSize;
                }
            }
        }
    }
}
