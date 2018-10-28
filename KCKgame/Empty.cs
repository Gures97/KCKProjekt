using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class Empty : Terrain
    {
        public Empty()
        {
            CanEnterNow();
            ChangeVisual("  ");
        }
    }
}
