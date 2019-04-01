using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace MazeVisualizer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Gui g = new Gui();
        ObservableCollection<MazeGenerationAlgorithm> mga = new ObservableCollection<MazeGenerationAlgorithm>()
        {
            new MazeGenerationAlgorithm{id = 0, method = "Stick Down Method"},
            new MazeGenerationAlgorithm{id = 1, method = "Wall Exntend Method"},
            new MazeGenerationAlgorithm{id = 2, method = "Digging Method"}
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
            g.drawMazeWall(maze, item.id);

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