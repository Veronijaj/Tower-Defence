using System;
using System.Numerics;

namespace Effect
{
    public enum TypeEffect
    {
        Slow_motion,
        Poisoning,
        Weakening
    };
    /// <summary>
    /// Class of effects imposed on the enemies
    /// </summary>
    public class Effect
    {
        int force;
        int duration;
        TypeEffect effectType;

        /// <summary>
        /// Default constructor of effect 
        /// </summary>
        public Effect()
        {
            force = 0;
            duration = 0;
            effectType = TypeEffect.Slow_motion;
        } 
        /// <summary>
        /// Constructor with initialization of force and duration
        /// </summary>
        /// <param name="f"></param>
        /// <param name="d"></param>
        public Effect(int f, int d, int te)
        {
            if (f <= 0) throw new ArgumentOutOfRangeException("Force cannot be less than zero", nameof(f));
            if (d <= 0) throw new ArgumentOutOfRangeException("Duration cannot be less than zero", nameof(d));
            if (te < 0 || te>2) throw new ArgumentOutOfRangeException("There isnt this effect", nameof(te));
            force = f;
            duration = d;
            effectType = (TypeEffect)te;
        }

        /// <summary>
        /// Get/Set force
        /// </summary>
        public int Force {  
            set {
                if(value < 0) throw new ArgumentOutOfRangeException("Force cannot be less than zero", nameof(force)) ;
                force = value; }
            get { return force; } 
        }

        /// <summary>
        /// Get/Set duration
        /// </summary>
        public int Duration
        {
            set
            {
                //if(value < 0) throw new ArgumentOutOfRangeException("Duration cannot be less than zero", nameof(duration));
                duration = value;
            }
            get { return duration; }
        }

        /// <summary>
        /// Get type of effect
        /// </summary>
        public TypeEffect Type { 
            set {
                if ((int)value < 0 || (int)value > 2) throw new ArgumentOutOfRangeException("There isnt this effect", nameof(value));
                effectType = value; 
            }
            get { return effectType; } 
        }

    };
}

