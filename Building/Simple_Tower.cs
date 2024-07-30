using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effect;
using Enemy;

namespace Building
{
    public class Simple_Tower: Tower
    {
        int speed_shooting;
        Strategy strategy;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="speed_shooting"></param>
        /// <param name="strategy"></param>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="h"></param>
        /// <param name="em"></param>
        public Simple_Tower(int speed_shooting = 3, Strategy strategy = Strategy.Closet_to_castle,
            int c = 100, int r = 3, int h = 2, int em = 50, int y=0, int x=0) :
            base(c, r, h, em, y, x)
        {
            this.speed_shooting = speed_shooting;
            this.strategy = strategy;
        }

        /// <summary>
        /// Setter/Getter of speed shooting
        /// </summary>
        public int SpeedShooting
        {
            get { return speed_shooting; }
            set {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Speed shooting cannot be less than zero");
                speed_shooting = value; }
        }

        /// <summary>
        /// Setter/Getter of strategy
        /// </summary>
        public Strategy Strategy_ { get { return strategy; } 
            set {
                if (value < 0 || value > (Strategy)4) 
                    throw new ArgumentOutOfRangeException(nameof(value), "Strategy cannot be less than zero");
                strategy = value; } }

    }
}
