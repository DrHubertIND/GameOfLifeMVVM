using GameOfLifeMVVM.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLifeMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private int _rows;
        private int _columns;
        private GOFModel? _model;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawFieldClick(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtRows.Text, out int rows) && int.TryParse(txtColumns.Text, out int columns))
            {
                _rows = rows;
                _columns = columns;

                NewGrid(_rows, _columns);

                _model = new(_rows, _columns);

                Draw(_model.Grid);
            }
            else
            {
                MessageBox.Show("Invalid input! Please enter valid row and column counts.");
            }
        }

        private void NewGrid(in int rows, in int columns)
        {
            
            ClearGrid();
            for (int i = 0; i < rows; i++)
            {
                RowDefinition rowDefinition = new RowDefinition();
                gridGame.RowDefinitions.Add(rowDefinition);
            }

            for (int i = 0; i < columns; i++)
            {
                ColumnDefinition columnDefinition = new ColumnDefinition();
                gridGame.ColumnDefinitions.Add(columnDefinition);
            }
        }

        private void ClearGrid()
        {
            gridGame.Children.Clear();
            gridGame.RowDefinitions.Clear();
            gridGame.ColumnDefinitions.Clear();
        }
        private void Draw()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    Border border = new Border();
                    border.BorderBrush = Brushes.RoyalBlue;
                    border.BorderThickness = new Thickness(1);
                    Grid.SetRow(border, i);
                    Grid.SetColumn(border, j);
                    gridGame.Children.Add(border);
                }
            }
        }

        private void Draw(in int[,] gof)
        {
            NewGrid(_rows, _columns);
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    Rectangle rectangle = new Rectangle();
                    rectangle.Stroke = Brushes.RoyalBlue;
                    rectangle.StrokeThickness = 1;
                    rectangle.Fill = gof[i, j] == 1 ? Brushes.RoyalBlue : Brushes.Transparent;
                    Grid.SetRow(rectangle, i);
                    Grid.SetColumn(rectangle, j);
                    gridGame.Children.Add(rectangle);
                }
            }
        }

        async private void GameClick(object sender, EventArgs e) 
        {
            ClearGrid();
            Draw(_model.Grid);
        }

        private void StopGameClick(object sender, EventArgs e)
        {
            ClearGrid();
        }

    }
}