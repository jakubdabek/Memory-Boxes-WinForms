using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

using CellPos = System.Windows.Forms.TableLayoutPanelCellPosition;

namespace Memory_Boxes_WinForms
{
    public static class Extensions
    {
        public static void Deconstruct<T>(this IEnumerable<T> sequence, out T val1, out T val2, out T val3, out T val4, out T val5, out T val6, out T val7)
        {
            var a = sequence.ToList();
            (val1, val2, val3, val4, val5, val6, val7) =
                (a.ElementAtOrDefault(0),
                a.ElementAtOrDefault(1),
                a.ElementAtOrDefault(2),
                a.ElementAtOrDefault(3),
                a.ElementAtOrDefault(4),
                a.ElementAtOrDefault(5),
                a.ElementAtOrDefault(6));
        }

        public static void Deconstruct<T>(this IEnumerable<T> sequence, out T val1, out T val2)
        {
            var a = sequence.ToArray();
            if(a.Length < 2) throw new ArgumentException("Sequence too small for deconstruction", nameof(sequence));
            (val1, val2) = (a[0], a[1]);
        }

        public static CellPos Add(this CellPos pos, int column, int row) => new CellPos(pos.Column + column, pos.Row + row);

        public static CellPos Add(this CellPos pos, CellPos addend) => pos.Add(addend.Column, addend.Row);

        public static IEnumerable<ValueTuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
        {
            using(var leftEnum = left.GetEnumerator())
            using(var rightEnum = right.GetEnumerator())
                while(leftEnum.MoveNext() & rightEnum.MoveNext())
                    yield return (leftEnum.Current, rightEnum.Current);
        }

        public static IEnumerable<ValueTuple<T, int>> WithIndex<T>(this IEnumerable<T> sequence, int index = 0)
        {
            foreach(var item in sequence)
            {
                yield return (item, index++);
            }
        }

        public static void Add(this IList seq, object obj, int count)
        {
            for(int i = 0; i < count; i++)
            {
                seq.Add(obj);
            }
        }

        public static IList<T> Shuffle<T>(this IList<T> list, Random rng)
        {
            for(var i = 0; i < list.Count; i++)
                list.Swap(i, rng.Next(i, list.Count));
            return list;
        }

        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void Add(this TableLayoutStyleCollection collection, TableLayoutStyle style, int count)
        {
            for(int i = 0; i < count; i++)
            {
                collection.Add(style);
            }
        }

        public static void Add(this TableLayoutStyleCollection collection, SizeType sizeType, float? height, int count)
        {
            object[] parameters = height.HasValue ? new object[] { sizeType, height.Value } : new object[] { sizeType };

            if(collection.GetType() == typeof(TableLayoutRowStyleCollection))
            {
                for(int i = 0; i < count; i++)
                {
                    collection.Add(Activator.CreateInstance(typeof(RowStyle), parameters) as RowStyle);
                }
            }
            else
            {
                for(int i = 0; i < count; i++)
                {
                    collection.Add(Activator.CreateInstance(typeof(ColumnStyle), parameters) as ColumnStyle);
                }
            }
        }

        public static Bitmap ReplaceColor(this Image image, Color oldColor, Color newColor)
        {
            var bmp = new Bitmap(image.Width, image.Height);
            using(var gr = Graphics.FromImage(bmp))
            {
                var imageAttrs = new System.Drawing.Imaging.ImageAttributes();
                var colorMap = new System.Drawing.Imaging.ColorMap() { OldColor = oldColor, NewColor = newColor };
                imageAttrs.SetRemapTable(new[] { colorMap });
                gr.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, imageAttrs);
            }
            
            return bmp;
        }
    }
}
