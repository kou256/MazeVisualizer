namespace MazeVisualizer
{
    class MazeGenerationAlgorithm
    {
        public int id { get; set; }
        public string method { get; set; }

        public MazeGenerationAlgorithm()
        {
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public string Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }
    }
}
