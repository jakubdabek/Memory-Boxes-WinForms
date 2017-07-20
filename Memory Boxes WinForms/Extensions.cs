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
            if(sequence.Count() < 2) throw new ArgumentException("Sequence too small for deconstruction", nameof(sequence));
            var a = sequence.Take(2).ToList();
            (val1, val2) = (a[0], a[1]);
        }

        public static CellPos Add(this CellPos pos, int column, int row) => new CellPos(pos.Column + column, pos.Row + row);

        public static CellPos Add(this CellPos pos, CellPos addend) => pos.Add(addend.Column, addend.Row);

        public static IEnumerable<ValueTuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
        {
            var leftEnum = left.GetEnumerator();
            var rightEnum = right.GetEnumerator();
            while(leftEnum.MoveNext() & rightEnum.MoveNext())
                yield return (leftEnum.Current, rightEnum.Current);
        }

        public static void Add(this IList seq, object obj, int count)
        {
            for(int i = 0; i < count; i++)
            {
                seq.Add(obj);
            }
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
    }
}
