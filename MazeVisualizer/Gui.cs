using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;

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
            else if (algorthm == 1)
            {
                Stack<Maze.Pillar> prev_pillar = new Stack<Maze.Pillar>();

                while (m.pillar_coordinate.Count != 0)
                {
                    int index = rand.Next(m.pillar_coordinate.Count);
                    Maze.Pillar pillar = m.pillar_coordinate[index];
                    int x = pillar.X, y = pillar.Y;
                    m.pillar_coordinate.RemoveAt(index);

                    if (!m.Is_Wall[y, x])
                    {
                        grid[y, x].Fill = Brushes.Black;

                        m.Is_Wall[y, x] = true;
                        m.Is_Discoverd[y, x] = true;


                        while (true)
                        {
                            int r = rand.Next(0, 4);
                            int x_base_distance = 0, y_base_distance = 0;

                            switch (r)
                            {
                                case 0:
                                    y_base_distance = -1;
                                    break;
                                case 1:
                                    y_base_distance = 1;
                                    break;
                                case 2:
                                    x_base_distance = -1;
                                    break;
                                case 3:
                                    x_base_distance = 1;
                                    break;
                            }



                            if (!m.Is_Wall[y + 2 * y_base_distance, x + 2 * x_base_distance])
                            {
                                for (int i = 1; i <= 2; i++)
                                {
                                    m.Is_Wall[y + i * y_base_distance, x + i * x_base_distance] = true;
                                    m.Is_Discoverd[y + i * y_base_distance, x + i * x_base_distance] = true;
                                    grid[y + i * y_base_distance, x + i * x_base_distance].Fill = Brushes.Black;
                                }
                                prev_pillar.Push(new Maze.Pillar(x, y));
                                x += 2 * x_base_distance;
                                y += 2 * y_base_distance;
                            }
                            else
                            {
                                if (!prev_pillar.Contains(new Maze.Pillar(x + 2 * x_base_distance, y + 2 * y_base_distance)))
                                {
                                    m.Is_Wall[y + y_base_distance, x + x_base_distance] = true;
                                    m.Is_Discoverd[y + y_base_distance, x + x_base_distance] = true;
                                    grid[y + y_base_distance, x + x_base_distance].Fill = Brushes.Black;
                                    break;
                                }
                                else
                                {
                                    if (prev_pillar.Contains(new Maze.Pillar(x, y - 2)) &&
                                        prev_pillar.Contains(new Maze.Pillar(x, y + 2)) &&
                                        prev_pillar.Contains(new Maze.Pillar(x - 2, y)) &&
                                        prev_pillar.Contains(new Maze.Pillar(x + 2, y)))
                                    {
                                        Maze.Pillar coordinate = prev_pillar.Pop();
                                        x = coordinate.X;
                                        y = coordinate.Y;
                                    }
                                }
                            }
                        }

                        prev_pillar.Clear();
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
