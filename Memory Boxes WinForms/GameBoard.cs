using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;

namespace Memory_Boxes_WinForms.Game
{
    [DesignerCategory("")]
    partial class GameBoard : TableLayoutPanel
    {
        public GameBoard(Control parent) => Form = (Parent = parent) as GameForm ?? FindForm() as GameForm;
        public GameBoard(Size boardSize, Control parent) : this(boardSize.Width, boardSize.Height, parent) { }

        public readonly IReadOnlyList<Color> availableColors = new List<Color>
        {
            Color.Black,
            Color.Red,
            Color.Blue,
            Color.Orange,
            Color.Green,
            Color.Purple,
        };

        HashSet<KeyValuePair<int, Color>> set = new HashSet<KeyValuePair<int, Color>>();
        GameForm Form { get; set; }

        public GameBoard(int columnCount, int rowCount, Control parent) : this(parent)
        {
            ColumnCount = columnCount;
            RowCount = rowCount;
            ColumnStyles.Add(SizeType.Percent, 100f / columnCount, count: columnCount);
            RowStyles.Add(SizeType.Percent, 100f / rowCount, count: rowCount);
            //CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Size = new Size(columnCount * cellSize, rowCount * cellSize);

            Random rand = new Random();

            for(int i = 0; i < rowCount * columnCount; i++)
            {
                Controls.Add(new PictureBox()
                {
                    Dock = DockStyle.Fill,
                    Image =
                        (Form.Images[rand.Next(Form.Images.Count)].Clone() as Image)
                        .ReplaceColor(Color.Black, availableColors[rand.Next(availableColors.Count)]),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BorderStyle = BorderStyle.FixedSingle,
                });
            }

            //InitializeComponents();
        }
        const int cellSize = 80;
    }

}