using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Boxes_WinForms
{
    public static partial class Utility
    {
        public static T[] DoubleElements<T>(T[] list)
        {
            T[] newList = new T[list.Length * 2];
            for(int i = 0; i < newList.Length; i += 2)
            {
                newList[i] = list[i / 2];
                newList[i + 1] = list[i / 2];
            }
            return newList;
        }

        public static List<T> DoubleElements<T>(List<T> list)
        {
            List<T> newList = new List<T>(list.Count * 2);
            foreach(T element in list)
            {
                newList.AddRange(new[] { element, element });
            }
            return newList;
        }

        [Flags]
        public enum CenterStyle { Vertical = 0b01, Horizontal = 0b10, Bilateral = Vertical | Horizontal }

        public static Rectangle GetCenterPositionInControl(Rectangle parentRect, Rectangle childRect, CenterStyle centerStyle = CenterStyle.Bilateral)
        {
            if(centerStyle.HasFlag(CenterStyle.Horizontal))
                childRect.X = (parentRect.Width - childRect.Width) / 2;
            if(centerStyle.HasFlag(CenterStyle.Vertical))
                childRect.Y = (parentRect.Height - childRect.Height) / 2;
            return childRect;
        }

        public static void CenterInControl(Control child, Control parent, CenterStyle centerStyle = CenterStyle.Bilateral)
        {
            child.Location = GetCenterPositionInControl(parent.ClientRectangle, child.Bounds, centerStyle).Location;
        }

        public enum InterpolationMethod { Linear, Sine, Cosine }

        public static double Interpolate(double start, double end, double fraction, InterpolationMethod method)
        {
            double difference = end - start;

            switch(method)
            {
            case InterpolationMethod.Linear:
                return start + (difference * fraction);
            case InterpolationMethod.Sine:
                return start + Math.Abs(Math.Sin(fraction * 2 * Math.PI) * difference);
            case InterpolationMethod.Cosine:
                return start + Math.Abs(Math.Cos(fraction * 2 * Math.PI) * difference);
            default:
                throw new ArgumentOutOfRangeException("Given interpolation method is not specified", nameof(method));
            }
        }
    }
}