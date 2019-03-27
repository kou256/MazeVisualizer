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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mazeGenerate(object sender, RoutedEventArgs e)
        {
            Maze m = new Maze(16, 16, (int)grid_count.Value, (int)grid_count.Value);
            m.drawGrid(maze);
        }
        private void mazeReset(object sender, RoutedEventArgs e)
        {

        }
    }
}