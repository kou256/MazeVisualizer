namespace MazeVisualizer
{
    class Maze
    {
        /* プロパティ */
        private int grid_height { get; set; }   // 1マスの縦幅
        private int grid_width { get; set; }    // 1マスの横幅
        private int grid_row { get; set; }      // 行の数
        private int grid_column { get; set; }   // 列の数
        private byte[,] grid_info { get; set; } // 上位4bit:上下左右の壁情報、下位4bit:上下左右の探索済み情報

        /* コンストラクタ */
        public Maze(int height, int width, int row, int column)
        {
            Grid_Height = height;
            Grid_Width = width;
            Grid_Row = row;
            Grid_Column = column;
            grid_info = new byte[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Grid_Info[i, j] = 0B00000000;
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
                grid_row = 2 * value + 1;
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
                grid_column = 2 * value + 1;
            }
        }

        public byte[,] Grid_Info
        {
            get
            {
                return grid_info;
            }
            set
            {
                grid_info = value;
            }
        }
    }
}
