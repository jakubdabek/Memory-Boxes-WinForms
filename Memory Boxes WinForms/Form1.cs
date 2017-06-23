using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Boxes_WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            rainbow = GenerateRainbowColors(15).ToArray();
            titleRainbowTimer.Interval = 200;
        }

        private void SetTitleColorBlend(string title, Graphics gr)
        {
            colorBlend = new ColorBlend();

            int len = title.Length;
            List<CharacterRange> ranges = new List<CharacterRange>();
            for(int i = 0; i < len; i++)
            {
                ranges.Add(new CharacterRange(i, 1));
            }
            StringFormat format = new StringFormat();
            format.SetMeasurableCharacterRanges(ranges.ToArray());

            Region[] regions = gr.MeasureCharacterRanges(title, titleFont, new RectangleF(titleStart.X, titleStart.Y, 300, 50), format);

            List<RectangleF> letterBounds = (from a in regions
                                                select a.GetBounds(gr)
                                            ).ToList();
            titleBounds = new RectangleF(
                letterBounds[0].Left,
                letterBounds[0].Top,
                letterBounds.Last().Right - letterBounds[0].Left,
                letterBounds[0].Height);

            //dd1 = new DebugDraw(gra =>
            //    {
            //        gra.FillRectangle(Brushes.Cyan, titleBounds);
            //        gra.DrawRectangles(Pens.Red, letterBounds.ToArray());
            //        gra.DrawString(title, new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, titleStart);
            //    });


            List<float> positions = new List<float> { 0.0f };
            foreach(var bounds in letterBounds)
            {
                float val = (bounds.Right - titleBounds.Left) / titleBounds.Width;
                if(bounds != letterBounds.Last()) positions.Add(val);
                positions.Add(val);
            }

            //List<float> factors = new List<float>();
            //for(int i = 0; i < positions.Count; i++)
            //{
            //    if(i % 4 < 2)
            //        factors.Add(0f);
            //    else
            //        factors.Add(1f);
            //}

            colorBlend.Positions = positions.ToArray();            
        }

        private void MakeExtendedRainbow(int len)
        {
            if(extendedRainbow == null)
            {
                List<Color> _extendedRainbow = new List<Color>();
                int originalLen = len;
                for(; _extendedRainbow.Count < originalLen; len -= rainbow.Length)
                    _extendedRainbow.AddRange(rainbow.Take(len));

                extendedRainbow = new LinkedList<Color>(_extendedRainbow);
            }
            else
            {
                int a;
                extendedRainbow.AddFirst(
                    rainbow[
                                (
                                    (
                                        a = rainbow.TakeWhile(col => col != extendedRainbow.First.Value).Count()
                                    ) > 0
                                ) ?
                                a - 1 :
                                (rainbow.Length - 1)
                           ]);
                extendedRainbow.RemoveLast();
            }
        }

        string title = "Memory Boxes";
        private RectangleF titleBounds;
        private PointF titleStart = new PointF(10f, 10f);
        private Font titleFont = new Font("Microsoft Sans Serif", 26, FontStyle.Bold);
        private ColorBlend colorBlend;
        readonly Color[] rainbow;
        private LinkedList<Color> extendedRainbow;

        List<Color> GenerateRainbowColors(int colorsCount)
        {
            double frequency = 2 * Math.PI / colorsCount;
            double deltaR = Math.PI * 0 / 3;
            double deltaG = Math.PI * 2 / 3;
            double deltaB = Math.PI * 4 / 3;

            List<Color> colors = new List<Color>();

            for(int i = 0; i < colorsCount; i++)
            {
                colors.Add(Color.FromArgb(
                    (int)(Math.Sin(frequency * i + deltaR) * 127.5 + 127.5),
                    (int)(Math.Sin(frequency * i + deltaG) * 127.5 + 127.5),
                    (int)(Math.Sin(frequency * i + deltaB) * 127.5 + 127.5)));
            }
            return colors;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(colorBlend == null)
                SetTitleColorBlend(title, e.Graphics);
            if(extendedRainbow == null)
                MakeExtendedRainbow(title.Length);

            titleRainbowTimer.Start();

            colorBlend.Colors = extendedRainbow.SelectMany(col => new[] { col, col }).ToArray();

            LinearGradientBrush brush = new LinearGradientBrush(titleBounds.Location, new PointF(titleBounds.Right, titleBounds.Top), Color.Empty, Color.Empty);
            brush.InterpolationColors = colorBlend;
            e.Graphics.DrawString(title, titleFont, brush, titleStart);
        }

        private void titleRainbowTimer_Tick(object sender, EventArgs e)
        {
            MakeExtendedRainbow(title.Length);
            TitlePanel.Invalidate();
        }
    }
    
    public class FPSClock:Timer
    {
        System.Threading.Timer timer;
        readonly int FPS;

        public FPSClock(int? fps)
        {
            FPS = fps ?? 30;
            timer = new System.Threading.Timer(new System.Threading.TimerCallback(Game.state.MainGameLoop));
            timer.Change(1000, 1000 / FPS);
        }
    }
}
