using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

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
            new MazeGenerationAlgorithm{id = 0, method = "Stick Down Method"}
        };

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = mga;
        }

        private void generateMaze(object sender, RoutedEventArgs e)
        {
            g.drawMazeGrid(maze, 2 * (int)grid_count.Value + 1, 2 * (int)grid_count.Value + 1);

            if (maze.Children.Count != 0)
            {
                maze_generate.IsEnabled = false;
                maze_reset.IsEnabled = true;
            }
        }

        private void resetMaze(object sender, RoutedEventArgs e)
        {
            g.eraseMazeGrid(maze);

            if (maze.Children.Count == 0)
            {
                maze_generate.IsEnabled = true;
                maze_reset.IsEnabled = false;
            }
        }
    }
}