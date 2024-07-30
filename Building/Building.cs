using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Building
{
    public enum Strategy
    {
        Closet_to_tower,
        Closet_to_castle,
        Weakest,
        Strongest,
        Fastest
    };

    /// <summary>
    /// Class of building
    /// </summary>
    public class Building
    {
        int x, y;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Building(int y = 0, int x = 0)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Getter/Setter of X
        /// </summary>
        public int X { get { return x; } set { x = value; } }

        /// <summary>
        /// Getter/Setter of Y
        /// </summary>
        public int Y { get { return y; } set { y = value; } }

    }
}