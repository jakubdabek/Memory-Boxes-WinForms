using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;

namespace Memory_Boxes_WinForms.Game
{
    [DesignerCategory("")]
    class Card : UserControl//PictureBox
    {
        public Card(Bitmap symbol, Bitmap reverse, Stopwatch stopwatch)
        {
            DoubleBuffered = true;

            this.stopwatch = stopwatch;
            this.symbol = symbol;
            if(reverse != null)
            {
                this.reverse = reverse;
            }
            else
            {
                Bitmap bitmap = new Bitmap(symbol.Width, symbol.Height);
                using(var gr = Graphics.FromImage(bitmap))
                {
                    gr.FillRectangle(Brushes.DarkRed, new Rectangle(new Point(0, 0), bitmap.Size));
                }
                this.reverse = bitmap;
            }

            Dock = DockStyle.Fill;
            //SizeMode = PictureBoxSizeMode.Zoom;
            BorderStyle = BorderStyle.FixedSingle;
            Image = new Bitmap(this.reverse);

            this.Click += Card_Click;

                     
            
        }

        [Flags]
        enum States
        {
            Reverse =     0b00000001,
            Rotating =    0b00000010,
            Obverse =     0b00000100,
            FirstPhase =  0b00001000,
            SecondPhase = 0b00010000,
            PhaseChange = Reverse | Obverse | FirstPhase | SecondPhase
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if(!state.HasFlag(States.Rotating))
            {
                startingTime = stopwatch.Elapsed;
                state |= States.Rotating | States.FirstPhase;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle resultRect = ClientRectangle;
            if(state.HasFlag(States.Rotating))
            {
                double argument = ((stopwatch.Elapsed - startingTime).TotalMilliseconds % Duration) * 2 * Math.PI / Duration;
                double cos = Math.Cos(argument);
                int newWidth = (int)(symbol.Width * Math.Abs(cos));
                if(state.HasFlag(States.FirstPhase))
                {
                    if(cos < 0)
                    {
                        state ^= States.PhaseChange;
                        Bitmap newImage = state.HasFlag(States.Reverse) ? reverse : symbol;
                        Image = newImage;
                    }
                }
                else
                if(Math.Sin(argument) <= 0)
                {
                    state ^= States.Rotating | States.SecondPhase;
                }
                //lock(this)
                //{
                //    Image?.Dispose();
                //    Image = newWidth >= 1 ? new Bitmap(currentImage, new Size(newWidth, symbol.Height)) : null;
                //}

                Size imageSize = new Size(newWidth, Image.Height);
                float ratio = Math.Min((float)ClientRectangle.Width / imageSize.Width, (float)ClientRectangle.Height / imageSize.Height);
                resultRect.Width = (int)(imageSize.Width * ratio);
                resultRect.Height = (int)(imageSize.Height * ratio);
                resultRect.X = (ClientRectangle.Width - resultRect.Width) / 2;
                resultRect.Y = (ClientRectangle.Height - resultRect.Height) / 2;
            }

            pe.Graphics.DrawImage(Image, resultRect);
            base.OnPaint(pe);
        }

        States state = States.Reverse;

        Stopwatch stopwatch;
        TimeSpan startingTime;
        Bitmap symbol;
        Bitmap reverse;
        Bitmap Image { get; set; }

        const int Duration = 3000; //ms
    }
}
