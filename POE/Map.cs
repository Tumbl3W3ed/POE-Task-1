﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE
{
    class Map
    {
        private Tile[,] map { get; set; }
        private Hero hero;
        private Enemy[] enemies;
        public int MapWidth;
        public int MapHeight;
        private Random random = new Random();

        public Tile[,] ThisMap
        {
            get => map;
            set => map = value;
        }

        public Hero Hero
        {
            get => hero;
            set => hero = value;
        }

        public Enemy[] Enemies
        {
            get => enemies;
            set => enemies = value;
        }
        public Map(int minWidth, int maxWidth, int minHeight, int maxHeight, int enemyAmount)
        {
            MapWidth = random.Next(minWidth, maxWidth);
            MapHeight = random.Next(minHeight, maxHeight);
            map = new Tile[MapHeight, MapWidth];
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    map[y, x] = null;
                }
            }
            hero = (Hero)Create(Tile.TileType.Hero);
            map[hero.Y, hero.X] = hero;
            enemies = new Enemy[enemyAmount];
            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i] = (Enemy)Create(Tile.TileType.Enemy);
                enemies[i].ThisTileType = Tile.TileType.Enemy;
                map[enemies[i].Y, enemies[i].X] = enemies[i];
            }

        }

        private Tile Create(Tile.TileType type)
        {
            switch (type)
            {
                case Tile.TileType.Hero:
                    return new Hero(random.Next(1, MapHeight - 1), random.Next(1, MapWidth - 1), 40);
                case Tile.TileType.Enemy:
                    int x, y;
                    x = random.Next(1, MapWidth - 1);
                    y = random.Next(1, MapHeight - 1);
                    while (map[x, y] != null)
                    {
                        x = random.Next(1, MapWidth - 1);
                        y = random.Next(1, MapHeight - 1);
                    }
                    return new Goblin(y, x);
            }
            return null;
        }

        public void UpdateVision()
        {
            UpdateCharacterVision(hero);
            foreach (Character c in enemies)
            {
                UpdateCharacterVision(c);
            }
        }

        public void UpdateCharacterVision(Character character)
        {
            Console.WriteLine(character.Y - 1);
            Console.WriteLine(character.X - 1);
            Console.WriteLine(character.Y + 1);
            Console.WriteLine(character.X + 1);

            character.Vision = new Tile[4]{map[character.Y-1, character.X],
                map[character.Y +1, character.X], map[character.Y, character.X - 1],
                map[character.Y, character.X+1] };
        }
    }
}
