using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory_Boxes_WinForms
{
    partial class GameForm
    {
        class Game
        {
            public static Game state;
            private bool _inMainLoop = false;

            public void MainGameLoop(object obj)
            {
                if(!_inMainLoop)
                {
                    _inMainLoop = true;



                    _inMainLoop = false;
                }
            }
        }
    }
}
