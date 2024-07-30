using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    /// <summary>
    /// Class of enemy lair
    /// </summary>
    public class Lair : Building
    {
        int time_exit;
        public List<Enemy.Enemy>? enemies;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="te"></param>
        public Lair(int te = 10, int y = 0, int x = 0, List<Enemy.Enemy>? enem = null) :
            base(y, x)
        {
            time_exit = te;
            enemies = enem;
        }

        /// <summary>
        /// Constructor with initialization of enemies
        /// </summary>
        /// <param name="enem"></param>
        /// <param name="te"></param>
        public Lair(List<Enemy.Enemy> enem, int te = 10, int y = 0, int x = 0)
        {
            time_exit = te;
            enemies = enem;
            Y = y;
            X = x;
        }

        /// <summary>
        /// Setter/Getter of exit time
        /// </summary>
        public int TimeExit
        {
            get { return time_exit; }
            set { time_exit = value; }
        }

        /// <summary>
        /// Realease the enemy st certain time
        /// </summary>
        /// <returns></returns>
        public Enemy.Enemy release_enemy()
        {
            if (enemies==null || enemies.Count == 0) throw new Exception("There are no more enemies");
            Enemy.Enemy enemy = enemies[0];
            enemies.RemoveAt(0);
            return enemy;
        }

    }
}
