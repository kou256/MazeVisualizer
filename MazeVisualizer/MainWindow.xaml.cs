using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using static MazeVisualizer.Drawer;
using static MazeVisualizer.MazeGenerationAlgorithm;
using static MazeVisualizer.AlgorithmList;

namespace MazeVisualizer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        Maze m;
        ObservableCollection<AlgorithmList> al = new ObservableCollection<AlgorithmList>()
        {
            new AlgorithmList{AlgorithmId = Id.SDM, AlgorithmName = "棒倒し法"},
            new AlgorithmList{AlgorithmId = Id.WEM, AlgorithmName = "壁伸ばし法"},
            new AlgorithmList{AlgorithmId = Id.DM,  AlgorithmName = "穴掘り法"}
        };

        /* コンストラクタ */
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = al;
        }

        /* Generateボタンが押されたとき */
        private void generateMaze(object sender, RoutedEventArgs e)
        {
            m = new Maze { GridRow = 2 * (int)cell_count.Value + 1, GridColumn = 2 * (int)cell_count.Value + 1 };
            var item = generation_algorithm_box.SelectedItem as AlgorithmList;
            if (item.AlgorithmId == Id.SDM)
            {
                StickDownMethod(m);
            }
            else if (item.AlgorithmId == Id.WEM)
            {
                WallExtendMethod(m);
            }
            else if (item.AlgorithmId == Id.DM)
            {
                DiggingMethod(m);
            }
            drawMaze(maze_canvas, m);

            if (maze_canvas.Children.Count != 0)
            {
                maze_generate_button.IsEnabled = false;
                maze_reset_button.IsEnabled = true;
                generation_algorithm_box.IsEnabled = false;
            }
        }

        /* Resetボタンが押されたとき */
        private void resetMaze(object sender, RoutedEventArgs e)
        {
            eraseMaze(maze_canvas);

            if (maze_canvas.Children.Count == 0)
            {
                maze_generate_button.IsEnabled = true;
                maze_reset_button.IsEnabled = false;
                generation_algorithm_box.IsEnabled = true;
            }
        }

        /* MazeGenerationAlgorthmが選択されたとき */
        private void selectedAlgorithm(object sender, SelectionChangedEventArgs e)
        {
            var item = generation_algorithm_box.SelectedItem as AlgorithmList;
            if (item != null)
            {
                maze_generate_button.IsEnabled = true;
            }
        }
    }
}