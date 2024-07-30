
using Building;
using Matrix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Landscape
{
    /// <summary>
    /// Field of game
    /// </summary>
    public class Landscape
    {
        List<Enemy.Enemy?>? enemies;
        Matrix.Matrix<Cell> place;
        readonly int[] way_to_castle;
        Castle castle;
        List<Simple_Tower>? simple_towers;
        List<Magic_Trap>? traps;
        List<Lair> lairs;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Landscape()
        {
            place = new Matrix.Matrix<Cell>();
            way_to_castle = new int[place.Width * place.Height];
            simple_towers = new List<Simple_Tower>();
            lairs = new List<Lair>();
            traps = new List<Magic_Trap>();
            enemies = new List<Enemy.Enemy?>();
        }

        /// <summary>
        /// Constructor with initialization of parametrs
        /// </summary>
        /// <param name="_place"></param>
        /// <param name="_castle"></param>
        /// <param name="_lairs"></param>
        public Landscape(Matrix<Cell> _place, Castle _castle, List<Lair> _lairs)
        {
            this.place = _place;
            this.way_to_castle = new int[place.Width * place.Height];
            this.castle = _castle;
            this.lairs = _lairs;
            simple_towers = new List<Simple_Tower>();
            traps = new List<Magic_Trap>();
            enemies = new List<Enemy.Enemy?>();
        }

        /// <summary>
        /// Setter/Getter of X
        /// </summary>
        public int Width
        {
            get { return place.Width; }
            set { place.Width = value; }
        }

        /// <summary>
        /// Setter/Getter of Y
        /// </summary>
        public int Height
        {
            get { return place.Height; }
            set { place.Height = value; }
        }

        /// <summary>
        /// setter/Getter of active enemies
        /// </summary>
        public List<Enemy.Enemy?>? Enemies
        {
            get { return enemies; }
            set { enemies = value; }
        }

        /// <summary>
        /// Setter/Getter of map
        /// </summary>
        public Matrix.Matrix<Cell> Place
        {
            get { return place; }
            set { place = value; }
        }

        /// <summary>
        /// Setter/Getter of list of towers
        /// </summary>
        public List<Simple_Tower>? SimpleTowers
        {
            get { return simple_towers; }
            set { simple_towers = value; }
        }

        /// <summary>
        /// Setter/Getter of castle
        /// </summary>
        public Castle Castle { get { return castle; } set { castle = value; } }

        /// <summary>
        /// Setter/Getter of list of lairs 
        /// </summary>
        public List<Lair> Lairs { get { return lairs; } set { lairs = value; } }

        /// <summary>
        /// Setter/Getter of list of traps
        /// </summary>
        public List<Magic_Trap>? Traps { get { return traps; } set { traps = value; } }

        /// <summary>
        /// Finding the shortest way from lair to the castle
        /// </summary>
        /// <param name="Graph"></param>
        /// <param name="i">The Y coordinate of the lair</param>
        /// <param name="j">The X coordinate of the lair</param>
        /// <param name="castle">The coordinate of castle</param>
        /// <returns></returns>
        public void Way(Matrix<int> graph)
        {
            int n = Width * Height;
            int[] distances = new int[n];
            //  way_to_castle = new int[n];

            bool[] visited = new bool[n];
            for (int i = 0; i < n; i++)
            {
                distances[i] = int.MaxValue;
                visited[i] = false;
            }

            distances[castle.Y * Width + castle.X] = 0;
            way_to_castle[castle.Y * Width + castle.X] = -1;

            for (int count = 0; count < n - 1; count++)
            {
                int minDistance = int.MaxValue;
                int minVertex = -1;

                for (int v = 0; v < n; v++)
                {
                    if (!visited[v] && distances[v] < minDistance)
                    {
                        minDistance = distances[v];
                        minVertex = v;
                    }
                }

                if (minVertex == -1)
                    break;

                visited[minVertex] = true;

                for (int u = 0; u < n; u++)
                {
                    if (graph[minVertex, u] != 0 && !visited[u] && distances[minVertex] != int.MaxValue &&
                        distances[minVertex] + graph[minVertex, u] < distances[u])
                    {
                        distances[u] = distances[minVertex] + graph[minVertex, u];
                        way_to_castle[u] = minVertex;
                    }
                }
            }
            int[] array = new int[]{ int.MaxValue, int.MaxValue, int.MaxValue,
                8, 3, int.MaxValue, 7, 8, 13, int.MaxValue, int.MaxValue, 6, int.MaxValue, -1, 13,
            int.MaxValue, int.MaxValue, 18, 13, 14, int.MaxValue, int.MaxValue, 17, int.MaxValue, int.MaxValue};
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] != way_to_castle[i]) throw new ArgumentException("The lair should be on road");
            }
        }

        /// <summary>
        /// Check the level correctness
        /// </summary>
        /// <returns></returns>
        public void CheckCorrect()
        {
            Array.Fill(way_to_castle, int.MaxValue);
            var Graph = new Matrix.Matrix<int>(Width * Height, Width * Height);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (place[i, j].CellType == TypeCell.Road)
                    {
                        if (i - 1 >= 0 && place[i - 1, j].CellType == TypeCell.Road)
                        {
                            Graph[i * Width + j, (i - 1) * Width + j] = 1;
                        }
                        if (i + 1 < Height && place[i + 1, j].CellType == TypeCell.Road)
                        {
                            Graph[i * Width + j, (i + 1) * Width + j] = 1;
                        }
                        if (j + 1 < Width && place[i, j + 1].CellType == TypeCell.Road)
                        {
                            Graph[i * Width + j, i * Width + j + 1] = 1;
                        }
                        if (j - 1 >= 0 && place[i, j - 1].CellType == TypeCell.Road)
                        {
                            Graph[i * Width + j, i * Width + j - 1] = 1;
                        }
                    }
                }
            }

            if (place[castle.Y, castle.X].CellType != TypeCell.Road)
                throw new ArgumentException("The castle should be on road", nameof(castle));
            place[castle.Y, castle.X].Building = castle;
            int[,] newmatr = new int[,]
            {
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,1,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,1,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                {0, 0 , 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
            };

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {

                if (Graph[i,j] != newmatr[i,j]) throw new ArgumentException("The lair should be on road");
                }
            }

            Way(Graph);
            foreach (Lair construction in lairs)
            {
                if (place[construction.Y, construction.X].CellType != TypeCell.Road)
                    throw new ArgumentException("The lair should be on road", nameof(construction));
                place[construction.Y, construction.X].Building = construction;
                if (way_to_castle[construction.Y * Width + construction.X] == int.MaxValue) throw new ArgumentException("The lair should be on road", nameof(construction));
            }

        }

        /// <summary>
        /// New coordinates for enemy moving 
        /// </summary>
        /// <param name="_enemy">Enemy if given coordinates</param>
        /// <returns>new coordinates</returns>
        public Tuple<int, int> GiveNextCoordinates(Enemy.Enemy _enemy)
        {
                int coordinates = way_to_castle[_enemy.Coordinates.Item1 * Width + _enemy.Coordinates.Item2];
            return new Tuple<int, int>(coordinates / Width, coordinates % Width);
        }


        public void AddTower(Tower tower)
        {
            if (tower is Magic_Trap)
            {
                if (place[tower.Y, tower.X].CellType != TypeCell.Road)
                    throw new ArgumentException("The trap should be on road", nameof(tower));
                traps.Add((Magic_Trap)tower);
                castle.Gold -= tower.Cost;
            }
            else
            {
                if (place[tower.Y, tower.X].CellType != TypeCell.Field)
                    throw new ArgumentException("The tower should be on field", nameof(tower));
                simple_towers.Add((Simple_Tower)tower);
                castle.Gold -= tower.Cost;
            }
        }
    }
}
