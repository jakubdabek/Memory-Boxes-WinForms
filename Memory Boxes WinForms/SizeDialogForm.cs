using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Memory_Boxes_WinForms.Menu
{
    public partial class SizeDialogForm : Form
    {
        public SizeDialogForm()
        {
            InitializeComponent();
            this.CenterToScreen();

            for(int i = 0; i < dialogTablePanel.ColumnCount; i++)
            {
                for(int j = 0; j < dialogTablePanel.RowCount; j++)
                {
                    bool isSmall = (i + 1) * (j + 1) <= 12;
                    bool isMinimal = isSmall && ((i + 2) * (j + 1) > 14 || (i + 1) * (j + 2) > 14);
                    bool isInit = i < GridSize.Width && j < GridSize.Height;
                    
                    var panel = new Panel()
                    {
                        Margin = new Padding(0),
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = isInit ? SystemColors.HotTrack : SystemColors.Control,
                    };

                    if(!isSmall || isMinimal)
                        panel.Click += GridSizeChoicePanel_Click;
                    else
                        panel.Click += WrongGridSizeChoicePanel_Click;

                    dialogTablePanel.Controls.Add(panel, i, j);
                }
            }
        }

        static Color ErrorColor = Color.FromArgb(220, 0, 0);

        private async void WrongGridSizeChoicePanel_Click(object sender, EventArgs e)
        {
            var panel = sender as Panel;
            var color = panel.BackColor;
            panel.BackColor = ErrorColor;

            await Task.Delay(150);

            if(panel.BackColor == ErrorColor)
                panel.BackColor = color;
        }

        private void GridSizeChoicePanel_Click(object sender, EventArgs e)
        {
            TableLayoutPanelCellPosition CornerCellPosition = dialogTablePanel.GetPositionFromControl(sender as Panel);
            GridSize = new Size(CornerCellPosition.Column + 1, CornerCellPosition.Row + 1);

            for(int i = 0; i < dialogTablePanel.ColumnCount; i++)
            {
                for(int j = 0; j < dialogTablePanel.RowCount; j++)
                {
                    if(i <= CornerCellPosition.Column && j <= CornerCellPosition.Row)
                        dialogTablePanel.GetControlFromPosition(i, j).BackColor = SystemColors.HotTrack;
                    else
                        dialogTablePanel.GetControlFromPosition(i, j).BackColor = SystemColors.Control;
                }
            }
        }

        public Size GridSize { get; private set; } = new Size(4, 3);

        private async void confirmSizeChoiceButton_Click(object sender, EventArgs e)
        {
            if(this.GridSize.Width * this.GridSize.Height % 2 == 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                var panels = dialogTablePanel.Controls.Cast<Panel>().Where(panel => panel.BackColor == SystemColors.HotTrack).ToList();
                //List<Color> colors = panels.Select(panel => panel.BackColor).ToList();

                foreach(var item in panels)
                    item.BackColor = ErrorColor;
                this.Invalidate();
                dialogTablePanel.Enabled = false;

                await Task.Delay(200);

                foreach(var panel in panels)
                    panel.BackColor = SystemColors.HotTrack;
                this.Invalidate();
                dialogTablePanel.Enabled = true;
            }
        }
    }
}
