using GameOfLifeMVVM.Infrastructure.Commands;
using GameOfLifeMVVM.Model;
using GameOfLifeMVVM.ViewModel.Base;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLifeMVVM.ViewModel
{
    internal class MainGOFVM : BaseVM
    {
        public MainGOFVM()
        {
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecuted);
            StartGameCommand = new LambdaCommand(OnStartGameCommandExecuted, CanStartGameCommandExecuted);
        }

        private int _rows = 0;
        private int _columns = 0;

        #region Commands

        #region CloseComandRegion
        public ICommand CloseAppCommand { get; }

        private bool CanCloseAppCommandExecuted(object p) => true;
        private void OnCloseAppCommandExecuted(object p)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region StartGameCommands

        public ICommand StartGameCommand { get; }

        private bool CanStartGameCommandExecuted(object p) => true;
        private void OnStartGameCommandExecuted(object p)
        {
            DrawGame();
        }

        #endregion

        #endregion

        private Grid _mainGrid;
        public Grid Grid
        {
            get => _mainGrid;
            set
            {
                _mainGrid = value;
            }
        }


        private GOFModel? _model;

        private string _TxtRow = "0";
        public string TxtRows
        {
            get => _TxtRow;
            set
            {
                Set(ref _TxtRow, value);
                UpdateGridRows();
                DrawGrid();
            }
        }

        private string _TxtColumn = "0";
        public string TxtColumns
        {
            get => _TxtColumn;
            set
            {
                Set(ref _TxtColumn, value);
                UpdateGridColumns();
                DrawGrid();
            }
        }

        private void UpdateGridRows()
        {
            if (int.TryParse(TxtRows, out var rows))
            {
                _rows = rows;
                _mainGrid.Children.Clear();
                _mainGrid.RowDefinitions.Clear();

                RowDef();
            }
            else
            {
                MessageBox.Show("Invalid input! Please enter valid row count");
            }
        }

        private void UpdateGridColumns()
        {
            if (int.TryParse(TxtColumns, out var columns))
            {
                _columns = columns;
                _mainGrid.Children.Clear();
                _mainGrid.ColumnDefinitions.Clear();

                ColumnDef();
            }
            else
            {
                MessageBox.Show("Invalid input! Please enter valid column count");
            }
        }


        private void DrawGrid()
        {
            if (_rows != 0 && _columns != 0)
            {
                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        var border = new Border();
                        border.BorderBrush = Brushes.RoyalBlue;
                        border.BorderThickness = new Thickness(0.5);
                        System.Windows.Controls.Grid.SetRow(border, i);
                        System.Windows.Controls.Grid.SetColumn(border, j);
                        _mainGrid.Children.Add(border);
                    }
                }
            }
        }

        private void DrawGame()
        {
            if (_rows != 0 && _columns != 0)
            {
                _model ??= new GOFModel(_rows, _columns);

                var gof = _model.GridData;

                NewGrid();
                RowDef();
                ColumnDef();


                //DrawGrid();

                UpdateGrid(gof);
            }
            else
            {
                MessageBox.Show("Invalid input! Please enter valid column count");
            }
        }

        private void NewGrid()
        {
            _mainGrid.Children.Clear();
            _mainGrid.RowDefinitions.Clear();
            _mainGrid.ColumnDefinitions.Clear();
        }

        private void ColumnDef()
        {
            for (int i = 0; i < _columns; i++)
            {
                var columnDefeniton = new ColumnDefinition();
                _mainGrid.ColumnDefinitions.Add(columnDefeniton);
            }
        }

        private void RowDef()
        {
            for (int i = 0; i < _rows; i++)
            {
                var rowDefinition = new RowDefinition();
                _mainGrid.RowDefinitions.Add(rowDefinition);
            }
        }

        private async void UpdateGrid(int[,] gof)
        {
            while (true)
            {
                NewGrid();
                RowDef();
                ColumnDef();

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
                        _mainGrid.Children.Add(rectangle);
                    }
                }


                gof = _model.GridData;

                await Task.Delay(100);

                OnPtopertyChaged();
            }
        }
    }
}
