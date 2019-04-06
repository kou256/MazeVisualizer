using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace MazeVisualizer
{
    class MazeGenerationAlgorithm
    {
        // 棒倒し法
        public static void StickDownMethod(Maze maze, Drawer drawer)
        {
            Random rand = new Random();
            List<Maze.Coordinate> pillar_coord = new List<Maze.Coordinate>();

            for (int i = 2; i < maze.GridRow - 2; i += 2)
            {
                for (int j = 2; j < maze.GridColumn - 2; j += 2)
                {
                    pillar_coord.Add(new Maze.Coordinate { X = j, Y = i });
                }
            }

            var timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                // インターバルを設定
                Interval = TimeSpan.FromSeconds(0.1),
            };
            // タイマメソッドを設定（ラムダ式で記述）
            timer.Tick += (s, e) =>
            {
                if (pillar_coord.Count == 0)
                {
                    timer.Stop();
                }
                else
                {

                Maze.Coordinate coord = pillar_coord[0];
                pillar_coord.RemoveAt(0);
                maze.IsWall[coord.Y, coord.X] = true;

                List<Direction> wall_candidates_direction = new List<Direction>();
                if (coord.Y == 2)
                {
                    wall_candidates_direction.Add(Direction.UP);
                }
                if (!maze.IsWall[coord.Y + 1, coord.X])
                {
                    wall_candidates_direction.Add(Direction.DOWN);
                }
                if (!maze.IsWall[coord.Y, coord.X - 1])
                {
                    wall_candidates_direction.Add(Direction.LEFT);
                }
                if (!maze.IsWall[coord.Y, coord.X + 1])
                {
                    wall_candidates_direction.Add(Direction.RIGHT);
                }

                int x_moving_distance = 0, y_moving_distance = 0;
                int wall_direction = rand.Next(wall_candidates_direction.Count);
                switch (wall_candidates_direction[wall_direction])
                {
                    case Direction.UP:
                        y_moving_distance = -1;
                        break;
                    case Direction.DOWN:
                        y_moving_distance = 1;
                        break;
                    case Direction.LEFT:
                        x_moving_distance = -1;
                        break;
                    case Direction.RIGHT:
                        x_moving_distance = 1;
                        break;
                }

                maze.IsWall[coord.Y + y_moving_distance, coord.X + x_moving_distance] = true;
                drawer.drawMazePartial(maze, coord.X + x_moving_distance, coord.Y + y_moving_distance);
                }
            };
            // タイマを開始
            timer.Start();
        }

        // 壁伸ばし法
        public static void WallExtendMethod(Maze maze, Drawer drawer)
        {
            Random rand = new Random();
            List<Maze.Coordinate> pillar_coord = new List<Maze.Coordinate>();
            Stack<Maze.Coordinate> prev_pillar_coord = new Stack<Maze.Coordinate>();

            for (int i = 2; i < maze.GridRow - 2; i += 2)
            {
                for (int j = 2; j < maze.GridColumn - 2; j += 2)
                {
                    pillar_coord.Add(new Maze.Coordinate { X = j, Y = i });
                }
            }


            var timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                // インターバルを設定
                Interval = TimeSpan.FromSeconds(0.25),
            };
            // タイマメソッドを設定（ラムダ式で記述）
            timer.Tick += (s, e) =>
            {
                if (pillar_coord.Count == 0)
                {
                    timer.Stop();
                }
                else
                {
                    Maze.Coordinate coord = pillar_coord[rand.Next(pillar_coord.Count)];
                    int x = coord.X, y = coord.Y;
                    pillar_coord.Remove(new Maze.Coordinate { X = x, Y = y });
                    if (!maze.IsWall[y, x])
                    {
                        maze.IsWall[y, x] = true;

                        while (true)
                        {
                            List<Direction> wall_candidates_direction = new List<Direction>();
                            if (!prev_pillar_coord.Contains(new Maze.Coordinate { X = x, Y = y - 2 }))
                            {
                                wall_candidates_direction.Add(Direction.UP);
                            }
                            if (!prev_pillar_coord.Contains(new Maze.Coordinate { X = x, Y = y + 2 }))
                            {
                                wall_candidates_direction.Add(Direction.DOWN);
                            }
                            if (!prev_pillar_coord.Contains(new Maze.Coordinate { X = x - 2, Y = y }))
                            {
                                wall_candidates_direction.Add(Direction.LEFT);
                            }
                            if (!prev_pillar_coord.Contains(new Maze.Coordinate { X = x + 2, Y = y }))
                            {
                                wall_candidates_direction.Add(Direction.RIGHT);
                            }

                            if (wall_candidates_direction.Count == 0)
                            {
                                Maze.Coordinate next_coord = prev_pillar_coord.Pop();
                                x = next_coord.X;
                                y = next_coord.Y;
                            }
                            else
                            {
                                int x_moving_distance = 0, y_moving_distance = 0;
                                int wall_direction = rand.Next(wall_candidates_direction.Count);
                                switch (wall_candidates_direction[wall_direction])
                                {
                                    case Direction.UP:
                                        y_moving_distance = -1;
                                        break;
                                    case Direction.DOWN:
                                        y_moving_distance = 1;
                                        break;
                                    case Direction.LEFT:
                                        x_moving_distance = -1;
                                        break;
                                    case Direction.RIGHT:
                                        x_moving_distance = 1;
                                        break;
                                }

                                if (maze.IsWall[y + 2 * y_moving_distance, x + 2 * x_moving_distance])
                                {
                                    maze.IsWall[y + y_moving_distance, x + x_moving_distance] = true;
                                    drawer.drawMazePartial(maze, x + x_moving_distance, y + y_moving_distance);
                                    prev_pillar_coord.Clear();

                                    break;
                                }
                                else
                                {
                                    for (int i = 1; i <= 2; i++)
                                    {
                                        maze.IsWall[y + i * y_moving_distance, x + i * x_moving_distance] = true;
                                        drawer.drawMazePartial(maze, x + i * x_moving_distance, y + i * y_moving_distance);
                                    }
                                    prev_pillar_coord.Push(new Maze.Coordinate { X = x, Y = y });
                                    x += 2 * x_moving_distance;
                                    y += 2 * y_moving_distance;
                                    pillar_coord.Remove(new Maze.Coordinate { X = x, Y = y });
                                }
                            }
                        }

                    }
                }
            };
                // タイマを開始
                timer.Start();
        }

        // 穴掘り法
        public static void DiggingMethod(Maze maze, Drawer drawer)
        {
            Random rand = new Random();
            List<Maze.Coordinate> cell_coord = new List<Maze.Coordinate>();
            List<Maze.Coordinate> astil_coord = new List<Maze.Coordinate>();

            for (int i = 1; i < maze.GridRow; i += 2)
            {
                for (int j = 1; j < maze.GridColumn; j += 2)
                {
                    cell_coord.Add(new Maze.Coordinate { X = j, Y = i });
                }
            }

            Maze.Coordinate coord = cell_coord[rand.Next(cell_coord.Count)];
            int x = coord.X, y = coord.Y;
            cell_coord.Remove(new Maze.Coordinate { X = x, Y = y });
            astil_coord.Add(new Maze.Coordinate { X = x, Y = y });

            var timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                // インターバルを設定
                Interval = TimeSpan.FromSeconds(0.1),
            };
            // タイマメソッドを設定（ラムダ式で記述）
            timer.Tick += (s, e) =>
            { 
                if (cell_coord.Count == 0)
                {
                    timer.Stop();
                }
                else
                {
                    List<Direction> wall_candidates_direction = new List<Direction>();

                    if (y - 2 > 0 && maze.IsWall[y - 2, x])
                    {
                        wall_candidates_direction.Add(Direction.UP);
                    }
                    if (y + 2 < maze.GridRow && maze.IsWall[y + 2, x])
                    {
                        wall_candidates_direction.Add(Direction.DOWN);
                    }
                    if (x - 2 > 0 && maze.IsWall[y, x - 2])
                    {
                        wall_candidates_direction.Add(Direction.LEFT);
                    }
                    if (x + 2 < maze.GridColumn && maze.IsWall[y, x + 2])
                    {
                        wall_candidates_direction.Add(Direction.RIGHT);
                    }

                    if (wall_candidates_direction.Count == 0)
                    {
                        astil_coord.Remove(new Maze.Coordinate { X = x, Y = y });
                        var next_coord = astil_coord[rand.Next(astil_coord.Count)];
                        astil_coord.Remove(next_coord);
                        x = next_coord.X;
                        y = next_coord.Y;
                    }
                    else
                    {
                        int x_moving_distance = 0, y_moving_distance = 0;
                        var wall_direction = rand.Next(wall_candidates_direction.Count);
                        switch (wall_candidates_direction[wall_direction])
                        {
                            case Direction.UP:
                                y_moving_distance = -1;
                                break;
                            case Direction.DOWN:
                                y_moving_distance = 1;
                                break;
                            case Direction.LEFT:
                                x_moving_distance = -1;
                                break;
                            case Direction.RIGHT:
                                x_moving_distance = 1;
                                break;
                        }

                        for (int k = 0; k < 3; k++)
                        {
                            maze.IsWall[y + k * y_moving_distance, x + k * x_moving_distance] = false;
                            drawer.drawMazePartial(maze, x + k * x_moving_distance, y + k * y_moving_distance);
                        }
                        astil_coord.Add(new Maze.Coordinate { X = x, Y = y });
                        x += 2 * x_moving_distance;
                        y += 2 * y_moving_distance;
                        cell_coord.Remove(new Maze.Coordinate { X = x, Y = y });
                    }
                }
            };
            // タイマを開始
            timer.Start();
        }

        // 方向の定義
        private enum Direction
        {
            UP = 0,
            DOWN = 1,
            LEFT = 2,
            RIGHT = 3,
        } 
    }
}
