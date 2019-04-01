using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static MazeVisualizer.MazeGenerationAlgorithm;

namespace Maze.Visualizer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Gui g = new Gui();
        ObservableCollection<MazeGenerationAlgorithm> mga = new ObservableCollection<MazeGenerationAlgorithm>()
        {
            new MazeGenerationAlgorithm{AlgorithmId = Id.sdm, AlgorithmName = "棒倒し法"},
            new MazeGenerationAlgorithm{AlgorithmId = Id.wem, AlgorithmName = "壁伸ばし法"},
            new MazeGenerationAlgorithm{AlgorithmId = Id.dm,  AlgorithmName = "穴掘り法"}
        };

        /* コンストラクタ */
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mga;
        }

        /* Generateボタンが押されたとき */
        private void generateMaze(object sender, RoutedEventArgs e)
        {
            g.drawMazeGrid(maze, 2 * (int)grid_count.Value + 1, 2 * (int)grid_count.Value + 1);

            var item = generation_algorithm_list.SelectedItem as MazeGenerationAlgorithm;
            g.drawMazeWall(maze, (int)item.AlgorithmId);

            if (maze.Children.Count != 0)
            {
                maze_generate.IsEnabled = false;
                maze_reset.IsEnabled = true;
                generation_algorithm_list.IsEnabled = false;
            }
        }

        /* Resetボタンが押されたとき */
        private void resetMaze(object sender, RoutedEventArgs e)
        {
            g.eraseMazeGrid(maze);

            if (maze.Children.Count == 0)
            {
                maze_generate.IsEnabled = true;
                maze_reset.IsEnabled = false;
                generation_algorithm_list.IsEnabled = true;
            }
        }

        /* MazeGenerationAlgorthmが選択されたとき */
        private void selectedAlgorithm(object sender, SelectionChangedEventArgs e)
        {
            var item = generation_algorithm_list.SelectedItem as MazeGenerationAlgorithm;
            if (item != null)
            {
                maze_generate.IsEnabled = true;
            }
        }
    }
}