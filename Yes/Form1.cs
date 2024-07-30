using Building;
using Landscape;
using System.Runtime.InteropServices;
using Effect;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Enemy;
using System;

namespace Yes
{

    public enum Click
    {
        zero,
        first,
        second
    }

    public partial class Form1 : Form
    {
        List<Button> buttons = new List<Button>();
        List<Button> strategybutton = new List<Button>();
        List<Button> effectbutton = new List<Button>();
        List<Label> castleinf = new List<Label>();
        List<System.Windows.Forms.Timer> timer = new List<System.Windows.Forms.Timer>();
        Game.Game game;
        Landscape.Landscape level = new Landscape.Landscape();
        PictureBox? currentpicture = null;
        bool isClick = false;
        bool isClick1 = false;
        Tower? currenttower = null;
        ProgressBar healthcastle = new ProgressBar();
        List<PictureBox> pictureenemy = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            Init();

        }

        public void strategyClick(object sender, System.EventArgs e)
        {
            if (((Button)sender).Text == "closest to tower")
            {
                ((Simple_Tower)currenttower).Strategy_ = Strategy.Closet_to_tower;
            }
            if (((Button)sender).Text == "closest to castle")
            {
                ((Simple_Tower)currenttower).Strategy_ = Strategy.Closet_to_castle;
            }
            if (((Button)sender).Text == "weakest")
            {
                ((Simple_Tower)currenttower).Strategy_ = Strategy.Weakest;
            }
            if (((Button)sender).Text == "strongest")
            {
                ((Simple_Tower)currenttower).Strategy_ = Strategy.Strongest;
            }
            if (((Button)sender).Text == "fastest")
            {
                ((Simple_Tower)currenttower).Strategy_ = Strategy.Fastest;
            }
            for (int i = 0; i < strategybutton.Count; i++)
            {
                this.Controls.Remove(strategybutton[i]);
            }
            if (isClick1 == true)
            {
                for (int i = 0; i < effectbutton.Count; i++)
                {
                    this.Controls.Add(effectbutton[i]);
                    effectbutton[i].BringToFront();
                }
            }
            else hightLight(TypeCell.Field);
        }

        public void effectClick(object sender, System.EventArgs e)
        {

            if (((Button)sender).Text == "slowing down")
            {
                if (isClick1 == true) ((Magic_Tower)currenttower).MagicEffect.Type = TypeEffect.Slow_motion;
                else ((Magic_Trap)currenttower).MagicEffect.Type = TypeEffect.Slow_motion;
            }
            if (((Button)sender).Text == "poisoning")
            {
                if (isClick1 == true) ((Magic_Tower)currenttower).MagicEffect.Type = TypeEffect.Poisoning;
                else ((Magic_Trap)currenttower).MagicEffect.Type = TypeEffect.Poisoning;
            }
            if (((Button)sender).Text == "weakening")
            {
                if (isClick1 == true) ((Magic_Tower)currenttower).MagicEffect.Type = TypeEffect.Weakening;
                else ((Magic_Trap)currenttower).MagicEffect.Type = TypeEffect.Weakening;
            }
            for (int i = 0; i < effectbutton.Count; i++)
            {
                this.Controls.Remove(effectbutton[i]);
            }
            //  isClick1 = false;
            if (isClick1 == true) hightLight(TypeCell.Field);
            else hightLight(TypeCell.Road);
        }

        public void hightLight(TypeCell tc)
        {
            if (isClick == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (game.Landscape.Place[i, j].CellType == tc)
                        {
                            Button tmp = buttons[i * game.Landscape.Width + j];
                            tmp.FlatStyle = FlatStyle.Standard;

                        }
                    }
                }
            }
            isClick = true;
            isClick1 = false;
        }

        public void InitStrategy()
        {
            Button butt1 = new Button();
            butt1.Size = new Size(128, 38);
            butt1.Text = "closest to tower";
            butt1.Location = new Point(64 * 6 + 32, 1 * 64);
            butt1.Click += new EventHandler(strategyClick);
            strategybutton.Add(butt1);

            Button butt2 = new Button();
            butt2.Size = new Size(128, 38);
            butt2.Text = "closest to castle";
            butt2.Location = new Point(64 * 6 + 32, 1 * 64 + 38);
            butt2.Click += new EventHandler(strategyClick);
            strategybutton.Add(butt2);

            Button butt3 = new Button();
            butt3.Size = new Size(128, 38);
            butt3.Text = "weakest";
            butt3.Location = new Point(64 * 6 + 32, 1 * 64 + 38 * 2);
            butt3.Click += new EventHandler(strategyClick);
            strategybutton.Add(butt3);

            Button butt4 = new Button();
            butt4.Size = new Size(128, 38);
            butt4.Text = "strongest";
            butt4.Location = new Point(64 * 6 + 32, 1 * 64 + 38 * 3);
            butt4.Click += new EventHandler(strategyClick);
            strategybutton.Add(butt4);

            Button butt5 = new Button();
            butt5.Size = new Size(128, 38);
            butt5.Text = "fastest";
            butt5.Location = new Point(64 * 6 + 32, 1 * 64 + 38 * 4);
            butt5.Click += new EventHandler(strategyClick);
            strategybutton.Add(butt5);
        }

        public void InitEffect()
        {
            Button butt1 = new Button();
            butt1.Size = new Size(128, 64);
            butt1.Text = "slowing down";
            butt1.Location = new Point(64 * 6 + 32, 1 * 64);
            butt1.Click += new EventHandler(effectClick);
            effectbutton.Add(butt1);

            Button butt2 = new Button();
            butt2.Size = new Size(128, 64);
            butt2.Text = "poisoning";
            butt2.Location = new Point(64 * 6 + 32, 1 * 64 + 64);
            butt2.Click += new EventHandler(effectClick);
            effectbutton.Add(butt2);

            Button butt3 = new Button();
            butt3.Size = new Size(128, 64);
            butt3.Text = "weakening";
            butt3.Location = new Point(64 * 6 + 32, 1 * 64 + 64 * 2);
            butt3.Click += new EventHandler(effectClick);
            effectbutton.Add(butt3);
        }

        public void simpletowerClick(object sender, System.EventArgs e)
        {
            if (game.Landscape.Castle.Gold >= 100)
            {
                for (int i = 0; i < strategybutton.Count; i++)
                {
                    this.Controls.Add(strategybutton[i]);
                    strategybutton[i].BringToFront();
                }

                Simple_Tower newtower = new Simple_Tower();
                currenttower = newtower;
                Image currentImage = ((Button)sender).BackgroundImage;
                PictureBox tower = new PictureBox();
                tower.Image = currentImage;
                tower.Size = new Size(64, 64);
                currentpicture = tower;
            }
        }

        public void magictowerClick(object sender, System.EventArgs e)
        {
            if (game.Landscape.Castle.Gold >= 200)
            {
                isClick1 = true;
                for (int i = 0; i < strategybutton.Count; i++)
                {
                    this.Controls.Add(strategybutton[i]);
                    strategybutton[i].BringToFront();
                }
                Effect.Effect effect = new Effect.Effect(5, 5, 0);
                Magic_Tower newtower = new Magic_Tower(effect);
                currenttower = newtower;
                Image currentImage = ((Button)sender).BackgroundImage;
                PictureBox tower = new PictureBox();
                tower.Image = currentImage;
                tower.Size = new Size(64, 64);
                currentpicture = tower;
            }
        }

        public void trapClick(object sender, System.EventArgs e)
        {
            if (game.Landscape.Castle.Gold >= 50)
            {

                for (int i = 0; i < effectbutton.Count; i++)
                {
                    this.Controls.Add(effectbutton[i]);
                    effectbutton[i].BringToFront();
                }
                Effect.Effect effect = new Effect.Effect(5, 5, 0);
                Magic_Trap newtower = new Magic_Trap(effect);
                currenttower = newtower;
                Image currentImage = ((Button)sender).BackgroundImage;
                PictureBox tower = new PictureBox();
                tower.Image = currentImage;
                tower.Size = new Size(64, 64);
                currentpicture = tower;
            }
        }

        public void clearClick()
        {

        }

        public void cellClick(object sender, System.EventArgs e)
        {
            if (isClick == true)
            {
                currentpicture.Location = ((Button)sender).Location;
                currentpicture.BackgroundImage = ((Button)sender).BackgroundImage;
                this.Controls.Add(currentpicture);
                currentpicture.BringToFront();
                currentpicture = null;
                currenttower.X = ((Button)sender).Location.X / 64;
                currenttower.Y = ((Button)sender).Location.Y / 64;
                game.Landscape.AddTower(currenttower);
                currenttower = null;
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Button tmp = buttons[i * game.Landscape.Width + j];
                        tmp.FlatStyle = FlatStyle.Flat;
                    }
                }
                castleinf[0].Text = "Gold: " + (game.Landscape.Castle.Gold).ToString();
            }

            isClick = false;
        }

        public void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                this.Close();
        }

        public void Init()
        {
            CreateMenue();
            CreateMap();
            CreateBuildings();
            InitStrategy();
            InitEffect();
            game = new Game.Game(level);
            CreateHeader();
            TimerLair1.Tick += new EventHandler(timerTick);
            TimerLair2.Tick += new EventHandler(timerTick);
            TimerLair3.Tick += new EventHandler(timerTick);
            make1.Interval = 5000;
            make1.Tick += new EventHandler(MakeMove);
            TimerLair1.Start();
            TimerLair2.Start();
            TimerLair3.Start();
            make1.Start();
            moving.Interval = make1.Interval / 64;
            //   moving.Tick += new EventHandler(MoveTick);
            moving.Start();
            //    CreateTimer()
        }

        public List<Enemy.Enemy> InitEnemies(int Y, int X)
        {
            var enemies1 = new List<Enemy.Enemy>();
            var simpleenemy1 = new Enemy.Enemy("Villain", 100, 70, 11, Y, X);
            var attackenemy1 = new Enemy.Enemy("Villain", 100, 70, 11, Y, X);
            //  var doubleenemy1 = new Enemy.EnemyDouble(15, "Villain", 100, 70, 7, Y, X);
            enemies1.Add(simpleenemy1);
            enemies1.Add(attackenemy1);
            //  enemies1.Add(doubleenemy1);
            return enemies1;
        }

        public void CreateInformation()
        {
            Label text11 = new Label();
            text11.Text = "Coast: 100";
            text11.BackColor = Color.White;
            text11.Location = new Point(6 * 64 + 32, 1 * 64);
            this.Controls.Add(text11);
            Label text12 = new Label();
            text12.Text = "Radius: 3";
            text12.BackColor = Color.White;
            text12.Location = new Point(6 * 64 + 32, 1 * 64 + 20);
            this.Controls.Add(text12);
            Label text21 = new Label();
            text21.Text = "Coast: 200";
            text21.BackColor = Color.White;
            text21.Location = new Point(6 * 64 + 32, 2 * 64);
            this.Controls.Add(text21);
            Label text22 = new Label();
            text22.Text = "Radius: 3";
            text22.BackColor = Color.White;
            text22.Location = new Point(6 * 64 + 32, 2 * 64 + 20);
            this.Controls.Add(text22);
            Label text31 = new Label();
            text31.Text = "Coast: 50";
            text31.BackColor = Color.White;
            text31.Location = new Point(6 * 64 + 32, 3 * 64);
            this.Controls.Add(text31);
            Label text32 = new Label();
            text32.Text = "Radius: 3";
            text32.BackColor = Color.White;
            text32.Location = new Point(6 * 64 + 32, 3 * 64 + 20);
            this.Controls.Add(text32);
        }

        public void CreateMenue()
        {
            var simpletowerSprite = new Bitmap("C:\\Users\\user\\Documents\\_SimpleTower.png");
            var magictowerSprite = new Bitmap("C:\\Users\\user\\Documents\\_MagicTower.png");
            var trapSprite = new Bitmap("C:\\Users\\user\\Documents\\_Crystal.png");
            Image partSimpletower = new Bitmap(64, 64);
            Image partMagictower = new Bitmap(64, 64);
            Image partTrap = new Bitmap(64, 64);
            Graphics gst = Graphics.FromImage(partSimpletower);
            Graphics gmt = Graphics.FromImage(partMagictower);
            Graphics gt = Graphics.FromImage(partTrap);
            gst.DrawImage(simpletowerSprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            gmt.DrawImage(magictowerSprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            gt.DrawImage(trapSprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            Button butt1 = new Button();
            Button butt2 = new Button();
            Button butt3 = new Button();
            butt1.Size = new Size(64, 64);
            butt2.Size = new Size(64, 64);
            butt3.Size = new Size(64, 64);
            butt1.FlatAppearance.BorderSize = 0;
            butt2.FlatAppearance.BorderSize = 0;
            butt3.FlatAppearance.BorderSize = 0;
            // text1.ReadOnly = true;
            butt1.Location = new Point(5 * 64 + 32, 1 * 64);
            butt2.Location = new Point(5 * 64 + 32, 2 * 64);
            butt3.Location = new Point(5 * 64 + 32, 3 * 64);
            butt1.BackgroundImage = partSimpletower;
            butt2.BackgroundImage = partMagictower;
            butt3.BackgroundImage = partTrap;
            butt1.Click += new EventHandler(simpletowerClick);
            butt2.Click += new EventHandler(magictowerClick);
            butt3.Click += new EventHandler(trapClick);
            this.Controls.Add(butt1);
            this.Controls.Add(butt2);
            this.Controls.Add(butt3);
            butt1.BringToFront();
            butt2.BringToFront();
            butt3.BringToFront();
            CreateInformation();
            //  castle.BringToFront();
        }

        public void CreateBuildings()
        {
            var castleSprite = new Bitmap("C:\\Users\\user\\Documents\\_Castle.png");
            var lairSprite = new Bitmap("C:\\Users\\user\\Documents\\_Lair.png");
            Image partCastle = new Bitmap(64, 64);
            Image partLair = new Bitmap(64, 64);
            Graphics g = Graphics.FromImage(partCastle);
            Graphics gl = Graphics.FromImage(partLair);
            g.DrawImage(castleSprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            gl.DrawImage(lairSprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            PictureBox castle = new PictureBox();
            PictureBox lair1 = new PictureBox();
            PictureBox lair2 = new PictureBox();
            PictureBox lair3 = new PictureBox();
            castle.Image = partCastle;
            lair1.Image = partLair;
            lair2.Image = partLair;
            lair3.Image = partLair;
            castle.Size = new Size(64, 64);
            lair1.Size = new Size(64, 64);
            lair2.Size = new Size(64, 64);
            lair3.Size = new Size(64, 64);
            castle.Location = new Point(3 * 64, 2 * 64);
            var en = InitEnemies(0, 4);
            Lair _lair1 = new Lair(3000, 0, 4, en);
            //  Lair _lair2 = new Lair(7000, 2, 1);
            Lair _lair2 = new Lair(7000, 2, 1, InitEnemies(2, 1));
            Lair _lair3 = new Lair(3000, 4, 2, InitEnemies(4, 2));
            //  Lair _lair3 = new Lair(3000, 4, 2);
            lair1.Location = new Point(_lair1.X * 64, _lair1.Y * 64);

            lair2.Location = new Point(1 * 64, 2 * 64);
            lair3.Location = new Point(2 * 64, 4 * 64);
            castle.BackgroundImage = buttons[2 * level.Width + 3].BackgroundImage;
            lair1.BackgroundImage = buttons[0 * level.Width + 4].BackgroundImage;
            lair2.BackgroundImage = buttons[2 * level.Width + 1].BackgroundImage;
            lair3.BackgroundImage = buttons[4 * level.Width + 2].BackgroundImage;
            Castle _castle = new Castle("White house", 500, 1000, 2, 3);
            TimerLair1.Interval = _lair1.TimeExit;
            TimerLair2.Interval = _lair2.TimeExit;
            TimerLair3.Interval = _lair3.TimeExit;
            //TimerLair1.Tick += new EventHandler(timerTick);
            //  CreateEnemy(_enemy);
            // var _enemy = level.Lairs[0].release_enemy();
            // CreateTimer(75);
            // CreateTimer(34);
            var lairs = new List<Lair>(new Lair[3] { _lair1, _lair2, _lair3 });

            level.Lairs = lairs;
            level.Castle = _castle;
            this.Controls.Add(castle);
            this.Controls.Add(lair1);
            this.Controls.Add(lair2);
            this.Controls.Add(lair3);
            lair1.BringToFront();
            lair2.BringToFront();
            lair3.BringToFront();
            castle.BringToFront();
            healthcastle.Size = new Size(200, 20);
            healthcastle.Maximum = _castle.EnduranceMax;
            healthcastle.Value = _castle.Endurance;
            healthcastle.Location = new Point(544, 60);
            this.Controls.Add(healthcastle);
        }

        public void CreateMap()
        {
            int[,] map = new int[5, 5]
            {
            {0, 1, 1, 2, 2},
            {1, 2, 2, 2, 1},
            {1, 2, 0, 2, 2},
            {0, 0, 2, 2, 2},
            {1, 0, 2, 0, 0},
            };
            var matrix = new Matrix.Matrix<Cell>(5, 5);
            var forestSprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_44.png");
            var fieldSprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_58.png");
            var road1Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_19.png");
            var road2Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_17.png");
            var road3Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_04.png");
            var road4Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_20.png");
            var road5Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_34.png");
            var road6Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_21.png");
            var road7Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_18.png");
            var road8Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_32.png");
            var road9Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_07.png");
            var road10Sprite = new Bitmap("C:\\Users\\user\\Documents\\Tile\\medievalTile_33.png");
            Image partForest = new Bitmap(64, 64);
            Image partField = new Bitmap(64, 64);
            Image partRoad1 = new Bitmap(64, 64);
            Image partRoad2 = new Bitmap(64, 64);
            Image partRoad3 = new Bitmap(64, 64);
            Image partRoad4 = new Bitmap(64, 64);
            Image partRoad5 = new Bitmap(64, 64);
            Image partRoad6 = new Bitmap(64, 64);
            Image partRoad7 = new Bitmap(64, 64);
            Image partRoad8 = new Bitmap(64, 64);
            Image partRoad9 = new Bitmap(64, 64);
            Image partRoad10 = new Bitmap(64, 64);
            Graphics g1 = Graphics.FromImage(partForest);
            Graphics g2 = Graphics.FromImage(partField);
            Graphics g3 = Graphics.FromImage(partRoad1);
            Graphics g4 = Graphics.FromImage(partRoad2);
            Graphics g5 = Graphics.FromImage(partRoad3);
            Graphics g6 = Graphics.FromImage(partRoad4);
            Graphics g7 = Graphics.FromImage(partRoad5);
            Graphics g8 = Graphics.FromImage(partRoad6);
            Graphics g9 = Graphics.FromImage(partRoad7);
            Graphics g10 = Graphics.FromImage(partRoad8);
            Graphics g11 = Graphics.FromImage(partRoad9);
            Graphics g12 = Graphics.FromImage(partRoad10);
            g1.DrawImage(forestSprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g2.DrawImage(fieldSprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g3.DrawImage(road1Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g4.DrawImage(road2Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g5.DrawImage(road3Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g6.DrawImage(road4Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g7.DrawImage(road5Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g8.DrawImage(road6Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g9.DrawImage(road7Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g10.DrawImage(road8Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g11.DrawImage(road9Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            g12.DrawImage(road10Sprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button butt = new Button();
                    butt.Size = new Size(64, 64);
                    butt.Location = new Point(j * 64, i * 64);
                    butt.FlatAppearance.BorderSize = 0;
                    butt.FlatStyle = FlatStyle.Flat;
                    if (map[i, j] == 0)
                    {
                        butt.Click += new EventHandler(cellClick);
                        butt.BackgroundImage = partField;
                    }
                    else if (map[i, j] == 1)
                    {
                        butt.BackgroundImage = partForest;
                    }
                    else
                    {
                        butt.Click += new EventHandler(cellClick);
                        if (i == 0 && j == 4) butt.BackgroundImage = partRoad1;
                        if (i == 0 && j == 3 || i == 1 && j == 1 || i == 3 && j == 2) butt.BackgroundImage = partRoad2;
                        if (i == 1 && j == 2) butt.BackgroundImage = partRoad3;
                        if (i == 1 && j == 3) butt.BackgroundImage = partRoad4;
                        if (i == 2 && j == 1 || i == 4 && j == 2) butt.BackgroundImage = partRoad5;
                        if (i == 2 && j == 3) butt.BackgroundImage = partRoad6;
                        if (i == 2 && j == 4) butt.BackgroundImage = partRoad7;
                        if (i == 3 && j == 4) butt.BackgroundImage = partRoad8;
                        if (i == 3 && j == 3) butt.BackgroundImage = partRoad9;
                    }
                    //images[i, j] = butt.BackgroundImage;
                    this.Controls.Add(butt);
                    matrix[i, j] = new Cell((TypeCell)map[i, j]);
                    buttons.Add(butt);
                }
            }
            level.Place = matrix;
        }

        public void CreateEnemy(Enemy.Enemy enemy)
        {
            Bitmap enemySprite;
            if (enemy is EnemyAttack) enemySprite = new Bitmap("C:\\Users\\user\\Documents\\Unit\\medievalUnit_15.png");
            else if (enemy is EnemyDouble) enemySprite = new Bitmap("C:\\Users\\user\\Documents\\Unit\\medievalUnit_13.png");
            else enemySprite = new Bitmap("C:\\Users\\user\\Documents\\Unit\\medievalUnit_12.png");
            Image partEnemy = new Bitmap(64, 64);
            Graphics g = Graphics.FromImage(partEnemy);
            g.DrawImage(enemySprite, new Rectangle(0, 0, 64, 64), 0, 0, 64, 64, GraphicsUnit.Pixel);
            PictureBox enemy_ = new PictureBox();
            enemy_.Size = new Size(64, 64);
            enemy_.Image = partEnemy;
            enemy_.BackgroundImageLayout = ImageLayout.Stretch;
            enemy_.BackgroundImage = buttons[enemy.Coordinates.Item1 * level.Width + enemy.Coordinates.Item2].BackgroundImage;
            enemy_.Location = new Point(enemy.Coordinates.Item2 * 64, enemy.Coordinates.Item1 * 64);
            pictureenemy.Add(enemy_);
            this.Controls.Add(enemy_);
            enemy_.BringToFront();
        }

        public void CreateHeader()
        {
            Label text11 = new Label();
            text11.Font = new Font("Sylfaen", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            int gold = game.Landscape.Castle.Gold;
            String gold_ = gold.ToString();
            text11.Size = new Size(50, 26);
            text11.AutoSize = true;
            text11.Text = "Gold: " + gold_;
            text11.BackColor = Color.White;
            text11.Location = new Point(544, 20);
            this.Controls.Add(text11);
            castleinf.Add(text11);
        }

        //public void CreateTimer(int ti)
        //{
        //    System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        //    timer1.Interval = 2;
        //    timer1.Tick += new EventHandler(timerTick);
        //    this.Controls.Add(timer1);
        //    timer.Add(timer1);
        //}

        public void MakeMove(object? sender, EventArgs e)
        {
            //int x_ = game.Landscape.GiveNextCoordinates(game.Landscape.Enemies[0]).Item2;
            //int y_ = game.Landscape.GiveNextCoordinates(game.Landscape.Enemies[0]).Item1;
            //game.Landscape.Enemies[0].x = x_;
            //game.Landscape.Enemies[0].y = y_;

            game.MakeMove();
            if (game.Landscape.Castle.Endurance <= 0)
            {
                ShowLosingNotification();
            }

            else
            {
                int final = 0;
                foreach (Lair lair in game.Landscape.Lairs)
                {
                    if (lair.enemies.Count != 0) final = 1;
                }
                if (game.Landscape.Enemies.Count != 0) final = 1;
                if (final == 0) ShowWinningNotification();
                healthcastle.Value = game.Landscape.Castle.Endurance;
                for (int i = 0; i < game.Landscape.Enemies.Count; i++)
                {
                    label1.Text = game.Landscape.Enemies[i].x.ToString();
                    label2.Text = game.Landscape.Enemies[i].y.ToString();
                    if (game.Landscape.Enemies[i].Health <= 0)
                    {
                        game.Landscape.Enemies.RemoveAt(i);
                        this.Controls.Remove(pictureenemy[i]);
                        pictureenemy.RemoveAt(i);
                    }
                    else
                    {
                        pictureenemy[i].Location = new Point(game.Landscape.Enemies[i].x * 64, game.Landscape.Enemies[i].y * 64);
                        pictureenemy[i].BackgroundImage = buttons[game.Landscape.Enemies[i].y * game.Landscape.Width
                                + game.Landscape.Enemies[i].x].BackgroundImage;
                    }
                }
            }

        }

        private void ShowLosingNotification()
        {
            DialogResult result = MessageBox.Show("Вы проиграли! Хотите выйти из программы?", "Проигрыш", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            make1.Stop();
            // this.Controls.Add(notifyIcon);
        }

        private void ShowWinningNotification()
        {
            DialogResult result = MessageBox.Show("Поздравляем, вы выиграли! Хотите выйти из программы?", "Победа!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            make1.Stop();
        }

        //private void MoveTick(object? sender, EventArgs e)
        //{
        //    for (int i = 0; i < game.Landscape.Enemies.Count; i++)
        //    {
        //        int x_ = pictureenemy[i].Location.X;
        //        int y_ = pictureenemy[i].Location.Y;
        //        if (pictureenemy[i].Location.X > game.Landscape.Enemies[i].x * 64) x_ -= 1;
        //        if (pictureenemy[i].Location.X < game.Landscape.Enemies[i].x * 64) x_ += 1;
        //        if (pictureenemy[i].Location.Y < game.Landscape.Enemies[i].y * 64) y_ += 1;
        //        if (pictureenemy[i].Location.Y > game.Landscape.Enemies[i].y * 64) y_ -= 1;
        //        pictureenemy[i].Location = new Point(x_, y_);

        //        //pictureenemy[i].BackgroundImage = buttons[game.Landscape.Enemies[i].y * game.Landscape.Width
        //        //    + game.Landscape.Enemies[i].x].BackgroundImage;


        //    }
        //}

        private void timerTick(object? sender, EventArgs e)
        {

            try
            {
                Enemy.Enemy _enemy;
                if (TimerLair1 == sender) _enemy = level.Lairs[0].release_enemy();
                else if (TimerLair2 == sender) _enemy = level.Lairs[1].release_enemy();
                else _enemy = level.Lairs[2].release_enemy();
                game.Landscape.Enemies.Add(_enemy);
                CreateEnemy(_enemy);

            }
            catch { }


        }



        public void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }
    }
}