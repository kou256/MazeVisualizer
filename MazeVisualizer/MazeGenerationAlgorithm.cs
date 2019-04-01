namespace Maze.Visualizer
{
    class MazeGenerationAlgorithm
    {
        public Id AlgorithmId { get; set; }
        public string AlgorithmName { get; set; }

        public MazeGenerationAlgorithm()
        {
        }

        public enum Id
        {
            sdm = 0,
            wem = 0,
            dm  = 0,
        }
    }
}
