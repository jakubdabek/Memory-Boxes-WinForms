using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Memory_Boxes_WinForms
{
    public static class RainbowUtilities
    {
        class RainbowCollection
        {
            readonly Dictionary<int, List<Color>> rainbows = new Dictionary<int, List<Color>>();

            public List<Color> this[int length]
            {
                get
                {
                    if(rainbows.TryGetValue(length, out var colors))
                        return colors;

                    colors = GenerateRainbowColors(length);
                    rainbows.Add(length, colors);
                    return colors;
                }
            }

            static List<Color> GenerateRainbowColors(int colorsCount)
            {
                double frequency = 2 * Math.PI / colorsCount;
                double deltaR = Math.PI * 0 / 3;
                double deltaG = Math.PI * 2 / 3;
                double deltaB = Math.PI * 4 / 3;

                var colors = new List<Color>();

                for(int i = 0; i < colorsCount; i++)
                {
                    colors.Add(Color.FromArgb(
                        (int)(Math.Sin(frequency * i + deltaR) * 127.5 + 127.5),
                        (int)(Math.Sin(frequency * i + deltaG) * 127.5 + 127.5),
                        (int)(Math.Sin(frequency * i + deltaB) * 127.5 + 127.5)));
                }

                return colors;
            }
        }

        static readonly RainbowCollection rainbows = new RainbowCollection();

        static IEnumerable<float> UniformValuesFrom0to1(int positionsCount)
        {
            if(positionsCount <= 1)
                throw new ArgumentOutOfRangeException("Number of positions must be greater than 1", nameof(positionsCount));

            return Generate(positionsCount);

            IEnumerable<float> Generate(int count)
            {
                for(int i = 0; i < count; i++)
                {
                    yield return (i / (count - 1f));
                }
            }
        }

        static List<Color> GenerateExtendedRainbow(int colorsCount, int length, int offset = 0)
        {
            List<Color> baseRainbow = rainbows[colorsCount];
            int _offset = colorsCount - offset;
            List<Color> colors = new List<Color>(length);

            for(int i = 0; i < length; i++)
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

        public class TextDrawer : IDisposable
        {
            private int _offset;
            private int Offset
            {
                get => ++_offset >= ColorCount ? _offset = 0 : _offset;
                set => _offset = value;
            }
            protected readonly int ColorCount = 15;
            private readonly int characterCount;
            private readonly Graphics graphics;
            private readonly PointF StartingPoint;
            public string Text { get; }
            public Font Font { get; }

            protected LinearGradientBrush Brush;
            protected ColorBlend ColorBlend;

            public TextDrawer(string text, Graphics initGraphics, Font font, PointF startingPoint)
            {
                Text = text;
                graphics = initGraphics;
                characterCount = text.Length;
                Font = font;
                StartingPoint = startingPoint;

                List<RectangleF> letterBounds;
                using(var format = new StringFormat())
                {
                    format.SetMeasurableCharacterRanges(generateSingleCharacterRanges(characterCount).ToArray());

                    var size = graphics.MeasureString(Text, Font);
                    letterBounds = graphics.MeasureCharacterRanges(Text, Font, new RectangleF(StartingPoint, size), format)
                        .Select(region => region.GetBounds(graphics))
                        .ToList();
                }

                var (firstLetterRect, lastLetterRect) = (letterBounds.First(), letterBounds.Last());

                var textBounds = new RectangleF(
                    firstLetterRect.Left,
                    firstLetterRect.Top,
                    lastLetterRect.Right - firstLetterRect.Left,
                    firstLetterRect.Height);

                var positions = new List<float>(letterBounds.Count * 2) { 0.0f };

                for(int i = 0; i < letterBounds.Count; i++)
                {
                    float val = (letterBounds[i].Right - textBounds.Left) / textBounds.Width;
                    if(i != letterBounds.Count - 1) positions.Add(val);
                    positions.Add(val);
                }

                ColorBlend = new ColorBlend()
                {
                    Colors = Utility.DoubleElements(GenerateExtendedRainbow(ColorCount, characterCount)).ToArray(),
                    Positions = positions.ToArray()
                };

                Brush = new LinearGradientBrush(textBounds.Location, new PointF(textBounds.Right, textBounds.Top), Color.Empty, Color.Empty)
                {
                    InterpolationColors = ColorBlend
                };

                IEnumerable<CharacterRange> generateSingleCharacterRanges(int _count)
                {
                    for(int i = 0; i < _count; i++)
                    {
                        yield return new CharacterRange(i, 1);
                    }
                }
            }

            public void Draw(Graphics gr, bool next)
            {
                gr.DrawString(Text, Font, Brush, StartingPoint);
                if(next)
                {
                    ColorBlend.Colors = Utility.DoubleElements(GenerateExtendedRainbow(ColorCount, characterCount, Offset)).ToArray();
                    Brush.InterpolationColors = ColorBlend;
                }
            }

            bool disposed = false;

            public void Dispose()
            {
                if(disposed)
                    throw new ObjectDisposedException(nameof(TextDrawer));
                Dispose(true);
            }

            protected void Dispose(bool disposing)
            {
                disposed = true;
                if(disposing)
                {
                    Brush.Dispose();
                }
            }
        }
    }
}
