using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KCKgame
{
    class Enemy : Entity
    {
        enum EnemyState{
            nothing,
            follow,
            attack
        };

        EnemyState state = EnemyState.nothing;
        int PlayerX;
        int PlayerY;
        bool ready = false;
        int vision = 5;
        public Enemy()
        {
            ChangeVisual(":(");

        }

        public void SetPlayerCoordinates(int x, int y)
        {
            PlayerX = x;
            PlayerY = y;
        }

        public void EnemyMovement()
        {
            //krok co dwa ticki
            if (ready)
            {
                //if(state = EnemyState.nothing) - szukaj przeciwnika
                //if(state = EnemyState.follow) - zblizaj sie do przeciwnika
                //if(state = EnemyState.attack) - zadaj obrazenia
                ready = false;
            }
            else
                ready = true;
        }

    }
}
