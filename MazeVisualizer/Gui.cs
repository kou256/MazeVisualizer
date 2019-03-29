using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MazeVisualizer
{
    class Gui
    {
        private Maze m;
        private Rectangle[,] grid;

        /* コンストラクタ */
        public Gui()
        {
        }

        /* 迷路のマス目を描画する */
        public void drawMazeGrid(Canvas target, int grid_row, int grid_column)
        {
            m = new Maze(grid_row, grid_column);
            grid = new Rectangle[m.Grid_Row, m.Grid_Column];

            for (int i = 0; i < m.Grid_Row; i++)
            {
                for (int j = 0; j < m.Grid_Column; j++)
                {
                    grid[i, j] = new Rectangle();
                    grid[i, j].Stroke = Brushes.DarkGray;
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

        public void drawMazeWall(Canvas target, int algorthm)
        {
            Random rand = new Random();

            /* 棒倒し法 */
            if (algorthm == 0)
            {
                for (int y = 2; y < m.Grid_Row - 1; y += 2)
                {
                    for (int x = 2; x < m.Grid_Column - 1; x += 2)
                    {
                        grid[y, x].Fill = Brushes.Black;

                        m.Is_Wall[y, x] = true;
                        m.Is_Discoverd[y, x] = true;

                        while (true)
                        {
                            int r;
                            if (y == 2)
                            {
                                r = rand.Next(0, 4);
                            }
                            else
                            {
                                r = rand.Next(1, 4);
                            }

                            int wall_candidate_x = x, wall_candidate_y = y;
                            switch (r)
                            {
                                // 上
                                case 0:
                                    wall_candidate_y--;
                                    break;
                                // 下
                                case 1:
                                    wall_candidate_y++;
                                    break;
                                // 左
                                case 2:
                                    wall_candidate_x--;
                                    break;
                                // 右
                                case 3:
                                    wall_candidate_x++;
                                    break;
                            }

                            if (!m.Is_Wall[wall_candidate_y, wall_candidate_x])
                            {
                                grid[wall_candidate_y, wall_candidate_x].Fill = Brushes.Black;

                                m.Is_Wall[wall_candidate_y, wall_candidate_x] = true;

                                break;
                            }
                        }
                    }
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
