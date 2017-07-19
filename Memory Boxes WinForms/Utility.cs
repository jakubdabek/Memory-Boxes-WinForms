using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CellPos = System.Windows.Forms.TableLayoutPanelCellPosition;

namespace Memory_Boxes_WinForms
{
    public class Utility
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
            for(int i = 0; i < list.Count; i++)
            {
                newList.AddRange(new[] { list[i], list[i] });
            }
            return newList;
        }

        public static class RainbowGenerator
        {
            static Dictionary<int, List<Color>> rainbows = new Dictionary<int, List<Color>>();

            static List<Color> GenerateBaseRainbow(int colorsCount)
            {
                if(rainbows.TryGetValue(colorsCount, out var colors))
                {
                    return colors;
                }
                else
                {
                    double frequency = 2 * Math.PI / colorsCount;
                    double deltaR = Math.PI * 0 / 3;
                    double deltaG = Math.PI * 2 / 3;
                    double deltaB = Math.PI * 4 / 3;

                    colors = new List<Color>();

                    for(int i = 0; i < colorsCount; i++)
                    {
                        colors.Add(Color.FromArgb(
                            (int)(Math.Sin(frequency * i + deltaR) * 127.5 + 127.5),
                            (int)(Math.Sin(frequency * i + deltaG) * 127.5 + 127.5),
                            (int)(Math.Sin(frequency * i + deltaB) * 127.5 + 127.5)));
                    }

                    rainbows.Add(colorsCount, colors);
                    return colors;
                }
            }

            static IEnumerable<float> UniformValuesFrom0to1(int positionsCount)
            {
                for(int i = 0; i < positionsCount; i++)
                {
                    yield return (i / (positionsCount - 1f));
                }
            }

            static List<Color> GenerateExtendedRainbow(int colorsCount, int count, int offset = 0)
            {
                List<Color> baseRainbow = GenerateBaseRainbow(colorsCount);
                int _offset = colorsCount - offset;
                List<Color> colors = new List<Color>(count);
                for(int i = 0; i < count; i++)
                {
                    colors.Add(baseRainbow[(i + _offset) % colorsCount]);
                }
                return colors;
            }

            static ColorBlend GenerateUniformColorBlend(int colorsCount, int count, int offset = 0)
            {
                ColorBlend cb = new ColorBlend()
                {
                    Colors = GenerateExtendedRainbow(colorsCount, count, offset).ToArray(),
                    Positions = UniformValuesFrom0to1(count).ToArray()
                };
                return cb;
            }

            public class TextBrush
            {
                private int offset;
                private int Offset
                {
                    get => ++offset >= colorsCount ? offset = 0 : offset;
                    set => offset = value;
                }
                private readonly int colorsCount = 15;
                private readonly int count;
                private readonly string text;
                private readonly Graphics graphics;
                private readonly Font font;
                private readonly PointF startingPoint;

                private LinearGradientBrush brush;

                private TextBrush(string _text, Graphics _graphics, Font _font, PointF _startingPoint, out Action<Graphics, bool> drawAction)
                {
                    text = _text;
                    graphics = _graphics;
                    count = text.Length;
                    font = _font;
                    startingPoint = _startingPoint;

                    MadeBrushes.Add(text, this);

                    SizeF size;
                    Region[] regions;
                    using(StringFormat format = new StringFormat())
                    {
                        format.SetMeasurableCharacterRanges(generateSingleCharacterRanges(count).ToArray());

                        size = graphics.MeasureString(text, font);
                        regions = graphics.MeasureCharacterRanges(text, font, new RectangleF(startingPoint, size), format);
                    }

                    //StringFormat format = new StringFormat();
                    //format.SetMeasurableCharacterRanges(generateSingleCharacterRanges(count).ToArray());

                    //SizeF size = graphics.MeasureString(text, font);
                    //Region[] regions = graphics.MeasureCharacterRanges(text, font, new RectangleF(startingPoint, size), format);

                    var letterBounds = regions.Select(r => r.GetBounds(graphics)).ToList();

                    var (firstRect, lastRect) = (letterBounds[0], letterBounds.Last());

                    var textBounds = new RectangleF(
                        firstRect.Left,
                        firstRect.Top,
                        lastRect.Right - firstRect.Left,
                        firstRect.Height);

                    var positions = new List<float> { 0.0f };
                    foreach(var bounds in letterBounds)
                    {
                        float val = (bounds.Right - textBounds.Left) / textBounds.Width;
                        if(bounds != letterBounds.Last()) positions.Add(val);
                        positions.Add(val);
                    }

                    ColorBlend colorBlend = new ColorBlend()
                    {
                        Colors = DoubleElements(GenerateExtendedRainbow(colorsCount, count)).ToArray(),
                        Positions = positions.ToArray()
                    };
                    this.brush = new LinearGradientBrush(textBounds.Location, new PointF(textBounds.Right, textBounds.Top), Color.Empty, Color.Empty)
                    {
                        InterpolationColors = colorBlend
                    };
                    drawAction = (gr, next) =>
                    {
                        gr.DrawString(text, font, brush, startingPoint);
                        if(next)
                        {
                            colorBlend.Colors = DoubleElements(GenerateExtendedRainbow(colorsCount, count, Offset)).ToArray();
                            this.brush.InterpolationColors = colorBlend;
                        }
                    };

                    IEnumerable<CharacterRange> generateSingleCharacterRanges(int _count)
                    {
                        for(int i = 0; i < _count; i++)
                        {
                            yield return new CharacterRange(i, 1);
                        }
                    }
                }

                public static Action<Graphics, bool> MakeDrawAction(string text, Graphics graphics, Font font, PointF startingPoint)
                {
                    new TextBrush(text, graphics, font, startingPoint, out var drawAction);
                    return drawAction;
                }

                private static Dictionary<string, TextBrush> MadeBrushes = new Dictionary<string, TextBrush>();
            }
        }

        [Flags]
        public enum CenterStyle { Vertical = 0b01, Horizontal = 0b10, Bilateral = Vertical | Horizontal }

        public static Rectangle GetCenterPositionInControl(Control parent, Rectangle childLocation, CenterStyle centerStyle = CenterStyle.Bilateral)
        {
            if(centerStyle.HasFlag(CenterStyle.Horizontal))
                childLocation.X = (parent.ClientSize.Width - childLocation.Width) / 2;
            if(centerStyle.HasFlag(CenterStyle.Vertical))
                childLocation.Y = (parent.ClientSize.Height - childLocation.Height) / 2;
            return childLocation;
        }

        public static void CenterInControl(Control child, Control parent, CenterStyle centerStyle = CenterStyle.Bilateral)
        {
            child.Location = GetCenterPositionInControl(parent, child.Bounds, centerStyle).Location;
        }
    }
    
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

        public static CellPos Add(this CellPos pos, int column, int row)
        {
            return new CellPos(pos.Column + column, pos.Row + row);
        }

        public static CellPos Add(this CellPos pos, CellPos addend)
        {
            return pos.Add(addend.Column, addend.Row);
        }

        public static IEnumerable<ValueTuple<T1, T2>> Zip<T1, T2>(this IEnumerable<T1> left, IEnumerable<T2> right)
        {
            var leftEnum = left.GetEnumerator();
            var rightEnum = right.GetEnumerator();
            while(leftEnum.MoveNext() & rightEnum.MoveNext())
                yield return (leftEnum.Current, rightEnum.Current);
        }
    }

    public partial class Form1
    {
        private bool nextRainbowStep = true;

        string titleText = "Memory Boxes";
        private Point titleStartLocation = new Point(10, 50);
        private Font titleFont = new Font("Microsoft Sans Serif", 30, FontStyle.Bold);

        Action<Graphics, bool> DrawTitleText;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(!titleRainbowTimer.Enabled)
                titleRainbowTimer.Start();

            if(DrawTitleText is null)
            {
                DrawTitleText = Utility.RainbowGenerator.TextBrush.MakeDrawAction(titleText, e.Graphics, titleFont, titleStartLocation);
            }

            DrawTitleText(e.Graphics, nextRainbowStep);
            nextRainbowStep = false;
        }

        private void titleRainbowTimer_Tick(object sender, EventArgs e)
        {
            nextRainbowStep = true;
            TitlePanel.Invalidate();
        }
    }
}