using System.Collections.Generic;

namespace MazeVisualizer
{
    class Maze
    {
        /* プロパティ */
        public int GridHeight { get; set; } = 16;
        public int GridWidth { get; set; } = 16;
        public int GridRow { get; set; }
        public int GridColumn { get; set; }
        public bool[,] IsWall { get; set; }
        public bool[,] IsDiscoverd { get; set; }
        public List<Pillar> PillarCoordinate;

        /* コンストラクタ */
        public Maze()
        {
            IsWall = new bool[GridRow, GridColumn];
            IsDiscoverd = new bool[GridRow, GridColumn];
            PillarCoordinate = new List<Pillar>();
        }

        public void InitializeMaze(bool aisle_exists, bool pillar_exists)
        {
            for (int y = 0; y < GridRow; y++)
            {
                for (int x = 0; x < GridColumn; x++)
                {
                    if (y == 0 || y == GridRow - 1 ||
                        x == 0 || x == GridColumn - 1)
                    {
                        IsWall[y, x] = true;
                    } 
                    else
                    {
                        if (aisle_exists)
                        {
                            IsWall[y, x] = false;
                        }
                        else
                        {
                            IsWall[y, x] = true;
                        }
                    }

                    if (pillar_exists && y % 2 == 0 && x % 2 == 0)
                    {
                        PillarCoordinate.Add(new Pillar { X = x, Y = y });
                    }
                }
            }
        }

        public struct Pillar
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
