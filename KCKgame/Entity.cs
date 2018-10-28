using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class Entity
    {
        private int level=1;
        private int life=5;
        private int currentLife = 5;
        private int attack = 1;
        private int armor=0;
        public void ChangeLevel(int i) { if (level + i >= 1) level += i; }
        public void ChangeLife(int i) { life += i; }
        public void ChangeCurrentLife(int i) { currentLife += i; }
        public void SetCurrentLife(int i) { currentLife = i; }
        public void ChangeArmor(int i) { armor += i; }
        public void ChangeAttack(int i) { attack += i; }
        public int GiveLevel() { return level; }
        public int GiveLife() { return life; }
        public int GiveCurrentLife() { return currentLife; }
        public int GiveArmor() { return armor; }
        public int GiveAttack() { return attack; }
        public void WearItem(Item i)
        {
            ChangeLife(i.GetLife());
            ChangeAttack(i.GetAttack());
            ChangeArmor(i.GetArmor());
        }

        public void UnwearItem(Item i)
        {
            ChangeLife((-1)*(i.GetLife()));
            ChangeAttack((-1)*(i.GetAttack()));
            ChangeArmor((-1)*(i.GetArmor()));
        }

        private String visual = "!!";
        public void ChangeVisual(String v) { visual = v; }
        public String Draw() { return visual; }
    }
}
