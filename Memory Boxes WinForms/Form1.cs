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
        }

        private void SetTitleColorBlend(string title, Graphics gr)
        {
            if(colorBlend == null)
            {
                colorBlend = new ColorBlend();
                titleFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);

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
        }

        private delegate void DebugDraw(Graphics gr);
        private DebugDraw dd1;

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
            }
        }

        //private Blend blend;
        private RectangleF titleBounds;
        private PointF titleStart = new PointF(30f, 10f);
        private Font titleFont;
        private ColorBlend colorBlend;
        readonly Color[] rainbow = { Color.Red, Color.Orange, Color.YellowGreen, Color.Green, Color.Blue, Color.Indigo, Color.Violet };
        LinkedList<Color> extendedRainbow;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            string title = "Memory Boxes";
            SetTitleColorBlend(title, e.Graphics);
            MakeExtendedRainbow(title.Length);

            colorBlend.Colors = extendedRainbow.SelectMany(col => new[] { col, col }).ToArray();

            LinearGradientBrush brush = new LinearGradientBrush(titleBounds.Location, new PointF(titleBounds.Right, titleBounds.Top), Color.Empty, Color.Empty);
            brush.InterpolationColors = colorBlend;
            using(Font font = new Font("Microsoft Sans Serif", 16, FontStyle.Bold))
            {
                e.Graphics.DrawString(title, font, brush, titleStart);
            }
        }

        private void MeasureCharacterRangesRegions(PaintEventArgs e)
        {

            // Set up string.
            string measureString = "First and Second ranges";
            Font stringFont = new Font("Times New Roman", 16.0F);

            // Set character ranges to "First" and "Second".
            CharacterRange[] characterRanges = { new CharacterRange(0, 5), new CharacterRange(10, 6) };

            // Create rectangle for layout.
            float x = 50.0F;
            float y = 50.0F;
            float width = 35.0F;
            float height = 200.0F;
            RectangleF layoutRect = new RectangleF(x, y, width, height);

            // Set string format.
            StringFormat stringFormat = new StringFormat();
            stringFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            stringFormat.SetMeasurableCharacterRanges(characterRanges);

            // Draw string to screen.
            e.Graphics.DrawString(measureString, stringFont, Brushes.Black, x, y, stringFormat);

            // Measure two ranges in string.
            Region[] stringRegions = e.Graphics.MeasureCharacterRanges(measureString, stringFont, layoutRect, stringFormat);

            // Draw rectangle for first measured range.
            RectangleF measureRect1 = stringRegions[0].GetBounds(e.Graphics);
            e.Graphics.DrawRectangle(new Pen(Color.Red, 1), Rectangle.Round(measureRect1));

            // Draw rectangle for second measured range.
            RectangleF measureRect2 = stringRegions[1].GetBounds(e.Graphics);
            e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), Rectangle.Round(measureRect2));
        }

        private void DemonstrateRegionData2(PaintEventArgs e)
        {

            //Create a simple region.
            Region region1 = new Region(new Rectangle(10, 10, 100, 100));

            // Extract the region data.
            System.Drawing.Drawing2D.RegionData region1Data = region1.GetRegionData();
            byte[] data1;
            data1 = region1Data.Data;

            // Create a second region.
            Region region2 = new Region();

            // Get the region data for the second region.
            System.Drawing.Drawing2D.RegionData region2Data = region2.GetRegionData();

            // Set the Data property for the second region to the Data from the first region.
            region2Data.Data = data1;

            // Construct a third region using the modified RegionData of the second region.
            Region region3 = new Region(region2Data);

            // Dispose of the first and second regions.
            region1.Dispose();
            region2.Dispose();

            // Call ExcludeClip passing in the third region.
            e.Graphics.ExcludeClip(region3);

            // Fill in the client rectangle.
            e.Graphics.FillRectangle(Brushes.Red, this.ClientRectangle);

            region3.Dispose();

        }

        Point point;

        private void DemontsratingRegionHit(PaintEventArgs e)
        {
            Point point = new Point(60, 10);

            // Assume that the variable "point" contains the location of the
            // most recent mouse click.
            // To simulate a hit, assign (60, 10) to point.
            // To simulate a miss, assign (0, 0) to point.

            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Region region1 = new Region(new Rectangle(50, 0, 50, 150));
            Region region2 = new Region(new Rectangle(0, 50, 150, 50));

            // Create a plus-shaped region by forming the union of region1 and 
            // region2.
            // The union replaces region1.
            region1.Union(region2);

            if(region1.IsVisible(this.point, e.Graphics))
            {
                // The point is in the region. Use an opaque brush.
                solidBrush.Color = Color.FromArgb(255, 255, 0, 0);
            }
            else
            {
                // The point is not in the region. Use a semitransparent brush.
                solidBrush.Color = Color.FromArgb(64, 255, 0, 0);
            }

            e.Graphics.FillRegion(solidBrush, region1);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /********************************************************
            Point startPoint2 = new Point(20, 110);
            Point endPoint2 = new Point(140, 110);
            float[] myFactors = { 0f,0f, 1f,1f, 0f, 0f, 1f, 0f };
            float[] myPositions = { 0.0f, .2f,.2f, .4f,.4f, .6f, .8f, 1.0f };
            Blend myBlend = new Blend();
            myBlend.Factors = myFactors;
            myBlend.Positions = myPositions;
            LinearGradientBrush lgBrush2 = new LinearGradientBrush(
                startPoint2,
                endPoint2,
                Color.Blue,
                Color.Red);
            lgBrush2.Blend = myBlend;
            Rectangle ellipseRect2 = new Rectangle(20, 110, 120, 80);
            e.Graphics.FillEllipse(lgBrush2, ellipseRect2);
            /*********************************************************/
            //MeasureCharacterRangesRegions(e);
            //DemonstrateRegionData2(e);
            //DemontsratingRegionHit(e);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            point = (e as MouseEventArgs).Location;
            Invalidate();
        }
    }
    public class FPSClock : Timer
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
