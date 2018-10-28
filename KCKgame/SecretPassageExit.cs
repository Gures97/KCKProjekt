using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class SecretPassageExit : Terrain
    {
        int x, y;
        public SecretPassageExit(int a, int b)
        {
            y = a; x = b;
            ChangeVisual("><");
        }
        public int GetX() { return x; }
        public int GetY() { return y; }
    }
}
