using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Building;
using Enemy;
using Landscape;
using Matrix;

namespace Game
{
    /// <summary>
    /// General class for game
    /// </summary>
    public class Game
    {
        Landscape.Landscape level;
        int time;

        /// <summary>
        /// Default constructor for game
        /// </summary>
        /// <param name="_place"></param>
        /// <param name="_castle"></param>
        /// <param name="_lairs"></param>
        public Game(Matrix<Cell> _place, Castle _castle, List<Lair> _lairs, int _time = 2)
        {
            level = new Landscape.Landscape(_place, _castle, _lairs);
            level.CheckCorrect();
            time = _time;
        }

        public Game(Landscape.Landscape _level, int _time = 2)
        {
            level = _level;
            level.CheckCorrect();
            time = _time;
        }

        /// <summary>
        /// Automated move for Magic Trap
        /// </summary>
        /// <param name="trap"></param>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        void MagicTrapAI(Magic_Trap trap, int _x, int _y)
        {
            for (int k = 0; level.Enemies != null && k < level.Enemies.Count; k++)
            {
                var coordinates_ = level.Enemies[k].Coordinates;
                double distance = Math.Sqrt(Math.Pow(coordinates_.Item1 - _y, 2) + Math.Pow(coordinates_.Item2 - _x, 2));
                if (distance <= trap.Radius) trap.SpecialAction(level.Enemies[k]);
            }
        }

        /// <summary>
        /// Automated move for simple tower and magic tower
        /// </summary>
        /// <param name="simple_tower"></param>
        void TowerAI(Simple_Tower simple_tower)
        {
            int radius_tower = simple_tower.Radius;
            Strategy strategy = simple_tower.Strategy_;
            double min = int.MaxValue;
            int max = 0;
            Enemy.Enemy? target = null;
            for (int k = 0; level.Enemies != null && k < level.Enemies.Count; k++)
            {
                Tuple<int, int> coordinates_ = level.Enemies[k].Coordinates;
                double distance = Math.Sqrt(Math.Pow(coordinates_.Item1 - simple_tower.X, 2)
                                + Math.Pow(coordinates_.Item2 - simple_tower.Y, 2));
                if ((double)radius_tower >= distance)
                {
                    if (strategy == Strategy.Closet_to_tower)
                    {
                        if (distance < min)
                        {
                            min = distance;
                            target = level.Enemies[k];
                        }
                    }
                    if (strategy == Strategy.Closet_to_castle)
                    {
                        double distance_castle = Math.Sqrt(Math.Pow(level.Castle.X - simple_tower.X, 2)
                                                + Math.Pow(level.Castle.Y - simple_tower.Y, 2));
                        if (distance_castle < min)
                        {
                            min = distance_castle;
                            target = level.Enemies[k];
                        }
                    }
                    if (strategy == Strategy.Strongest)
                    {
                        if (level.Enemies[k].Health > max)
                        {
                            max = level.Enemies[k].Health;
                            target = level.Enemies[k];
                        }
                    }
                    if (strategy == Strategy.Weakest)
                    {
                        if (level.Enemies[k].Health < min)
                        {
                            min = level.Enemies[k].Health;
                            target = level.Enemies[k];
                        }
                    }
                    if (strategy == Strategy.Fastest)
                    {
                        if (level.Enemies[k].Velocity > max)
                        {
                            max = level.Enemies[k].Velocity;
                            target = level.Enemies[k];
                        }
                    }
                }
                if (target != null) simple_tower.SpecialAction(target);

            }
        }
        private Mutex mutKillTower = new Mutex();
        /// <summary>
        /// Automated move for enemy
        /// </summary>
        /// <param name="i"></param>
        void EnemyAI(int i)
        {
            if (level.Enemies[i].Health <= 0)
            {
                level.Castle.Gold += level.Enemies[i].Gold;
               // level.Enemies.RemoveAt(i);
            }
            else
            {
                var coordinates = level.GiveNextCoordinates(level.Enemies[i]);
                int _x = coordinates.Item2;
                int _y = coordinates.Item1;
                level.Enemies[i].make_move(coordinates.Item1, coordinates.Item2);
                if (level.Enemies[i] is EnemyAttack)
                {
                    int radius_enemy = ((EnemyAttack)level.Enemies[i]).Radius;
                    double min = int.MaxValue;
                    Tower? target = null;
                    foreach (Simple_Tower _tower in level.SimpleTowers)
                    {
                        double distance = Math.Sqrt(Math.Pow(_x - _tower.X, 2) + Math.Pow(_y - _tower.Y, 2));
                        if (distance < min && distance <= radius_enemy)
                        {
                            min = distance;
                            target = _tower;
                        }
                    }
                    foreach (Magic_Trap _tower in level.Traps)
                    {
                        double distance = Math.Sqrt(Math.Pow(_x - _tower.X, 2) + Math.Pow(_y - _tower.Y, 2));
                        if (distance < min && distance <= radius_enemy)
                        {
                            min = distance;
                            target = _tower;
                        }
                    }

                    mutKillTower.WaitOne();
                    if (target != null)
                    {
                        target.Endurance -= ((EnemyAttack)level.Enemies[i]).Harm;
                        if (target.Endurance <= 0)
                        {
                            level.Place[target.Y, target.X].Building = null;
                            if (target is Simple_Tower) level.SimpleTowers.Remove((Simple_Tower)target);
                            else level.Traps.Remove((Magic_Trap)target);
                        }
                    }
                    mutKillTower.ReleaseMutex();
                }
                if (level.Enemies[i] is EnemyDouble)
                {
                    if (time % ((EnemyDouble)level.Enemies[i]).DevideTime == 0)
                    {
                        var new_enemy = ((EnemyDouble)level.Enemies[i]).copy_enemy();
                        level.Enemies.Add(new_enemy);
                    }
                }
                if (level.Place[_y, _x].Building is Magic_Trap)
                    MagicTrapAI((Magic_Trap)level.Place[_y, _x].Building, _x, _y);
                //mutKillTower.WaitOne();
                if (level.Place[_y, _x].Building is Castle)
                {
                    ((Castle)level.Place[_y, _x].Building).MakeHarm(level.Enemies[i]);
                    level.Enemies[i].Health = 0;
                }
                //mutKillTower.ReleaseMutex();
            }
        }

        /// <summary>
        /// Everything make move
        /// </summary>
        public void MakeMove()
        {
            //Parallel.ForEach(level.SimpleTowers, tower => TowerAI(tower));
            int k = 0;
            if (level.SimpleTowers != null) k = level.SimpleTowers.Count;
            if (level.Enemies != null) k += level.Enemies.Count;
            Task[] tasks_enemies_tower;
            if (k != 0)
            {

                tasks_enemies_tower = new Task[k];
                for (int i = 0; i < level.Enemies.Count; i++)
                {
                    //   Parallel.For(EnemyAI(i));
                    tasks_enemies_tower[i] = Task.Run(() => EnemyAI(i));
                    EnemyAI(i);
                }
                int count = level.Enemies.Count;
                if (level.SimpleTowers != null)
                {
                    foreach (Simple_Tower simple_tower in level.SimpleTowers)
                    {
                        tasks_enemies_tower[count] = Task.Run(() => TowerAI(simple_tower));
                        TowerAI(simple_tower);
                        count++;
                    }
                }
                //  Task.WaitAll(tasks_enemies_tower);
            }

        }

        /// <summary>
        /// Setter/Getter of landscape
        /// </summary>
        public Landscape.Landscape Landscape
        {
            get { return level; }
            set { level = value; }
        }


    }
}