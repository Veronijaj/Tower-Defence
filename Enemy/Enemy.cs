using System;
using Effect;

namespace Enemy
{
    /// <summary>
    /// Class of enemies
    /// </summary>
    public class Enemy
    {
        string name;
        protected int health_now;
        protected int health_max;
        protected int gold;
        protected double velocity;
        public int x;
        public int y;
        List<Effect.Effect>? effects;
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="n">Name of enemy</param>
        /// <param name="hm">Initial health</param>
        /// <param name="v">Initial velocity</param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>

        public Enemy(string n = "Villian", int hm = 100, int g = 0, double v = 1, int y1 = 0, int x1 = 0)
        {
            name = n;
            health_max = hm;
            health_now = hm;
            gold = g;
            velocity = v;
            x = x1;
            y = y1;
        }
        /// <summary>
        /// Addition effect
        /// </summary>
        /// <param name="effect"></param>
        public void add_effect(Effect.Effect effect)
        {
            if (effects == null)
            {
                effects = new List<Effect.Effect>();
            }
            effects.Add(effect);
        }

        /// <summary>
        /// Setter/Getter health
        /// </summary>
        public int Health
        {
            get { return health_now; }
            set
            {
              //  if (value < 0) throw new ArgumentOutOfRangeException("Health cannot be less than zero", nameof(value));
                health_now = value;
            }
        }

        /// <summary>
        /// Setter/Getter velocity
        /// </summary>
        public int Velocity
        {
            get
            {
                int force = get_force(TypeEffect.Slow_motion);
                return (int)(1 - 0.01 * force > 0 ? (1 - 0.01 * force) * velocity : 0);
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("Velocity cannot be less than zero", nameof(value));
                velocity = value;
            }
        }

        /// <summary>
        /// Get of name
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Getter Gold
        /// </summary>
        public int Gold
        {
            get { return gold; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("Gold cannot be less than zero", nameof(value));
                gold = value;
            }
        }

        /// <summary>
        /// Getter of coordinates
        /// </summary>
        public Tuple<int, int> Coordinates
        {
            get { return new Tuple<int, int>(y, x); }
        }

        public Effect.Effect geteffect(int i)
        {
            if (effects == null || i >= effects.Count)
                throw new ArgumentOutOfRangeException("There isn't this effect", nameof(effects));
            return effects[i];
        }

        /// <summary>
        /// Calculate health considering effects
        /// </summary>
        public void calculate_health()
        {
            int force = get_force(TypeEffect.Poisoning);
            health_now -= force;
            if (health_now < 0) health_now = 0;
        }

        /// <summary>
        /// Enemy make move
        /// </summary>
        /// <param name="x1">new coordinate x</param>
        /// <param name="y1">new coordinate y</param>
        public void make_move(int y1, int x1)
        {
            calculate_health();
            for (int i = 0; effects != null && i < effects.Count; i++)
            {
                effects[i].Duration--;
                if (effects[i].Duration == 0)
                {
                    effects.RemoveAt(i);
                    i--;
                }
            }
            x = x1;
            y = y1;
        }

        /// <summary>
        /// Getter of the force of effects
        /// </summary>
        public int get_force(TypeEffect te)
        {
            int force = 0;
            for (int i = 0; effects != null && i < effects.Count; i++)
                if (effects[i].Type == te)
                    force += effects[i].Force;
            return force;
        }
    };

    /// <summary>
    /// At a certain time enemy devide by two
    /// </summary>
    public class EnemyDouble : Enemy
    {
        int devide_time;

        /// <summary>
        /// Constructor of enemy
        /// </summary>
        /// <param name="devide_time"></param>
        /// <param name="n"></param>
        /// <param name="hm"></param>
        /// <param name="g"></param>
        /// <param name="v"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        public EnemyDouble(int devide_time=30, string n = "Villian", int hm = 100, int g = 0, double v = 1, int y1 = 0, int x1 = 0)
            : base(n, hm, g, v, y1, x1)
        {
            this.devide_time = devide_time;
        }

        /// <summary>
        /// Setter/getter of devide time
        /// </summary>
        public int DevideTime { 
            get { return devide_time; }
            set { devide_time = value; }
        }

        /// <summary>
        /// Copy enemy
        /// </summary>
        /// <returns></returns>
        public EnemyDouble copy_enemy()
        {
            var other = new EnemyDouble(devide_time, Name, health_max, gold, velocity, y, x);
            return other;
        }

    }

    /// <summary>
    /// Enemy which attack 
    /// </summary>
    public class EnemyAttack : Enemy
    {
        int harm;
        int radius;
        /// <summary>
        /// Constructor of enemy
        /// </summary>
        /// <param name="h"></param>
        /// <param name="n"></param>
        /// <param name="hm"></param>
        /// <param name="g"></param>
        /// <param name="v"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        public EnemyAttack(int h=10, string n = "Villian", int hm = 100, int g = 0, double v = 1, int y1 = 0, int x1 = 0) 
            :base(n, hm, g, v, y1, x1){
            harm = h;
        }

        /// <summary>
        /// Setter/Getter of harm
        /// </summary>
        public int Harm
        {
            get { 
                return harm; }
            set {
                harm = value; }
        }

        /// <summary>
        /// Setter/Getter of damage range 
        /// </summary>
        public int Radius{
            get { return radius; }
            set { radius = value; }
        }
    }
}