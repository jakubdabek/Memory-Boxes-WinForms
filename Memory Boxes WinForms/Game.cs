using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Boxes_WinForms.Game
{
    partial class GameBoard
    {
        const int FPS = 60;

        public Stopwatch stopwatch;

        void InitializeGame()
        {
            Random rand = new Random();

            stopwatch = Stopwatch.StartNew();
            //timeTimer.Start();
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
    }
}
