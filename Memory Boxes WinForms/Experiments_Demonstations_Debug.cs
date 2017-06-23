using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Boxes_WinForms
{
    public class Experiments_Demonstations_Debug
    {
        public void MeasureCharacterRangesRegionsDemonstration(PaintEventArgs e)
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

        public void RegionDataDemonstration(PaintEventArgs e)
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
            e.Graphics.FillRectangle(Brushes.Red, e.ClipRectangle);

            region3.Dispose();

        }

        public void RegionHitDemonstration(PaintEventArgs e)
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

            if(region1.IsVisible(point, e.Graphics))
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

        public void CustomBlendDemonstration(PaintEventArgs e)
        {
            Point startPoint2 = new Point(20, 110);
            Point endPoint2 = new Point(140, 110);
            float[] myFactors = { 0f, 0f, 1f, 1f, 0f, 0f, 1f, 0f };
            float[] myPositions = { 0.0f, .2f, .2f, .4f, .4f, .6f, .8f, 1.0f };
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
        }

    }
}
