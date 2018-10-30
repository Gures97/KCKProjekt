﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class Entity
    {
        protected int level=1;
        protected int life=5;
        protected int currentLife = 5;
        protected int attack = 1;
        protected int armor=0;
        public void ChangeLevel(int i) { if (level + i >= 1) level += i; }
        public void ChangeLife(int i) { life += i; }
        public void ChangeCurrentLife(int i) { currentLife += i; }
        public void ChangeArmor(int i) { armor += i; }
        public void ChangeAttack(int i) { attack += i; }
        public int GetLevel() { return level; }
        public int GetLife() { return life; }
        public int GetCurrentLife() { return currentLife; }
        public int GetArmor() { return armor; }
        public int GetAttack() { return attack; }
        public void SetCurrentLife(int i) { currentLife = i; }
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
