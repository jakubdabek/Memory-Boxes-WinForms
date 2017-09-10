using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Memory_Boxes_WinForms.Game
{
    partial class GameBoard
    {
        const int FPS = 60;

        public Stopwatch stopwatch = new Stopwatch();

        void InitializeGame()
        {
            Random rand = new Random();

            stopwatch.Start();
            Timer timer = Form.mainTimer;
            timer.Tick += MainTimer_Tick;
            timer.Interval = 500 / FPS;            
            timer.Start();
            Form.timeDisplayTimer.Start();            
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        bool _paused = true;

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        Timer timer;        
    }
}
