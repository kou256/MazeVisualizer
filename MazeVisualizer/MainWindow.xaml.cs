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
        Maze m;
        Gui g;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void generateMaze(object sender, RoutedEventArgs e)
        {
            m = new Maze(16, 16, (int)grid_count.Value, (int)grid_count.Value);
            g = new Gui();
            g.drawMazeGrid(maze, m.Grid_Height, m.Grid_Width, m.Grid_Row, m.Grid_Column);
        }

        private void resetMaze(object sender, RoutedEventArgs e)
        {
            g.eraseMazeGrid(maze);
        }
    }
}