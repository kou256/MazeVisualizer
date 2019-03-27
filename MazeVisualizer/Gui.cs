using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeVisualizer
{
    class Gui
    {
        public Gui()
        {

        }

        /* 迷路のマス目を描画する */
        public void drawMazeGrid(Canvas target, int height, int width, int row, int column)
        {
            Rectangle[,] grid = new Rectangle[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    grid[i, j] = new Rectangle();
                    grid[i, j].Stroke = Brushes.Black;
                    grid[i, j].Fill = Brushes.White;
                    grid[i, j].Height = height;
                    grid[i, j].Width = width;

                    Canvas.SetTop(grid[i, j], i * (height - 1));
                    Canvas.SetLeft(grid[i, j], j * (width - 1));

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
