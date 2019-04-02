using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeVisualizer
{
    class Gui
    {
        private Rectangle[,] grid;

        /* コンストラクタ */
        public Gui()
        {
        }

        /* 迷路のマス目を描画する */
        public void drawMazeGrid(Canvas target, Maze maze)
        {
            grid = new Rectangle[maze.GridRow, maze.GridColumn];

            for (int y = 0; y < maze.GridRow; y++)
            {
                for (int x = 0; x < maze.GridColumn; x++)
                {
                    grid[y, x] = new Rectangle();
                    grid[y, x].Stroke = Brushes.DarkGray;
                    if (maze.IsWall[y, x])
                    {
                        grid[y, x].Fill = Brushes.Black;
                    }
                    else
                    {
                        grid[y, x].Fill = Brushes.White;
                    }
                    grid[y, x].Height = maze.GridHeight;
                    grid[y, x].Width = maze.GridWidth;
                    Canvas.SetTop(grid[y, x], y * (maze.GridHeight - 1));
                    Canvas.SetLeft(grid[y, x], x * (maze.GridWidth - 1));
                    target.Children.Add(grid[y, x]);
                }
            }
        }

        /* 迷路のマス目を削除する */
        public void eraseMazeGrid(Canvas target)
        {
            target.Children.Clear();
        }
    }
}
