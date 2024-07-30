using Building;
using Landscape;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class GameTest
    {
        //[TestMethod]
        //public void DefaultConstructor()
        //{
        //    var load = new LandscapeTest();
        //    Matrix.Matrix<Cell> place = new Matrix.Matrix<Cell>();
        //    Castle castle = new();
        //    var lairs = new List<Lair>();
        //    load.Loading(ref place, ref castle, ref lairs,
        //        "C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile1.txt");
        //    var game = new Game.Game(place, castle, lairs);
        //    Assert.AreEqual(game.Landscape.Castle, castle);
        //    Assert.AreEqual(game.Landscape.Place, place);
        //    Assert.AreEqual(game.Landscape.Lairs, lairs);
        //}

        public Game.Game Loading(String name)
        {
            Matrix.Matrix<Cell> place;
            Castle castle;
            var lairs = new List<Lair>();
            string path = name;
            using (StreamReader reader = new(path))
            {
                string? line;
                line = reader.ReadLine();
                string[] line2 = line.Split(' ');
                Matrix.Matrix<Cell> place1 = new Matrix.Matrix<Cell>(Int32.Parse(line2[0]), Int32.Parse(line2[1]));
                place = place1;
                line = reader.ReadLine();
                line2 = line.Split(' ');
                for (int i = 0; i < place.Height; i++)
                {
                    for (int j = 0; j < place.Width; j++) place[i, j] = new Cell((TypeCell)Int32.Parse(line2[j + i * place.Width]));
                }
                line = reader.ReadLine();
                line2 = line.Split(' ');
                castle = new Castle(line2[0], Int32.Parse(line2[1]), Int32.Parse(line2[2]), Int32.Parse(line2[3]), Int32.Parse(line2[4]));
                line = reader.ReadLine();
                line2 = line.Split(' ');
                int count = Int32.Parse(line2[0]);
                for (int i = 0; i < count; i++)
                {
                    line = reader.ReadLine();
                    line2 = line.Split(' ');
                    int number_enemy = Int32.Parse(line2[3]);
                    var enemies = new List<Enemy.Enemy>();
                    for (int j = 0; j < number_enemy; j++)
                    {
                        line = reader.ReadLine();
                        string[] line3 = line.Split(' ');
                        var _enemyc = new Enemy.Enemy(line3[0], Int32.Parse(line3[1]), Int32.Parse(line3[2]),
                            Double.Parse(line3[2]), Int32.Parse(line2[1]), Int32.Parse(line2[2]));
                        enemies.Add(_enemyc);
                    }
                    lairs.Add(new Lair(Int32.Parse(line2[0]), Int32.Parse(line2[1]), Int32.Parse(line2[2]), enemies));

                }
            }
            var game = new Game.Game(place, castle, lairs);
            return game;
        }

        [TestMethod]
        public void EnemyAITest()
        {
            var game = Loading(
                "C:\\Users\\user\\Documents\\Tower_Defence#\\Game\\TestProject1\\TextFile5.txt");
            game.MakeMove();
         //   game.MakeMove();
            var enemy1 = new Enemy.EnemyDouble(1, "Villian", 100, 50, 1, 0, 4);
            var effect1 = new Effect.Effect();
            var effect2 = new Effect.Effect(89, 5, 1);
            var effect3 = new Effect.Effect(89, 5, 2);
         //   game.Landscape.Enemies.Add(enemy1);
            var tower1 = new Magic_Tower(effect1, 3, Strategy.Closet_to_castle,
            100, 3, 2, 50, 1, 4);
            var tower2 = new Magic_Tower(effect2, 3, Strategy.Closet_to_tower,
            100, 3, 2, 50, 0, 0);
            var tower3 = new Magic_Trap(effect3, 100, 3, 2, 50, 1, 1);
            game.Landscape.AddTower(tower1);
            game.Landscape.AddTower(tower2);
            game.Landscape.AddTower(tower3);
          //  game.MakeMove();
        }
    }
}
