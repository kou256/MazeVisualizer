namespace MazeVisualizer
{
    class Maze
    {
        /* プロパティ */
        private int grid_height { get; set; }       // 1マスの縦幅
        private int grid_width { get; set; }        // 1マスの横幅
        private int grid_row { get; set; }          // 行の数
        private int grid_column { get; set; }       // 列の数
        private bool[,] is_wall { get; set; }       // 壁か？
        private bool[,] is_discoverd { get; set; }  // 探索済みか？

        /* コンストラクタ */
        public Maze(int row, int column)
        {
            grid_height = 16;
            grid_width = 16;
            grid_row = row;
            grid_column = column;
            is_wall = new bool[grid_row, grid_column];
            is_discoverd = new bool[grid_row, grid_column];

            for (int i = 0; i < grid_row; i++)
            {
                for (int j = 0; j < grid_column; j++)
                {
                    if (i == 0 || i == row - 1 || j == 0 || j == column - 1)
                    {
                        is_wall[i, j] = true;
                        is_discoverd[i, j] = true;
                    }
                    else
                    {
                        is_wall[i, j] = false;
                        is_discoverd[i, j] = false;
                    }
                }
            }
        }

        /* プロパティのアクセサ */
        public int Grid_Height
        {
            get
            {
                return grid_height;
            }
            set
            {
                grid_height = value;
            }
        }

        public int Grid_Width
        {
            get
            {
                return grid_width;
            }
            set
            {
                grid_width = value;
            }
        }

        public int Grid_Row
        {
            get
            {
                return grid_row;
            }
            set
            {
                grid_row = value;
            }
        }

        public int Grid_Column
        {
            get
            {
                return grid_column;
            }
            set
            {
                grid_column = value;
            }
        }

        public bool[,] Is_Wall
        {
            get
            {
                return is_wall;
            }
            set
            {
                is_wall = value;
            }
        }

        public bool[,] Is_Discoverd
        {
            get
            {
                return is_discoverd;
            }
            set
            {
                is_discoverd = value;
            }
        }
    }
}
