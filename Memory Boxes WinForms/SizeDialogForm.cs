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

namespace Memory_Boxes_WinForms
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
                    var panel = new Panel()
                    {
                        Margin = new Padding(0),
                        Dock = DockStyle.Fill,
                        //Name = $"{i},{j}",
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = i == 0 & j == 0 ? SystemColors.HotTrack : SystemColors.Control,
                    };
                    panel.Click += GridSizeChoicePanel_Click;
                    dialogTablePanel.Controls.Add(panel, i, j);
                }
            }
        }

        //static Regex panelNameRowColNumExtractionRegex = new Regex(@"(\d+),(\d+)", RegexOptions.Compiled);

        private void GridSizeChoicePanel_Click(object sender, EventArgs e)
        {
            //var panel = sender as Panel;

            ////var (rowMax, colMax,_,_,_,_,_) = (panelNameRowColNumExtractionRegex.Match(panel.Name).Groups as IEnumerable<Group>).Skip(1).Select(a=>int.Parse(a.Value));
            //var query = panelNameRowColNumExtractionRegex.Match(panel.Name).Groups.Cast<Group>().Skip(1).Select(gr => int.Parse(gr.Value));
            //(int colMax, int rowMax) = query;

            //GridSize = new TableLayoutPanelCellPosition(colMax + 1, rowMax + 1);

            TableLayoutPanelCellPosition ZeroBasedTableOffset = new TableLayoutPanelCellPosition(1, 1);

            GridSize = dialogTablePanel.GetPositionFromControl(sender as Panel).Add(ZeroBasedTableOffset);
            (int colMax, int rowMax) = (GridSize.Column - 1, GridSize.Row - 1);

            for(int i = 0; i < dialogTablePanel.ColumnCount; i++)
            {
                for(int j = 0; j < dialogTablePanel.RowCount; j++)
                {
                    if(i <= colMax & j <= rowMax)
                        dialogTablePanel.GetControlFromPosition(i, j).BackColor = SystemColors.HotTrack;
                    else
                        dialogTablePanel.GetControlFromPosition(i, j).BackColor = SystemColors.Control;
                }
            }
        }

        public TableLayoutPanelCellPosition GridSize { get; private set; } = new TableLayoutPanelCellPosition(1, 1);

        private void confirmSizeChoiceButton_Click(object sender, EventArgs e)
        {
            if(this.GridSize.Column * this.GridSize.Row % 2 == 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                var panels = dialogTablePanel.Controls.Cast<Panel>().Where(panel=>panel.BackColor == SystemColors.HotTrack).ToList();
                List<Color> colors = panels.Select(panel => panel.BackColor).ToList();

                foreach(var item in panels) item.BackColor = Color.FromArgb(200, 0, 0);

                this.Invalidate();

                System.Threading.Timer timer = new System.Threading.Timer(delegate
                {
                    //foreach(var (panel, color) in panels.Zip(colors))
                    //    panel.BackColor = color;
                    foreach(var panel in panels) panel.BackColor = SystemColors.HotTrack;

                    this.Invalidate();
                }, 
                null, 200, System.Threading.Timeout.Infinite);
            }
        }
    }
}
