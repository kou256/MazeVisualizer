namespace MazeVisualizer
{
    class AlgorithmList
    {
        public Id AlgorithmId { get; set; }
        public string AlgorithmName { get; set; }

        //  各アルゴリズムに対応するID
        public enum Id
        {
            SDM = 0,
            WEM = 1,
            DM = 2,
        }
    }
}
