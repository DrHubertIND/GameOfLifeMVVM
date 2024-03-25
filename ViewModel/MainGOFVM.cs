using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GameOfLifeMVVM.ViewModel.Base;

namespace GameOfLifeMVVM.ViewModel
{
    public class MainGOFVM : BaseVM
    {
        private int _rows = 0;
        private int _columns = 0;
        public MainGOFVM() 
        {

        }

        private Grid _mainGrid;
        public Grid Grid
        {
            get => _mainGrid;
            set
            {
                _mainGrid = value;
            }
        }

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

                for (int i = 0; i < rows; i++)
                {
                    var rowDefinition = new RowDefinition();
                    _mainGrid.RowDefinitions.Add(rowDefinition);
                }
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

                for (int i = 0;i < columns;i++)
                {
                    var columnDefeniton = new ColumnDefinition();
                    _mainGrid.ColumnDefinitions.Add(columnDefeniton);

                    
                }
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
                for (int i=0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns;j++)
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
        //private string _TxtColumn;
    }
}
