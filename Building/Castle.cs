using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    /// <summary>
    /// Class of castle
    /// </summary>
    public class Castle : Building
    {
        readonly string name;
        readonly int endurance_max;
        int endurance_now;
        int gold;

        /// <summary>
        /// Default constructor of castle
        /// </summary>
        /// <param name="n"></param>
        /// <param name="em"></param>
        /// <param name="g"></param>
        public Castle(string n = "White house", int em = 1000, int g = 10000, int y = 0, int x = 0) :
            base(y, x)
        {
            name = n;
            endurance_now = em;
            endurance_max = em;
            gold = g;
        }

        /// <summary>
        /// Castle is damaged by enemy
        /// </summary>
        /// <param name="enemy"></param>
        public void MakeHarm(Enemy.Enemy enemy)
        {
            Endurance -= enemy.Health;
        }

        /// <summary>
        /// Setter/Getter of gold
        /// </summary>
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }

        /// <summary>
        /// Endurance of castle
        /// </summary>
        public int Endurance
        {
            get { return endurance_now; }
            set
            {
                endurance_now = value;
               // if (endurance_now <= 0) throw new ArgumentOutOfRangeException("The castle is defeated, the game is lost", nameof(endurance_now));
            }
        }

        /// <summary>
        /// Getter of name
        /// </summary>
        public String Name
        {
            get { return name; }
        }

        /// <summary>
        /// Getter of initial endurance
        /// </summary>
        public int EnduranceMax { get { return endurance_max; } }

    }
}
