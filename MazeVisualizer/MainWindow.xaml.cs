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
                Maze m = new Maze(16, 16, 17, 17);
                m.drawGrid(maze);
            }
        }
}