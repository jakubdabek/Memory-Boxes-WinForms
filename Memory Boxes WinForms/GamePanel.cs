using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Memory_Boxes_WinForms
{
    public class GamePanel : TableLayoutPanel
    {
        public GamePanel(Control parent) : base() => parent.Controls.Add(this);
        public GamePanel(TableLayoutPanelCellPosition size, Control parent) : this(size.Column, size.Row, parent) { }
        public GamePanel(TableLayoutPanelCellPosition size, Point position, Control parent) : this(size.Column, size.Row, position, parent) { }

        public GamePanel(int columnCount, int rowCount, Control parent) : this(parent)
        {
            Size = new Size(columnCount * cellSize, rowCount * cellSize);

            ColumnCount = columnCount;
            RowCount = rowCount;
            ColumnStyles.Add(SizeType.Percent, 100f / columnCount, columnCount);
            RowStyles.Add(SizeType.Percent, 100f / rowCount, rowCount);
            CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

            if(!_positionSet)
            {
                Utility.CenterInControl(this, this.Parent);
            }
        }

        public GamePanel(int columnCount, int rowCount, Point position, Control parent) : this(columnCount, rowCount, parent)
        {
            Location = position;
            _positionSet = true;
        }

        bool _positionSet = false;
        const int cellSize = 80;
    }
}
