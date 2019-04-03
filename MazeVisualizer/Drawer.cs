using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeVisualizer
{
    class Drawer
    {
        /* 迷路のマス目を描画する */
        public static void drawMaze(Canvas target_canvas, Maze target_maze)
        {
            Rectangle[,] grid = new Rectangle[target_maze.GridRow, target_maze.GridColumn];

            for (int y = 0; y < target_maze.GridRow; y++)
            {
                for (int x = 0; x < target_maze.GridColumn; x++)
                {
                    grid[y, x] = new Rectangle();

                    if (y % 2 == 0 && x % 2 == 0)
                    {
                        grid[y, x].Height = 1;
                        grid[y, x].Width = 1;
                        Canvas.SetTop(grid[y, x], y / 2 * (target_maze.GridHeight + 1));
                        Canvas.SetLeft(grid[y, x], x / 2 * (target_maze.GridWidth + 1));
                        grid[y, x].Stroke = Brushes.Black;
                        grid[y, x].Fill = Brushes.Black;
                    }
                    else if (y % 2 == 0 && x % 2 == 1)
                    {
                        grid[y, x].Height = 1;
                        grid[y, x].Width = target_maze.GridWidth;
                        Canvas.SetTop(grid[y, x], y / 2 * (target_maze.GridHeight + 1));
                        Canvas.SetLeft(grid[y, x], x / 2 * (target_maze.GridWidth + 1) + 1);
                        if (target_maze.IsWall[y, x])
                        {
                            grid[y, x].Stroke = Brushes.Black;
                            grid[y, x].Fill = Brushes.Black;
                        }
                        else
                        {
                            grid[y, x].Stroke = Brushes.LightGray;
                            grid[y, x].Fill = Brushes.LightGray;
                        }
                    }
                    else if (y % 2 == 1 && x % 2 == 0)
                    {
                        grid[y, x].Height = target_maze.GridHeight;
                        grid[y, x].Width = 1;
                        Canvas.SetTop(grid[y, x], y / 2 * (target_maze.GridHeight + 1) + 1);
                        Canvas.SetLeft(grid[y, x], x / 2 * (target_maze.GridWidth + 1));
                        if (target_maze.IsWall[y, x])
                        {
                            grid[y, x].Stroke = Brushes.Black;
                            grid[y, x].Fill = Brushes.Black;
                        }
                        else
                        {
                            grid[y, x].Stroke = Brushes.LightGray;
                            grid[y, x].Fill = Brushes.LightGray;
                        }
                    }
                    else
                    {
                        grid[y, x].Height = target_maze.GridHeight;
                        grid[y, x].Width = target_maze.GridWidth;
                        Canvas.SetTop(grid[y, x], y / 2 * (target_maze.GridHeight + 1) + 1);
                        Canvas.SetLeft(grid[y, x], x / 2 * (target_maze.GridWidth + 1) + 1);
                        grid[y, x].Stroke = Brushes.White;
                        grid[y, x].Fill = Brushes.White;
                    } 

                    target_canvas.Children.Add(grid[y, x]);
                }
            }
        }

        /* 迷路のマス目を削除する */
        public static void eraseMaze(Canvas target_canvas)
        {
            target_canvas.Children.Clear();
        }
    }
}
