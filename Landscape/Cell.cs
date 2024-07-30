using System;
using System.Numerics;
using Building;
using Matrix;

namespace Landscape
{

    /// <summary>
    /// Type of space
    /// 0 - Field, 1 - Forest, 2 - Road
    /// </summary>
    public enum TypeCell
    {
        Field,
        Forest,
        Road
    };

    /// <summary>
    /// Class of cell
    /// </summary>
    public class Cell : IComparable<Cell>
    {
        TypeCell typecell;
        Building.Building? building;

        /// <summary>
        /// Default constructor 
        /// </summary>
        /// <param name="typecell">Type of space</param>
        public Cell()
        {
            this.typecell = TypeCell.Forest;
            this.building = null;
        }

        /// <summary>
        /// Constructor with initialization of parametrs
        /// </summary>
        /// <param name="_typecell"></param>
        /// <param name="building"></param>
        public Cell(TypeCell _typecell=TypeCell.Forest, Building.Building? _building=null)
        {
            this.typecell = _typecell;
            this.building = _building;
        }
        /// <summary>
        /// Operator equality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(Cell lhs, Cell rhs)
        {
            if ((int)lhs.typecell != (int)rhs.typecell) return false;
            if (lhs.building == null && rhs.building == null) return true;
            else if (lhs.building == null || rhs.building == null) return false;
            Type t = rhs.building.GetType();
            if (lhs.building.GetType() != t) return false;
            return true;
        }

        /// <summary>
        /// Operator unequality
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(Cell lhs, Cell rhs)
        {
            return !(lhs == rhs);
        }


        /// <summary>
        /// Setter/Getter of type of cell
        /// </summary>
        public TypeCell CellType
        {
            get { return typecell; }
            set { typecell = value; }
        }

        /// <summary>
        /// Setter/Getter of building
        /// </summary>
        public Building.Building? Building
        {
            get { return building; }
            set { building = value; }
        }

        /// <summary>
        /// Comparison of two parametrs
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int CompareTo(Cell? other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return this==(Cell)obj;
        }
    }
}