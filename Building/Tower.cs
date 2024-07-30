using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Effect;
using Enemy;
using System.Runtime.InteropServices;

namespace Building
{
    /// <summary>
    /// Class of default tower
    /// </summary>
    public class Tower : Building
    {
        protected int cost;
        protected int radius;
        protected int harm;
        protected int level;
        protected int endurance_max;
        protected int endurance_now;
      //  protected 

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="c">Initial cost</param>
        /// <param name="r">Initial radius</param>
        /// <param name="h">Initial harm</param>
        /// <param name="em">Initial edurance</param>
        public Tower(int c = 100, int r = 3, int h = 2, int em = 50, int y = 0, int x = 0) : base(y, x)
        {
            cost = c;
            radius = r;
            harm = h;
            level = 1;
            endurance_max = em;
            endurance_now = em;
        }


        /// <summary>
        /// Setter/Getter of radius
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Radius cannot be less than zero");
                radius = value;
            }
        }

        /// <summary>
        /// Setter/Getter of harm
        /// </summary>
        public int Harm
        {
            get { return harm; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Harm cannot be less than zero");
                harm = value;
            }
        }

        /// <summary>
        /// Setter/Getter of level
        /// </summary>
        public int Level
        {
            get { return level; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("Level cannot be less than zero", nameof(level));
                level = value;
            }
        }

        /// <summary>
        /// Setter/Getter of endurance
        /// </summary>
        public int Endurance
        {
            get { return endurance_now; }
            set
            {
               // if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Endurance cannot be less than zero");
                endurance_now = value;
            }
        }

        /// <summary>
        /// Setter/Getter of cost
        /// </summary>
        public int Cost
        {
            get { return cost; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Cost cannot be less than zero");
                cost = value;
            }
        }

        /// <summary>
        /// Level up the tower
        /// </summary>
        public void LevelUp()
        {
            level++;
            radius = (int)(1.1 * radius);
            harm += 10;
        }
        /// <summary>
        /// Deal damage to enemies
        /// </summary>
        /// <param name="enemy">The enemy corresponding to the determined strategy and radius</param>
        public virtual void SpecialAction(Enemy.Enemy enemy)
        {
            int harmforhim = harm;
            harmforhim += (int)(0.01 * enemy.get_force(TypeEffect.Weakening));
            enemy.Health -= harmforhim;
        }
    }

    /// <summary>
    /// Class of simple tower
    /// </summary>
    public class Magic_Tower : Simple_Tower
    {
        Effect.Effect magic_effect;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="magic_effect"></param>
        /// <param name="speed_shooting"></param>
        /// <param name="strategy"></param>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="h"></param>
        /// <param name="em"></param>
        public Magic_Tower(Effect.Effect magic_effect, int speed_shooting = 3,
            Strategy strategy = Strategy.Closet_to_castle,
            int c = 200, int r = 3, int h = 2, int em = 50, int y = 0, int x = 0) :
            base(speed_shooting, strategy, c, r, h, em, y, x)
        {
            this.magic_effect = magic_effect;
        }

        /// <summary>
        /// Setter/Getter of effct for enemies
        /// </summary>
        public Effect.Effect MagicEffect
        {
            get { return magic_effect; }
            set { magic_effect = value; }
        }

        /// <summary>
        /// Apply an effect on the enemy along with damage
        /// </summary>
        /// <param name="enemy">Enemy is got the effect</param>
        public override void SpecialAction(Enemy.Enemy enemy)
        {
            int harmforhim = harm;
            harmforhim += (int)(0.01 * enemy.get_force(TypeEffect.Weakening));
            enemy.Health -= harmforhim;
            enemy.add_effect(magic_effect);
        }
    }

    /// <summary>
    /// Class of magic trap.
    /// <note>It acts after it is stepped on</note>
    /// </summary>
    public class Magic_Trap : Tower
    {
        Effect.Effect magic_effect;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="magic_effect"></param>
        /// <param name="c"></param>
        /// <param name="r"></param>
        /// <param name="h"></param>
        /// <param name="em"></param>
        public Magic_Trap(Effect.Effect magic_effect, int c = 50, int r = 3, int h = 2, int em = 50, int y = 0, int x = 0) :
            base(c, r, h, em, y, x)
        {
            this.magic_effect = magic_effect;
        }

        /// <summary>
        /// Setter/Getter of effct for enemies
        /// </summary>
        public Effect.Effect MagicEffect
        {
            get { return magic_effect; }
            set { magic_effect = value; }
        }

        /// <summary>
        /// Applay effect on the enemy
        /// </summary>
        /// <param name="enemy"></param>
        public override void SpecialAction(Enemy.Enemy enemy)
        {
            enemy.add_effect(magic_effect);
        }
    }

}
