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
            Color.Pink,
            Color.Orange,
            Color.Green,
            Color.Purple,
            Color.Turquoise,
        };

        HashSet<KeyValuePair<int, Color>> set = new HashSet<KeyValuePair<int, Color>>();
        GameForm Form { get; set; }

        public GameBoard(int columnCount, int rowCount, Control parent) : this(parent)
        {
            ColumnCount = columnCount;
            RowCount = rowCount;
            ColumnStyles.Add(SizeType.Percent, 100f / ColumnCount, count: ColumnCount);
            RowStyles.Add(SizeType.Percent, 100f / RowCount, count: RowCount);
            
            //CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Size = new Size(ColumnCount * CellSize, RowCount * CellSize);

            Random rand = new Random();

            while(set.Count * 2 < RowCount * ColumnCount)
            {
                set.Add(new KeyValuePair<int, Color>(
                    rand.Next(Form.Images.Count),
                    availableColors[rand.Next(availableColors.Count)]));
            }

            var list = Utility.DoubleElements(set.ToList()).Shuffle(rand);

            for(int i = 0; i < RowCount * ColumnCount; i++)
            {
                var chosen = list[i];
                Controls.Add(new Card(Form.Images[chosen.Key].ReplaceColor(Color.Black, chosen.Value), null, stopwatch));
            }

            //InitializeComponents();
            InitializeGame();
        }

        const int CellSize = 80;
    }

}