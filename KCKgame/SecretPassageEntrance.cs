using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class SecretPassageEntrance : Terrain
    {
        private SecretPassageExit exit;
        public SecretPassageEntrance(SecretPassageExit e)
        {
            exit = e;
            CanEnterNow();
            ChangeVisual("<>");
        }
        public int GiveExitX() { return exit.GetX(); }
        public int GiveExitY() { return exit.GetY(); }
    }
}
