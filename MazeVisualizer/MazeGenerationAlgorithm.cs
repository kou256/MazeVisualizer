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
