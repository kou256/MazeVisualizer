using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeVisualizer
{
    class Maze
    {
        /* プロパティ */
        private int grid_height { get; set; }   // 1マスの縦幅
        private int grid_width { get; set; }    // 1マスの横幅
        private int grid_row { get; set; }      // 行の数
        private int grid_column { get; set; }   // 列の数

        /* コンストラクタ */
        public Maze(int height, int width, int row, int column)
        {
            Grid_Height = height;
            Grid_Width = width;
            Grid_Row = row;
            Grid_Column = column;
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

        /* 迷路のマス目を描画する */
        public void drawGrid(Canvas target_canvas)
        {
            Rectangle[,] grid = new Rectangle[grid_row, grid_column];

            for (int i = 0; i < grid_row; i++)
            {
                for (int j = 0; j < grid_column; j++)
                {
                    grid[i, j] = new Rectangle();
                    grid[i, j].Stroke = Brushes.Black;
                    grid[i, j].Fill = Brushes.White;
                    grid[i, j].Height = grid_height;
                    grid[i, j].Width = grid_width;

                    Canvas.SetTop(grid[i, j], i * (grid_height - 1));
                    Canvas.SetLeft(grid[i, j], j * (grid_width - 1));

                    target_canvas.Children.Add(grid[i, j]);
                }
            }
        }

        /* 迷路のマス目を削除する */
        public void eraseGrid(Canvas target_canvas)
        {
            target_canvas.Children.Clear();
        }
    }
}
