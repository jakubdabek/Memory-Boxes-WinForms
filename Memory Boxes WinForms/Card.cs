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
        const int Duration = 3000; //ms

        static bool Available = true;
        static Card CurrentCard = null;

        GameBoard Board { get; }
        public int ImageIndex { get; private set; }
        public Color Color { get; private set; }

        Stopwatch stopwatch;
        TimeSpan startingTime;

        Bitmap Obverse { get; }
        Bitmap Reverse { get; }
        Bitmap CurrentImage { get; set; }
        Card Match { get; set; }
        States state = States.Reverse;

        public Card(int index, Color color, Bitmap reverse, GameBoard board) : 
            this(board.Form.Images[index].ReplaceColor(Color.Black, color), reverse, board)
        {
            ImageIndex = index;
            Color = color;
        }

        public Card(Bitmap symbol, Bitmap reverse, GameBoard board)
        {
            Board = board;
            CurrentCard = null;
            Available = true;
            Board.MatchedCardsCount = 0;

            DoubleBuffered = true;

            Board = board; 
            this.stopwatch = Board.internalStopwatch;
            this.Obverse = symbol;
            if(reverse != null)
            {
                this.Reverse = reverse;
            }
            else
            {
                Bitmap bitmap = new Bitmap(symbol.Width, symbol.Height);
                using(var gr = Graphics.FromImage(bitmap))
                {
                    gr.FillRectangle(Brushes.DarkRed, new Rectangle(new Point(0, 0), bitmap.Size));
                }
                this.Reverse = bitmap;
            }

            Dock = DockStyle.Fill;
            //SizeMode = PictureBoxSizeMode.Zoom;
            BorderStyle = BorderStyle.FixedSingle;
            CurrentImage = new Bitmap(this.Reverse);

            this.Click += Card_Click;
        }

        [Flags]
        enum States : byte
        {
            Reverse =     0b00000001,
            Rotating =    0b00000010,
            Obverse =     0b00000100,
            FirstPhase =  0b00001000,
            SecondPhase = 0b00010000,
            Matched =     0b00100000,
            ///<summary>To be XOR'ed with the state value to change phases</summary>
            PhaseChange = Reverse | Obverse | FirstPhase | SecondPhase
        }

        class OverrideEventArgs : EventArgs { }        

        private void OverrideClick()
        {
            Card_Click(this, new OverrideEventArgs());
        }

        private void Card_Click(object sender, EventArgs e)
        {
            if(Available)
            {
                if(CurrentCard is null)
                {
                    CurrentCard = this;
                }
                //else if(this.Obverse == CurrentCard.Obverse)
                else if(ImageIndex == CurrentCard.ImageIndex && Color == CurrentCard.Color)
                {
                    this.state |= States.Matched;
                    this.Match = CurrentCard;
                    CurrentCard.state |= States.Matched;
                    CurrentCard.Match = this;
                    CurrentCard = null;
                }
                else
                {
                    Available = false;
                    //CurrentCard = null;
                }

                Enabled = false;                
                StartRotation();
            }

        }

        async void StartRotation(int delay = 0)
        {
            if(delay > 0)
            {
                await Task.Delay(delay);
                Available = true;
            }
            else
            {
                Board.ActiveCardsCount += 1;
            }

            startingTime = stopwatch.Elapsed;
            state |= States.Rotating | States.FirstPhase;

        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle resultRect = ClientRectangle;

            if(state.HasFlag(States.Rotating))            
            {
                double argument = ((stopwatch.Elapsed - startingTime).TotalMilliseconds % Duration) * 2 * Math.PI / Duration;
                double cos = Math.Cos(argument);
                int newWidth = (int)(Obverse.Width * Math.Abs(cos));
                if(state.HasFlag(States.FirstPhase))
                {
                    if(cos < 0)
                    {
                        state ^= States.PhaseChange;
                        Bitmap newImage = state.HasFlag(States.Reverse) ? Reverse : Obverse;
                        CurrentImage = newImage;
                    }
                }
                else if(Math.Sin(argument) <= 0)
                {
                    state ^= States.Rotating | States.SecondPhase;
                    if(state.HasFlag(States.Reverse))
                    {
                        Enabled = true;
                        if(CurrentCard is null)
                            Available = true;
                        Board.ActiveCardsCount -= 1;
                    }
                    else if(!state.HasFlag(States.Matched) && !ReferenceEquals(this, CurrentCard))
                    {
                        StartRotation(1000);
                        CurrentCard.StartRotation(1000);
                        CurrentCard = null;
                    }
                    if(state.HasFlag(States.Matched))
                    {
                        if(!Match.state.HasFlag(States.Rotating))
                        {
                            Board.MatchedCardsCount += 2;
                            Board.ActiveCardsCount -= 2;
                        }
                    }
                }

                //Size imageSize = new Size(newWidth, Image.Height);
                //float ratio = Math.Min((float)ClientRectangle.Width / imageSize.Width, (float)ClientRectangle.Height / imageSize.Height);
                //resultRect.Width = (int)(imageSize.Width * ratio);
                //resultRect.Height = (int)(imageSize.Height * ratio);
                //resultRect.X = (ClientRectangle.Width - resultRect.Width) / 2;
                //resultRect.Y = (ClientRectangle.Height - resultRect.Height) / 2;

                resultRect.Inflate(-(int)((1 - Math.Abs(cos)) * resultRect.Width) / 2, 0);
            }

            pe.Graphics.DrawImage(CurrentImage, resultRect);
            base.OnPaint(pe);
        }
    }
}
