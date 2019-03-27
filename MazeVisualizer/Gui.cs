using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeVisualizer
{
    class Gui
    {
        Maze m;

        public Gui()
        {
        }

        /* 迷路のマス目を描画する */
        public void drawMazeGrid(Canvas target, int grid_row, int grid_column)
        {
            m = new Maze(grid_row, grid_column);
            Rectangle[,] grid = new Rectangle[m.Grid_Row, m.Grid_Column];

            for (int i = 0; i < m.Grid_Row; i++)
            {
                for (int j = 0; j < m.Grid_Column; j++)
                {
                    grid[i, j] = new Rectangle();
                    grid[i, j].Stroke = Brushes.Black;
                    if (m.Is_Wall[i, j])
                    {
                        grid[i, j].Fill = Brushes.Black;
                    }
                    else
                    {
                        grid[i, j].Fill = Brushes.White;
                    }
                    grid[i, j].Height = m.Grid_Height;
                    grid[i, j].Width = m.Grid_Width;

                    Canvas.SetTop(grid[i, j], i * (m.Grid_Height - 1));
                    Canvas.SetLeft(grid[i, j], j * (m.Grid_Width - 1));

                    target.Children.Add(grid[i, j]);
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
