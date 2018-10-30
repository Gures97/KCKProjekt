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
        Map map;
        bool ready = false;
        int vision;
        public int enemyX;
        public int enemyY;
        int tier;
        public Enemy(Map map, int x, int y, int tier)
        {
            ChangeVisual(":(");
            this.map = map;
            enemyX = x;
            enemyY = y;
            armor = tier;
            life = 5 + tier;
            attack = 1 + tier;
            currentLife = life;
            level = tier;
            vision = 5 + tier;
        }

        public void EnemyMovement()
        {
            int playerX = map.currentPosX;
            int playerY = map.currentPosY;
            //krok co dwa ticki
            if (ready)
            {
                if (state == EnemyState.nothing)
                {
                    if (Math.Abs(enemyX - playerX) <= vision && Math.Abs(enemyY - playerY) <= vision)
                        state = EnemyState.follow;
                }
                if (state == EnemyState.follow)
                {
                    if (Math.Abs(enemyX - playerX) > vision || Math.Abs(enemyY - playerY) > vision)
                        state = EnemyState.nothing;
                    else
                    {
                        MoveToPlayer();
                    }
                    if(Math.Abs(enemyX - playerX) <= 1 && Math.Abs(enemyY - playerY) <= 1)
                    {
                        state = EnemyState.attack;
                    }

                }
                if (state == EnemyState.attack)
                {
                    if (Math.Abs(enemyX - playerX) > 1 || Math.Abs(enemyY - playerY) > 1)
                    {
                        state = EnemyState.follow;
                    }
                    map.character.ChangeCurrentLife(-attack+map.character.GetArmor());
                }
                ready = false;
            }
            else
            {
                ready = true;
            }
        }

        public void MoveToPlayer()
        {
            int Xdiff = map.currentPosX - enemyX; // + gracz po prawej    - gracz po lewej
            int Ydiff = map.currentPosY - enemyY; // + gracz wyzej        - gracz nizej

            if(Xdiff != 0)
            {
                if(Xdiff > 0 && map.stage[enemyX-1][enemyY].Enterable())
                {
                    enemyX--;
                }
                else if(Xdiff < 0 && map.stage[enemyX + 1][enemyY].Enterable())
                {
                    enemyX++;
                }
            }
            if (Ydiff != 0)
            {
                if (Ydiff > 0 && map.stage[enemyX][enemyY - 1].Enterable())
                {
                    enemyY--;
                }
                else if (Ydiff < 0 && map.stage[enemyX][enemyY + 1].Enterable())
                {
                    enemyY++;
                }
            }
        }

    }
}
