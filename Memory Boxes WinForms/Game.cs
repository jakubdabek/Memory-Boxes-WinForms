using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Boxes_WinForms
{
    partial class GameForm
    {
        const int FPS = 60;

        void InitializeGame()
        {
            Random rand = new Random();

            mainLoopTimer.Interval = 1000 / FPS;
            mainLoopTimer.Start();
        }

        bool _starting = true;

        private void mainLoopTimer_Tick(object sender, EventArgs e)
        {

        }
    }
}
