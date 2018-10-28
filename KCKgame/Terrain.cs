using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class Terrain
    {
        private bool canEnter = false;
        private String visual = "- ";
        public void CanEnterNow() { canEnter = true; }
        public void CannotEnterNow() { canEnter = false; }
        public bool Enterable() { if (canEnter == true) return true; else return false; }
        public void ChangeVisual(String v) { visual = v; }
        public String Draw() { return visual; }
    }
}
