using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class Item : Terrain
    {
        private int type; //1=weapon 2=armor 3=artefact 4=potion
        private int tier = 4;
        private String name = " [^] Tajemniczy eliksir";
        private String[] properties = new String[2];
        private String description = "Przedmiot o nieznanym pochodzeniu";
        private int life, attack, armor, restoration=3, charges=1;

        public void SetName(String s)
        {
            String temp = "";
            if (type == 1)
                temp+=" =|=> ";
            else if (type == 2)
                temp+=" /[]\\ ";
            else if (type == 3)
                temp+=" <O> ";
            else if (type == 4)
                temp+=" [^] ";
            name = temp + s;
        }
        public void SetDescription(String s) { description = s; }
        public String GetName() { return name; }
        public String GetDescription() { return description; }
        public int GetItemType() { return type; }
        public int GetLife() { return life; }
        public int GetAttack() { return attack; }
        public int GetArmor() { return armor; }
        public int GetRestoration() { return restoration; }
        public int GetCharges() { return charges;  }
        public void ChangeCharges(int i) { charges += i; }

        public Item(int t)
        {
            SetDescription("");
            SetName("Pusty");
        }

        public Item(int t, int l, int a, int ar)
        {
            CanEnterNow();
            type = t;
            if (type == 1)
                SetName("Tajemniczy miecz");
            else if (type == 2)
                SetName("Tajemnicza zbroja");
            else if (type == 3)
                SetName("Tajemniczy artefakt");

            life = l;
            attack = a;
            armor = ar;
            int counter = 0;
            if (life != 0)
            { properties[counter] = life.ToString() + " zycie"; counter++; }
            if (attack != 0)
            { properties[counter] = attack.ToString() + " atak"; counter++; }
            if (armor != 0 && counter<2)
            { properties[counter] = armor.ToString() + " pancerz"; }
            if (properties[1] == null)
                properties[1] = "";

            ChangeVisual("[]");
        }
        public Item(int t, int l, int a, int ar, int r, int c)
        {
            CanEnterNow();
            type = t;
            life = l;
            attack = a;
            armor = ar;
            restoration = r;
            charges = c;
            if (life != 0)
            properties[0] = "Stale zwieksza zycie o " + life.ToString();
            else if (attack != 0)
            properties[0] = "Stale zwieksza atak o " + attack.ToString();
            else if (armor != 0)
            properties[0] = "Stale zwieksza pancerz o " + armor.ToString();
            else if (restoration != 0)
            properties[0] = "Odnawia " + restoration.ToString() + " zycia";

            properties[1] = "Pozostale uzycia: " + charges.ToString();

            ChangeVisual("{}");
        }

        public String GetFirstProperty()
        {
            return properties[0];
        }

        public String GetSecondProperty()
        {
            return properties[1];
        }

        public void ActualizeCharges()
        {
            properties[1] = "Pozostale uzycia: " + charges.ToString();
        }
    }
}
