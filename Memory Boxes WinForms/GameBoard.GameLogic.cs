using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;

namespace Memory_Boxes_WinForms.Game
{
    partial class GameBoard
    {
        const int FPS = 60;

        public Stopwatch internalStopwatch = new Stopwatch();
        public Stopwatch gameDurationStopwatch = new Stopwatch();
        int matchedCardsCount = 0;
        public int MatchedCardsCount
        {
            set
            {
                matchedCardsCount = value;
                if(matchedCardsCount == RowCount * ColumnCount)
                {
                    Win();
                }
            }
            get => matchedCardsCount;
        }

        int activeCardsCount = 0; 
        public int ActiveCardsCount
        {
            get => activeCardsCount;
            set
            {
                activeCardsCount = value;
                CheckPause();
            }
        }


        void InitializeGame()
        {
            internalStopwatch.Start();
            gameDurationStopwatch.Start();
            var timer = Form.mainTimer;
            timer.Tick += MainTimer_Tick;
            timer.Interval = 500 / FPS;            
            timer.Start();
            Form.timeDisplayTimer.Start();            
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        bool pauseRequested = false;

        public void PauseUnpause()
        {
            Enabled = !Enabled;
            pauseRequested = !pauseRequested;            
            CheckPause();
        }

        public void CheckPause()
        {
            if(pauseRequested && ActiveCardsCount == 0)
            {
                if(Enabled)
                    gameDurationStopwatch.Start();
                else
                    gameDurationStopwatch.Stop();
                pauseRequested = false;
            }
        }

        public void Win()
        {
            internalStopwatch.Stop();
            gameDurationStopwatch.Stop();

            MessageBox.Show($"Congratulations, you won!\n    Your time was {gameDurationStopwatch.Elapsed:mm\\:ss\\.f}", "Congratulations!");
            Form.Close();
        }
    }
}
