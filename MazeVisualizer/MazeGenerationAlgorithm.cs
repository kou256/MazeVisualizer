using System;
using System.Collections.Generic;

namespace MazeVisualizer
{
    class MazeGenerationAlgorithm
    {
        public Id AlgorithmId { get; set; }
        public string AlgorithmName { get; set; }

        public MazeGenerationAlgorithm()
        {
        }

        // 棒倒し法
        public void StickDownMethod(Maze maze)
        {
            Random rand = new Random();

            maze.InitializeMaze(true, true);

            foreach (Maze.Pillar coord in maze.PillarCoordinate){
                maze.IsWall[coord.Y, coord.X] = true;

                List<Direction> wall_candidates_direction = new List<Direction>();
                if (coord.Y == 2)
                {
                    wall_candidates_direction.Add(Direction.UP);
                }
                else if (!maze.IsWall[coord.Y + 1, coord.X])
                {
                    wall_candidates_direction.Add(Direction.DOWN);
                }
                else if (!maze.IsWall[coord.Y, coord.X - 1])
                {
                    wall_candidates_direction.Add(Direction.LEFT);
                }
                else if (!maze.IsWall[coord.Y, coord.X + 1])
                {
                    wall_candidates_direction.Add(Direction.RIGHT);
                }

                int x_moving_distance = 0, y_moving_distance = 0;
                int wall_direction = rand.Next(wall_candidates_direction.Count);
                switch (wall_direction)
                {
                    case 0:
                        y_moving_distance = -1;
                        break;
                    case 1:
                        y_moving_distance = 1;
                        break;
                    case 2:
                        x_moving_distance = -1;
                        break;
                    case 3:
                        x_moving_distance = 1;
                        break;
                }

                maze.IsWall[coord.Y + y_moving_distance, coord.X + x_moving_distance] = true;
            }
        }

        // 壁伸ばし法
        public void WallExtendMethod(Maze maze)
        {
            Random rand = new Random();
            Stack<Maze.Pillar> prev_pillar_coord = new Stack<Maze.Pillar>();

            while (maze.PillarCoordinate.Count != 0)
            {
                Maze.Pillar coord = maze.PillarCoordinate[rand.Next(maze.PillarCoordinate.Count)];
                int x = coord.X, y = coord.Y;
                maze.PillarCoordinate.Remove(new Maze.Pillar { X = x, Y = y });

                if (!maze.IsWall[y, x])
                {
                    maze.IsWall[y, x] = true;

                    while (true)
                    {
                        List<Direction> wall_candidates_direction = new List<Direction>();
                        if (!prev_pillar_coord.Contains(new Maze.Pillar { X = x, Y = y - 2 }))
                        {
                            wall_candidates_direction.Add(Direction.UP);
                        }
                        if (!prev_pillar_coord.Contains(new Maze.Pillar { X = x, Y = y + 2 }))
                        {
                            wall_candidates_direction.Add(Direction.DOWN);
                        }
                        if (!prev_pillar_coord.Contains(new Maze.Pillar { X = x - 2, Y = y }))
                        {
                            wall_candidates_direction.Add(Direction.LEFT);
                        }
                        if (!prev_pillar_coord.Contains(new Maze.Pillar { X = x + 2, Y = y }))
                        {
                            wall_candidates_direction.Add(Direction.RIGHT);
                        }

                        if (wall_candidates_direction.Count == 0)
                        {
                            Maze.Pillar next_coord = prev_pillar_coord.Pop();
                            x = next_coord.X;
                            y = next_coord.Y;
                        }
                        else
                        {
                            int x_moving_distance = 0, y_moving_distance = 0;
                            int wall_direction = rand.Next(wall_candidates_direction.Count);

                            switch (wall_direction)
                            {
                                case 0:
                                    y_moving_distance = -1;
                                    break;
                                case 1:
                                    y_moving_distance = 1;
                                    break;
                                case 2:
                                    x_moving_distance = -1;
                                    break;
                                case 3:
                                    x_moving_distance = 1;
                                    break;
                            }

                            if (maze.IsWall[y + 2 * y_moving_distance, x + 2 * x_moving_distance])
                            {
                                maze.IsWall[y + y_moving_distance, x + x_moving_distance] = true;
                                prev_pillar_coord.Clear();

                                break;
                            }
                            else
                            {
                                for (int i = 1; i <= 2; i++)
                                {
                                    maze.IsWall[y + i * y_moving_distance, x + i * x_moving_distance] = true;
                                }
                                prev_pillar_coord.Push(new Maze.Pillar { X = x, Y = y });
                                x += 2 * x_moving_distance;
                                y += 2 * y_moving_distance;
                                maze.PillarCoordinate.Remove(new Maze.Pillar { X = x, Y = y });
                            }
                        }
                    }
                }
            }
        }

        public void DiggingMethod()
        {
        }

        private enum Direction
        {
            UP = 0,
            DOWN = 1,
            LEFT = 2,
            RIGHT = 3,
        }

        public enum Id
        {
            SDM = 0,
            WEM = 0,
            DM = 0,
        }
    }
}
